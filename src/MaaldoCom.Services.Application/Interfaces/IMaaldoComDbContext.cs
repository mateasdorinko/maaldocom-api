using System.Security.Claims;
using MaaldoCom.Services.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MaaldoCom.Services.Application.Interfaces;

public interface IMaaldoComDbContext
{
    DbSet<MediaAlbum> MediaAlbums { get; set; }
    DbSet<Medium> Media { get; set; }
    DbSet<Knowledge> Knowledge { get; set; }
    DbSet<Tag> Tags { get; set; }
    
    Task<int> SaveChangesAsync(ClaimsPrincipal user, CancellationToken cancellationToken = default, bool audit = true);
    Task<IEnumerable<T>> ExecuteDataSetAsync<T>(string commandText, SqlParameter[]? parameters = null);
}