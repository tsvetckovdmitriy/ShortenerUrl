using Microsoft.EntityFrameworkCore;

using ShortenerUrl.Entities;

namespace ShortenerUrl.Interfaces;
public interface IApplicationDbContext
{
    DbSet<ShortLink> ShortLinks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
