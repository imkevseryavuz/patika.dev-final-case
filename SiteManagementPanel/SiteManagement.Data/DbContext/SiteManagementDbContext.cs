using Microsoft.EntityFrameworkCore;
using SiteManagement.Data;
using SiteManagementPanel.Domain;

namespace SiteManagement.Data;

public class SiteManagementDbContext : DbContext
{
    public SiteManagementDbContext(DbContextOptions<SiteManagementDbContext> options) : base(options)
    {

    }
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<ApartmentStatus> ApartmentStatuses { get; set; }
    public DbSet<ApartmentType> ApartmentTypes { get; set; }
    public DbSet<Block> Blocks { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserLog> UserLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ApartmentConfiguration());
        modelBuilder.ApplyConfiguration(new ApartmentStatusConfiguration());
        modelBuilder.ApplyConfiguration(new ApartmentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BlockConfiguration());
        modelBuilder.ApplyConfiguration(new BillConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserLogConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}