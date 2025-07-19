using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services later on 


// Add authorization services
builder.Services.AddAuthorization();


// Configure GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();
    //.AddAuthorization(); // Optional: if you want to use authorization

var app = builder.Build();

// Ensure the database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // This will create the database and tables if they do not exist
    await dbContext.Database.MigrateAsync(); // Use Migrate for applying migrations
    await dbContext.SeedAsync(); // Seed the database with the default user
}

app.MapGraphQL(); // Maps the /graphql endpoint
app.Run();


// Define your Query class
public class Query
{
    public string Hello() => "Hello World!";
}

// Define your Mutation class (if needed)
public class Mutation
{
    // Mutation methods here
}