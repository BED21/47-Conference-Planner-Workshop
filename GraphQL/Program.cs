using GraphQL;
using GraphQL.Data;
using GraphQL.DataLoader;
using GraphQL.Types;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<ApplicationDbContext>(options => 
//            options.UseSqlite("Data Source=conferences.db"));

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(
        (s, opt) =>
            opt.UseSqlite($"Data Source=conferences.db")
            .LogTo(Console.WriteLine, new[]
            {
                DbLoggerCategory.Database.Command.Name
            }));

// Add GraphQL related services
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<SpeakerType>()
    .AddDataLoader<SpeakerByIdDataLoader>()
    .AddDataLoader<SessionByIdDataLoader>();




var app = builder.Build();

//app.UseRouting();

app.MapGraphQL("/");

app.Run();
