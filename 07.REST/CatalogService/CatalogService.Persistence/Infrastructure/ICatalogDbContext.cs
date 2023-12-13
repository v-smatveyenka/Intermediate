using CatalogService.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Persistence.Infrastructure;

public interface ICatalogDbContext
{
    public DbSet<Item> Items { get; set; }

    public DbSet<Category> Categories { get; set; }

    public Task<int> SaveAsync();
}
