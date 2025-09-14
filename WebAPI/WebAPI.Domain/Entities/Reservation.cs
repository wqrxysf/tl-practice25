using WebAPI.Domain.Enums;
using WebAPI.Domain.ValueObjects;

namespace WebAPI.Domain.Entities;

public class Reservation
{
    public Guid Id { get; private set; }
    public Guid PropertyId { get; private set; }
    public Guid RoomTypeId { get; private set; }

    public DateTime ArrivalDateAtUtc { get; private set; }
    public DateTime DepartureDateAtUtc { get; private set; }
    public TimeSpan ArrivalTime { get; private set; }
    public TimeSpan DepartureTime { get; private set; }

    public Guest GuestInfo { get; private set; }

    public decimal Total { get; private set; }
    public Currency Currency { get; private set; }

    public bool IsCanceled { get; private set; } = false;

    protected Reservation() { } // для EFCore

    public Reservation( Guid propertyId,
        Guid roomTypeId,
        DateTime arrivalDateAtUtc,
        DateTime departureDateAtUtc,
        TimeSpan arrivalTime,
        TimeSpan departureTime,
        Currency currency,
        Guest guestInfo,
        decimal total )
    {
        Id = Guid.NewGuid();
        PropertyId = propertyId;
        RoomTypeId = roomTypeId;
        UpdateDates( arrivalDateAtUtc, departureDateAtUtc );
        UpdateTimes( arrivalTime, departureTime );
        UpdateGuestInfo( guestInfo );
        Currency = currency;
        IsCanceled = false;
        GuestInfo = guestInfo;
        Total = total;
    }

    public void UpdateDates( DateTime newArrivalDateAtUtc, DateTime newDepartureDateAtUtc )
    {
        if ( newArrivalDateAtUtc == default )
        {
            throw new ArgumentException( "Arrival date is required", nameof( newArrivalDateAtUtc ) );
        }
        if ( newDepartureDateAtUtc == default )
        {
            throw new ArgumentException( "Departure date is required", nameof( newDepartureDateAtUtc ) );
        }
        if ( newArrivalDateAtUtc >= newDepartureDateAtUtc )
        {
            throw new ArgumentException( "Departure date must be greater than arrival date" );
        }
        ArrivalDateAtUtc = newArrivalDateAtUtc;
        DepartureDateAtUtc = newDepartureDateAtUtc;
    }

    public void UpdateTimes( TimeSpan newArrivalTime, TimeSpan newDepartureTime )
    {
        if ( newArrivalTime == default )
        {
            throw new ArgumentException( "Arrival time is required", nameof( newArrivalTime ) );
        }
        if ( newDepartureTime == default )
        {
            throw new ArgumentException( "Departure time is required", nameof( newDepartureTime ) );
        }
        ArrivalTime = newArrivalTime;
        DepartureTime = newDepartureTime;
    }

    public void UpdateGuestInfo( Guest newGuestInfo )
    {
        if ( newGuestInfo == null )
        {
            throw new ArgumentNullException( nameof( newGuestInfo ), "Guest info is required" );
        }
        GuestInfo = newGuestInfo;
    }

    public void CancelReservation()
    {
        IsCanceled = true;
    }
}
