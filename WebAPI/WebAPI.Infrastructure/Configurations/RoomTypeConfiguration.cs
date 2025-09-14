using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;

namespace WebAPI.Infrastructure.Configurations;

public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure( EntityTypeBuilder<RoomType> builder )
    {
        builder.HasKey( r => r.Id );

        builder.Property( r => r.Name )
               .IsRequired()
               .HasMaxLength( 200 );

        builder.Property( r => r.DailyPrice )
               .HasColumnType( "decimal(18,2)" )
               .IsRequired();

        builder.Property( r => r.Currency )
               .IsRequired();

        builder.Property( r => r.MinPersonCount )
               .IsRequired();

        builder.Property( r => r.MaxPersonCount )
               .IsRequired();

        builder.Property( r => r.Services )
               .HasConversion<int>()
               .IsRequired();

        builder.Property( r => r.Amenities )
               .HasConversion<int>()
               .IsRequired();

        builder.Property( r => r.PropertyId )
               .IsRequired();
    }
}
