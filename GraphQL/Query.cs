using GraphQL.Data;
using GraphQL.DataLoader;
using GraphQL.Extensions;

using Microsoft.EntityFrameworkCore;

namespace GraphQL;
public class Query
{
    [UseApplicationDbContext]
    public Task<List<Speaker>> GetSpeakersAsync([ScopedService] ApplicationDbContext context) =>
            context.Speakers.ToListAsync();

    public Task<Speaker> GetSpeakerAsync(
        int id,
        SpeakerByIdDataLoader dataLoader,
        CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
}
