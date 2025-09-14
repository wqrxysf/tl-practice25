using WebAPI.Domain.Entities;
using WebAPI.Domain.Enums;
using WebAPI.Domain.ValueObjects;

namespace WebAPI.Domain.Services;

public interface IReservationService
{
    Reservation CreateReservation( Guid propertyId, Guid roomTypeId,
        DateTime arrivalAtUtc, DateTime departureAtUtc,
        TimeSpan arrivalTime, TimeSpan departureTime, Currency currency, Guest guest );

    void CancelReservation( Reservation reservation );

    Reservation? GetReservationById( Guid reservationId );

    IEnumerable<Reservation> GetReservations( Guid? propertyId, DateTime? from, DateTime? to, Guest? guest );
}
