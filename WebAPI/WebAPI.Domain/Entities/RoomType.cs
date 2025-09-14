using WebAPI.Domain.Enums;

namespace WebAPI.Domain.Entities;

public class RoomType
{
    public Guid Id { get; private set; }
    public Guid PropertyId { get; private set; }

    public string Name { get; private set; }
    public decimal DailyPrice { get; private set; }
    public Currency Currency { get; private set; }

    public int MinPersonCount { get; private set; }
    public int MaxPersonCount { get; private set; }

    public RoomService Services { get; private set; }
    public RoomAmenity Amenities { get; private set; }

    protected RoomType() { } // для EFCore

    public RoomType( Guid propertyId,
        string name,
        decimal dailyPrice,
        Currency currency,
        int minPersonCount,
        int maxPersonCount,
        RoomService services,
        RoomAmenity amenities )
    {
        Id = Guid.NewGuid();
        PropertyId = propertyId;
        Name = name;
        UpdatePrice( dailyPrice, currency );
        UpdateCapacity( minPersonCount, maxPersonCount );
        UpdateServices( services );
        UpdateAmenities( amenities );
    }

    public void UpdatePrice( decimal newPrice, Currency newCurrency )
    {
        if ( newPrice <= 0 )
            throw new ArgumentException( "Price must be positive." );
        DailyPrice = newPrice;
        Currency = newCurrency;
    }

    public void UpdateCapacity( int min, int max )
    {
        if ( min < 1 || max < min )
            throw new ArgumentException( "Invalid person count." );
        MinPersonCount = min;
        MaxPersonCount = max;
    }

    public void UpdateServices( RoomService services ) => Services = services;
    public void UpdateAmenities( RoomAmenity amenities ) => Amenities = amenities;

}
