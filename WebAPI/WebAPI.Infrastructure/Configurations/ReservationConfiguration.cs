using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;

namespace WebAPI.Infrastructure.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure( EntityTypeBuilder<Reservation> builder )
    {
        builder.HasKey( r => r.Id );

        builder.Property( r => r.PropertyId )
               .IsRequired();

        builder.Property( r => r.RoomTypeId )
               .IsRequired();

        builder.Property( r => r.ArrivalDateAtUtc )
               .IsRequired();

        builder.Property( r => r.DepartureDateAtUtc )
               .IsRequired();

        builder.Property( r => r.ArrivalTime )
               .IsRequired();

        builder.Property( r => r.DepartureTime )
               .IsRequired();

        builder.OwnsOne( r => r.GuestInfo, g =>
        {
            g.Property( gu => gu.GuestName )
             .IsRequired()
             .HasMaxLength( 200 );
            g.Property( gu => gu.GuestPhoneNumber )
             .IsRequired()
             .HasMaxLength( 20 );
        } );

        builder.Property( r => r.Total )
               .HasColumnType( "decimal(18,2)" );

        builder.Property( r => r.Currency )
               .IsRequired();

        builder.Property( r => r.IsCanceled )
               .HasDefaultValue( false );
    }
}
