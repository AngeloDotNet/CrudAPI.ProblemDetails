using Microsoft.EntityFrameworkCore;
using Sample.API.DataAccessLayer.Entity;

namespace Sample.API.DataAccessLayer.Infrastructure;

public partial class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {
    }

    public virtual DbSet<PersonEntity> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PersonEntity>(entity =>
        {
            entity.ToTable("People");
            entity.HasKey(e => e.Id);
        });
    }
}