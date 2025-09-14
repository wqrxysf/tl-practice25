using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Repositories;

namespace WebAPI.Infrastructure.Repositories;

public class EfRoomTypeRepository : IRoomTypeRepository
{
    private readonly WebApiDbContext _context;

    public EfRoomTypeRepository( WebApiDbContext context )
    {
        _context = context;
    }

    public RoomType? GetById( Guid id )
    {
        return _context.RoomTypes.FirstOrDefault( rt => rt.Id == id );
    }

    public IEnumerable<RoomType> GetByPropertyId( Guid propertyId )
    {
        return _context.RoomTypes
            .Where( rt => rt.PropertyId == propertyId )
            .AsNoTracking()
            .ToList();
    }

    public void Add( RoomType roomType )
    {
        _context.RoomTypes.Add( roomType );
    }

    public void Update( RoomType roomType )
    {
        _context.RoomTypes.Update( roomType );
    }

    public void Delete( RoomType roomType )
    {
        _context.RoomTypes.Remove( roomType );
    }
}
