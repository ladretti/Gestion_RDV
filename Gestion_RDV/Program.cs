using Gestion_RDV.Models.DataManager.API_Gymbrodyssey.Models.DataManager;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IDataRepository<Address>, AddressManager>();
builder.Services.AddScoped<IDataRepository<Availability>, AvailabilityManager>();
builder.Services.AddScoped<IDataRepository<Comment>, CommentManager>();
builder.Services.AddScoped<IDataRepositoryConversation<Conversation>, ConversationManager>();
builder.Services.AddScoped<IDataRepository<Facture>, FactureManager>();
builder.Services.AddScoped<IDataRepository<Message>, MessageManager>();
builder.Services.AddScoped<IDataRepository<Notification>, NotificationManager>();
builder.Services.AddScoped<IDataRepository<Office>, OfficeManager>();
builder.Services.AddScoped<IDataRepository<Post>, PostManager>();
builder.Services.AddScoped<IDataRepository<RendezVous>, RendezVousManager>();
builder.Services.AddScoped<IDataRepository<Review>, ReviewManager>();
builder.Services.AddScoped<IDataRepository<SocialMediaAccount>, SocialMediaAccountManager>();
builder.Services.AddScoped<IDataRepository<Subscription>, SubscriptionManager>();
builder.Services.AddScoped<IDataRepository<User>, UserManager>();



// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GestionRdvDbContext>(options =>
               options.UseNpgsql(builder.Configuration.GetConnectionString("GestionRDV")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
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
