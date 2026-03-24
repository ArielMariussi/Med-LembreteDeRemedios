using Med.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Med.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<IdentityUser<long>,
        IdentityRole<long>, long>(options)
{

    public DbSet<Medicine> Medicines => Set<Medicine>();
    public DbSet<MedicineHistory> MedicineHistories => Set<MedicineHistory>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.ToTable("Medicines");

            entity.HasKey(m => m.Id);

            entity.Property(m => m.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(m => m.UserId)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(m => m.Time)
                  .IsRequired();

            entity.Property(m => m.Taken)
                  .IsRequired();
        });

        modelBuilder.Entity<MedicineHistory>(entity =>
        {
            entity.ToTable("MedicineHistories");
            entity.HasKey(h => h.Id);
            entity.Property(h => h.UserId).IsRequired().HasMaxLength(100);
            entity.Property(h => h.Date).IsRequired();
            entity.Property(h => h.Time).IsRequired();
            entity.HasOne(h => h.Medicine)
                  .WithMany()
                  .HasForeignKey(h => h.MedicineId);
        });
    }
}