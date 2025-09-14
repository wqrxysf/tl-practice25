using WebAPI.Domain.Entities;
using WebAPI.Domain.Enums;
using WebAPI.Domain.ValueObjects;
using WebAPI.Infrastructure.UnitOfWork;

namespace WebAPI.Domain.Services;

public class ReservationService : IReservationService
{
    private readonly IUnitOfWork _uow;

    public ReservationService( IUnitOfWork uow )
    {
        _uow = uow;
    }

    public Reservation CreateReservation( Guid propertyId, Guid roomTypeId, DateTime arrivalAtUtc, DateTime departureAtUtc,
        TimeSpan arrivalTime, TimeSpan departureTime, Currency currency, Guest guest )
    {
        var overlapping = _uow.Reservations.GetReservations(
            propertyId,
            arrivalAtUtc,
            departureAtUtc,
            null
        );

        if ( overlapping.Any( r => r.RoomTypeId == roomTypeId && !r.IsCanceled ) )
            throw new InvalidOperationException( "Room is already booked for the selected dates." );

        var roomType = _uow.RoomTypes.GetById( roomTypeId );
        if ( roomType == null )
            throw new InvalidOperationException( "RoomType not found" );

        int nights = ( departureAtUtc.Date - arrivalAtUtc.Date ).Days;
        if ( nights <= 0 )
            throw new InvalidOperationException( "Departure date must be after arrival date" );

        decimal total = roomType.DailyPrice * nights;

        var reservation = new Reservation(
            propertyId,
            roomTypeId,
            arrivalAtUtc,
            departureAtUtc,
            arrivalTime,
            departureTime,
            currency,
            guest,
            total
        );

        _uow.Reservations.Add( reservation );
        _uow.SaveChanges();

        return reservation;
    }

    public void CancelReservation( Reservation reservation )
    {
        reservation.CancelReservation();
        _uow.Reservations.Update( reservation );
        _uow.SaveChanges();
    }

    public Reservation? GetReservationById( Guid reservationId )
    {
        return _uow.Reservations.GetById( reservationId );
    }

    public IEnumerable<Reservation> GetReservations( Guid? propertyId, DateTime? from, DateTime? to, Guest? guest )
    {
        return _uow.Reservations.GetReservations( propertyId, from, to, guest );
    }
}
