using WebAPI.Domain.Repositories;

namespace WebAPI.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IPropertyRepository Properties { get; }
    IRoomTypeRepository RoomTypes { get; }
    IReservationRepository Reservations { get; }

    int SaveChanges();
}