using EmployeeManagement.Application.GraphQL.Mutations;
using EmployeeManagement.Application.GraphQL.Queries;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.Enum;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();


// Register application services later on 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IInvitationService, InvitationService>();
builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// Add authorization services
builder.Services.AddAuthorization();

// Configure GraphQL
builder.Services
    .AddGraphQLServer()
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

//// Configure both endpoints
//if (app.Environment.IsDevelopment())
//{
//    app.UseGraphQLPlayground(); // Classic GraphQL playground
//    // OR (for Banana Cake Pop)
//    app.UseBananaCakePop(); // More modern IDE
//}


// Configure the HTTP request pipeline
app.UseRouting();

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
