using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Repositories;

namespace WebAPI.Infrastructure.Repositories;

public class EfPropertyRepository : IPropertyRepository
{
    private readonly WebApiDbContext _context;

    public EfPropertyRepository( WebApiDbContext context )
    {
        _context = context;
    }

    public Property? GetById( Guid id )
    {
        return _context.Properties
            .Include( p => p.RoomTypes )
            .FirstOrDefault( p => p.Id == id );
    }

    public IEnumerable<Property> GetAll()
    {
        return _context.Properties
            .Include( p => p.RoomTypes )
            .AsNoTracking()
            .ToList();
    }

    public void Add( Property property )
    {
        _context.Properties.Add( property );
    }

    public void Update( Property property )
    {
        _context.Properties.Update( property );
    }

    public void Delete( Property property )
    {
        _context.Properties.Remove( property );
    }
}
