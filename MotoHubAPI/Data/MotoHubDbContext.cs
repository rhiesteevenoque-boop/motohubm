using Microsoft.EntityFrameworkCore;
using MotoHubAPI.Models;
namespace MotoHubAPI.Data;
public class MotoHubDbContext(DbContextOptions<MotoHubDbContext> options) : DbContext(options) {
 public DbSet<AppUser> Users => Set<AppUser>(); public DbSet<Motorcycle> Motorcycles => Set<Motorcycle>(); public DbSet<ServiceItem> Services => Set<ServiceItem>(); public DbSet<PartItem> Parts => Set<PartItem>(); public DbSet<Booking> Bookings => Set<Booking>();
 protected override void OnModelCreating(ModelBuilder b) { b.Entity<AppUser>().HasIndex(x=>x.Email).IsUnique(); b.Entity<PartItem>().HasIndex(x=>x.SKU).IsUnique(); b.Entity<Booking>().HasOne(x=>x.User).WithMany().HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.Restrict); b.Entity<Booking>().HasOne(x=>x.Motorcycle).WithMany().HasForeignKey(x=>x.MotorcycleId).OnDelete(DeleteBehavior.Restrict); b.Entity<Booking>().HasOne(x=>x.ServiceItem).WithMany().HasForeignKey(x=>x.ServiceItemId).OnDelete(DeleteBehavior.Restrict); b.Entity<ServiceItem>().Property(x=>x.Price).HasPrecision(10,2); b.Entity<PartItem>().Property(x=>x.Price).HasPrecision(10,2); }
}
