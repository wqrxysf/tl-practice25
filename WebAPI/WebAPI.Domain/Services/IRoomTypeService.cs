using WebAPI.Domain.Entities;
using WebAPI.Domain.Enums;

namespace WebAPI.Domain.Services;

public interface IRoomTypeService
{
    RoomType CreateRoomType( Guid propertyId, string name, decimal dailyPrice, Currency currency,
        int minPersonCount, int maxPersonCount, RoomService services, RoomAmenity amenities );

    RoomType UpdateRoomType( Guid roomTypeId, string name, decimal dailyPrice, Currency currency,
        int minPersonCount, int maxPersonCount, RoomService services, RoomAmenity amenities );

    void DeleteRoomType( Guid roomTypeId );

    RoomType? GetRoomTypeById( Guid roomTypeId );
    IEnumerable<RoomType> GetRoomTypesByProperty( Guid propertyId );
}