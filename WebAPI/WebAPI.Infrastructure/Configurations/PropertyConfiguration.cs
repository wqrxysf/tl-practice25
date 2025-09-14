using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;

namespace WebAPI.Infrastructure.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure( EntityTypeBuilder<Property> builder )
    {
        builder.HasKey( p => p.Id );

        builder.Property( p => p.Name )
               .IsRequired()
               .HasMaxLength( 200 );

        builder.Property( p => p.Country )
               .IsRequired()
               .HasMaxLength( 100 );

        builder.Property( p => p.City )
               .IsRequired()
               .HasMaxLength( 100 );

        builder.Property( p => p.Address )
               .IsRequired()
               .HasMaxLength( 300 );

        builder.Property( p => p.Latitude )
               .HasColumnType( "decimal(9,6)" );

        builder.Property( p => p.Longitude )
               .HasColumnType( "decimal(9,6)" );

        builder.HasMany( p => p.RoomTypes ) 
                .WithOne()
                .HasForeignKey( rt => rt.PropertyId )
                .HasForeignKey( "PropertyId" )
                .OnDelete( DeleteBehavior.Cascade );

        builder.Navigation( p => p.RoomTypes )
            .UsePropertyAccessMode( PropertyAccessMode.Field );
    }
}
