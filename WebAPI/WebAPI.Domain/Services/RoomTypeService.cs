using WebAPI.Domain.Entities;
using WebAPI.Domain.Enums;
using WebAPI.Infrastructure.UnitOfWork;

namespace WebAPI.Domain.Services;

public class RoomTypeService : IRoomTypeService
{
    private readonly IUnitOfWork _uow;

    public RoomTypeService( IUnitOfWork uow )
    {
        _uow = uow;
    }

    public RoomType CreateRoomType( Guid propertyId, string name, decimal dailyPrice, Currency currency,
        int minPersonCount, int maxPersonCount, RoomService services, RoomAmenity amenities )
    {
        var property = _uow.Properties.GetById( propertyId )
            ?? throw new FileNotFoundException( "Property not found" );

        var roomType = new RoomType( propertyId, name, dailyPrice, currency, minPersonCount, maxPersonCount, services, amenities );
        _uow.RoomTypes.Add( roomType );
        _uow.SaveChanges();
        return roomType;
    }

    public RoomType UpdateRoomType( Guid roomTypeId, string name, decimal dailyPrice, Currency currency,
        int minPersonCount, int maxPersonCount, RoomService services, RoomAmenity amenities )
    {
        var roomType = _uow.RoomTypes.GetById( roomTypeId )
            ?? throw new FileNotFoundException( "RoomType not found" );

        roomType.UpdatePrice( dailyPrice, currency );
        roomType.UpdateCapacity( minPersonCount, maxPersonCount );
        roomType.UpdateServices( services );
        roomType.UpdateAmenities( amenities );

        _uow.RoomTypes.Update( roomType );
        _uow.SaveChanges();

        return roomType;
    }

    public void DeleteRoomType( Guid roomTypeId )
    {
        var roomType = _uow.RoomTypes.GetById( roomTypeId )
            ?? throw new FileNotFoundException( "RoomType not found" );

        _uow.RoomTypes.Delete( roomType );
        _uow.SaveChanges();
    }

    public RoomType? GetRoomTypeById( Guid roomTypeId )
    {
        return _uow.RoomTypes.GetById( roomTypeId );
    }

    public IEnumerable<RoomType> GetRoomTypesByProperty( Guid propertyId )
    {
        return _uow.RoomTypes.GetByPropertyId( propertyId );
    }
}
