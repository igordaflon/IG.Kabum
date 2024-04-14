using KBM.Catalogo.API.Models;
using KBM.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace KBM.Catalogo.API.Data;

public class CatalogoContext : DbContext, IUnitOfWork
{
    public CatalogoContext(DbContextOptions<CatalogoContext> options)
        :base(options) { }

    public DbSet<Produto> Produto { get; set; }

    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
        {
            property.SetColumnType("varchar(100)");
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
    }
}
