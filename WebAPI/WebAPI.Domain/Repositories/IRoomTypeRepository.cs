using WebAPI.Domain.Entities;

namespace WebAPI.Domain.Repositories;

public interface IRoomTypeRepository
{
    RoomType? GetById( Guid id );
    IEnumerable<RoomType> GetByPropertyId( Guid propertyId );
    void Add( RoomType roomType );
    void Update( RoomType roomType );
    void Delete( RoomType roomType );
}
