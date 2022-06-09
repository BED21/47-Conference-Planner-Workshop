using GraphQL.Data;
using GraphQL.DataLoader;
using GraphQL.Extensions;

using Microsoft.EntityFrameworkCore;

namespace GraphQL.Speakers;

[ExtendObjectType("Query")]
public class SpeakerQueries
{
    [UseApplicationDbContext]
    public Task<List<Speaker>> GetSpeakersAsync([ScopedService] ApplicationDbContext context) =>
            context.Speakers.ToListAsync();

    public Task<Speaker> GetSpeakerAsync(
        [ID(nameof(Speaker))] int id,
        SpeakerByIdDataLoader dataLoader,
        CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
}
