using GraphQL.Data;
using GraphQL.DataLoader;
using GraphQL.Speakers;
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
    .AddQueryType(d => d.Name("Query"))
        .AddTypeExtension<SpeakerQueries>()
    .AddMutationType(d => d.Name("Mutation"))
        .AddTypeExtension<SpeakerMutations>()
    .AddType<AttendeeType>()
    .AddType<SessionType>()
    .AddType<SpeakerType>()
    .AddType<TrackType>()
    //.EnableRelaySupport()
    .AddGlobalObjectIdentification()
    .AddDataLoader<SpeakerByIdDataLoader>()
    .AddDataLoader<SessionByIdDataLoader>();




var app = builder.Build();

//app.UseRouting();

app.MapGraphQL("/");

app.Run();
