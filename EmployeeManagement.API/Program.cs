using EmployeeManagement.Application.GraphQL.Mutations;
using EmployeeManagement.Application.GraphQL.Queries;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.Enum;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register application services later on 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IInvitationService, InvitationService>();
builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization(); 

// Configure JWT Authentication (without authorization)
var jwtConfig = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfig["SecretKey"]))
        };

        // Optional: Disable authentication for development
        if (builder.Environment.IsDevelopment())
        {
            options.RequireHttpsMetadata = false;
        }
    });

// Minimal authorization services (required for GraphQL)
builder.Services.AddAuthorization(options =>
{
    // Empty policies - will allow all access
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:5173") // your Vite frontend origin
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()); // optional, if using cookies/auth
});

// Configure GraphQL
builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    // Add enum type with proper configuration
    .AddEnumType<UserRole>(descriptor =>
    {
        descriptor.Name("UserRole");
        descriptor.Value(UserRole.Manager).Name("MANAGER");
        descriptor.Value(UserRole.SystemAdmin).Name("SYSTEMADMIN");
        descriptor.Value(UserRole.GeneralEmployee).Name("GENERALEMPLOYEE");
    })
    // Add error handling
    .AddErrorFilter(error =>
    {
        // Customize error messages
        if (error.Exception is ArgumentException argEx)
            return error.WithMessage(argEx.Message);
        return error;
    });

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseRouting();

// Use CORS before MapGraphQL
app.UseCors("AllowFrontend");

// Add authentication middleware (without enforcing authorization)
app.UseAuthentication();
app.UseAuthorization();


// Enable GraphQL endpoint at /graphql
app.MapGraphQL();

// Ensure the database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // This will create the database and tables if they do not exist
    await dbContext.Database.MigrateAsync(); // Use Migrate for applying migrations
    await dbContext.SeedAsync(); // Seed the database with the default user
}

app.Run();
