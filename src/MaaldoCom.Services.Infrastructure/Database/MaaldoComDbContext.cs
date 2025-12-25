using System.Data;
using System.Security.Claims;
using MaaldoCom.Services.Application.Interfaces;
using MaaldoCom.Services.Domain.Entities;
using MaaldoCom.Services.Domain.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MaaldoCom.Services.Infrastructure.Database;

public class MaaldoComDbContext(DbContextOptions<MaaldoComDbContext> options) : DbContext(options), IMaaldoComDbContext
{
    public DbSet<MediaAlbum> MediaAlbums { get; set; }
    public DbSet<Medium> Media { get; set; }
    public DbSet<Knowledge> Knowledge { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public async Task<int> SaveChangesAsync(ClaimsPrincipal user, CancellationToken cancellationToken = default, bool audit = true)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = user?.GetUserId() ?? entry.Entity.CreatedBy;
                    entry.Entity.Created = now;
                    entry.Entity.LastModified = now;
                    entry.Entity.LastModifiedBy = user?.GetUserId() ?? entry.Entity.CreatedBy;
                    break;
                case EntityState.Modified:
                    if (audit)
                    {
                        entry.Entity.LastModifiedBy = user?.GetUserId() ?? entry.Entity.LastModifiedBy;
                        entry.Entity.LastModified = now;
                    }

                    break;
                case EntityState.Deleted:
                    if (audit)
                    {
                        entry.Entity.LastModifiedBy = user?.GetUserId() ?? entry.Entity.LastModifiedBy;
                        entry.Entity.LastModified = now;
                        entry.State = EntityState.Modified;
                    }

                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> ExecuteDataSetAsync<T>(string commandText, SqlParameter[]? parameters = null)
        => await Task.Run(() => ExecuteDataSet<T>(commandText, parameters));

    private IEnumerable<T> ExecuteDataSet<T>(string commandText, SqlParameter[]? parameters = null)
    {
        var resultSet = new DataSet("Output");

        var conn = Database.GetDbConnection();
        if (conn.CreateCommand() is SqlCommand command)
        {
            command.CommandText = commandText;
            command.CommandTimeout = TimeSpan.FromMinutes(5).Seconds;
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            var adapter = new SqlDataAdapter(command);
            adapter.Fill(resultSet);
        }

        return from DataRow row in resultSet.Tables[0].Rows select GetItem<T>(row);
    }

    private static T GetItem<T>(DataRow dr)
    {
        var properties = typeof(T).GetProperties();
        var obj = Activator.CreateInstance<T>();
        foreach (DataColumn column in dr.Table.Columns)
        {
            var currentProperty = properties.First(x => x.Name == column.ColumnName);

            var type = currentProperty.PropertyType;
            var targetType = Nullable.GetUnderlyingType(type) ?? type; // Handle nullable types

            var value = dr[column.ColumnName] is DBNull
                ? null
                : Convert.ChangeType(dr[column.ColumnName], targetType);
            currentProperty.SetValue(obj, value);
        }

        return obj;
    }
}