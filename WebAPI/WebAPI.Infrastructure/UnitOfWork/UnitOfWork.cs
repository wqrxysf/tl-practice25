using WebAPI.Domain.Repositories;
using WebAPI.Infrastructure;
using WebAPI.Infrastructure.Repositories;
using WebAPI.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly WebApiDbContext _context;

    public IPropertyRepository Properties { get; }
    public IRoomTypeRepository RoomTypes { get; }
    public IReservationRepository Reservations { get; }

    public UnitOfWork( WebApiDbContext context )
    {
        _context = context;
        Properties = new EfPropertyRepository( _context );
        RoomTypes = new EfRoomTypeRepository( _context );
        Reservations = new EfReservationRepository( _context );
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}