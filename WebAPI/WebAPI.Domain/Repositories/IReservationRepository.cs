using WebAPI.Domain.Entities;
using WebAPI.Domain.ValueObjects;

namespace WebAPI.Domain.Repositories;

public interface IReservationRepository
{
    Reservation? GetById( Guid id );
    void Add( Reservation reservation );
    void Update( Reservation reservation );
    void Delete( Guid id );

    IEnumerable<Reservation> GetReservations(
        Guid? propertyId,
        DateTime? from,
        DateTime? to,
        Guest? guest );
}
