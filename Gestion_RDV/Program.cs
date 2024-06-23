using Gestion_RDV.AutoMapper;
using Gestion_RDV.Filters;
using Gestion_RDV.Models.DataManager;
using Gestion_RDV.Models.DataManager.API_Gymbrodyssey.Models.DataManager;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IDataRepository<Address>, AddressManager>();
builder.Services.AddScoped<IDataRepository<Availability>, AvailabilityManager>();
builder.Services.AddScoped<IDataRepository<Comment>, CommentManager>();
builder.Services.AddScoped<IDataRepositoryConversation<Conversation>, ConversationManager>();
builder.Services.AddScoped<IDataRepository<Facture>, FactureManager>();
builder.Services.AddScoped<IDataRepositoryMessage<Message>, MessageManager>();
builder.Services.AddScoped<IDataRepository<Notification>, NotificationManager>();
builder.Services.AddScoped<IDataRepository<Office>, OfficeManager>();
builder.Services.AddScoped<IDataRepository<Post>, PostManager>();
builder.Services.AddScoped<IDataRepository<RendezVous>, RendezVousManager>();
builder.Services.AddScoped<IDataRepository<Review>, ReviewManager>();
builder.Services.AddScoped<IDataRepository<SocialMediaAccount>, SocialMediaAccountManager>();
builder.Services.AddScoped<IDataRepository<Subscription>, SubscriptionManager>();
builder.Services.AddScoped<IDataRepository<User>, UserManager>();
builder.Services.AddScoped<IDataRepository<ConversationUser>, ConversationUserManager>();
builder.Services.AddScoped<IDataRepository<LikePost>, LikePostManager>();
builder.Services.AddScoped<IDataRepository<LikeReview>, LikeReviewManager>();
builder.Services.AddScoped<IDataRepository<Diagnosis>, DiagnosisManager>();
builder.Services.AddScoped<IDataRepository<MedicalInfo>, MedicalInfoManager>();
builder.Services.AddScoped<IDataRepository<Prescription>,PrescriptionManager>();
builder.Services.AddScoped<IDataRepository<Medication>, MedicationManager>();
builder.Services.AddScoped<UserAuthorizationFilter>(provider =>
{
    var routeKey = "userId";
    return new UserAuthorizationFilter(routeKey);
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GestionRdvDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("GestionRDV")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});



var app = builder.Build();

app.UseCors(
        options => options.WithOrigins("https://localhost:7153").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
