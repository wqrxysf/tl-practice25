using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Configurations;

namespace WebAPI.Infrastructure;

public class WebApiDbContext : DbContext
{
    public WebApiDbContext( DbContextOptions<WebApiDbContext> options )
        : base( options )
    {
    }

    public DbSet<Property> Properties { get; set; } = null!;
    public DbSet<RoomType> RoomTypes { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );

        modelBuilder.Entity<Property>()
            .Navigation( p => p.RoomTypes )
            .UsePropertyAccessMode( PropertyAccessMode.Field );

        modelBuilder.ApplyConfiguration( new PropertyConfiguration() );
        modelBuilder.ApplyConfiguration( new RoomTypeConfiguration() );
        modelBuilder.ApplyConfiguration( new ReservationConfiguration() );
    }

}