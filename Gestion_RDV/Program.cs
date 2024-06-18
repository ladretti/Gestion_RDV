using Gestion_RDV.Filters;
using Gestion_RDV.Models.DataManager.API_Gymbrodyssey.Models.DataManager;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IDataRepository<Address>, AddressManager>();
builder.Services.AddScoped<IDataRepository<Availability>, AvailabilityManager>();
builder.Services.AddScoped<IDataRepository<Comment>, CommentManager>();
builder.Services.AddScoped<IDataRepositoryConversation<Conversation>, ConversationManager>();
builder.Services.AddScoped<IDataRepository<Facture>, FactureManager>();
builder.Services.AddScoped<IDataRepository<Message>, MessageManager>();
builder.Services.AddScoped<IDataRepositoryNotification<Notification>, NotificationManager>();
builder.Services.AddScoped<IDataRepository<Office>, OfficeManager>();
builder.Services.AddScoped<IDataRepository<Post>, PostManager>();
builder.Services.AddScoped<IDataRepository<RendezVous>, RendezVousManager>();
builder.Services.AddScoped<IDataRepository<Review>, ReviewManager>();
builder.Services.AddScoped<IDataRepository<SocialMediaAccount>, SocialMediaAccountManager>();
builder.Services.AddScoped<IDataRepository<Subscription>, SubscriptionManager>();
builder.Services.AddScoped<IDataRepositoryUser<User>, UserManager>();
builder.Services.AddScoped<UserAuthorizationFilter>(provider =>
{
    var routeKey = "userId";
    return new UserAuthorizationFilter(routeKey);
});



builder.Services.AddControllers();


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
