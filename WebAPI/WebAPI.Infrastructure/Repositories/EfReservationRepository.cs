using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Repositories;
using WebAPI.Domain.ValueObjects;

namespace WebAPI.Infrastructure.Repositories;

public class EfReservationRepository : IReservationRepository
{
    private readonly WebApiDbContext _context;

    public EfReservationRepository( WebApiDbContext context )
    {
        _context = context;
    }

    public Reservation? GetById( Guid id )
    {
        return _context.Reservations.FirstOrDefault( r => r.Id == id );
    }

    public void Add( Reservation reservation )
    {
        _context.Reservations.Add( reservation );
    }

    public void Update( Reservation reservation )
    {
        _context.Reservations.Update( reservation );
    }

    public void Delete( Guid id )
    {
        var reservation = _context.Reservations.FirstOrDefault( r => r.Id == id );
        if ( reservation != null )
        {
            _context.Reservations.Remove( reservation );
        }
    }

    public IEnumerable<Reservation> GetReservations( Guid? propertyId, DateTime? from, DateTime? to, Guest? guest )
    {
        IQueryable<Reservation> query = _context.Reservations.AsQueryable();

        if ( propertyId.HasValue )
            query = query.Where( r => r.PropertyId == propertyId.Value );

        if ( from.HasValue && to.HasValue )
        {
            query = query.Where( r =>
                !( r.DepartureDateAtUtc <= from.Value || r.ArrivalDateAtUtc >= to.Value ) );
        }
        else
        {
            if ( from.HasValue )
                query = query.Where( r => r.DepartureDateAtUtc >= from.Value );

            if ( to.HasValue )
                query = query.Where( r => r.ArrivalDateAtUtc <= to.Value );
        }

        if ( guest != null )
        {
            if ( !string.IsNullOrWhiteSpace( guest.GuestName ) )
                query = query.Where( r => r.GuestInfo.GuestName.Contains( guest.GuestName ) );
            if ( !string.IsNullOrWhiteSpace( guest.GuestPhoneNumber ) )
                query = query.Where( r => r.GuestInfo.GuestPhoneNumber.Contains( guest.GuestPhoneNumber ) );
        }

        return query.AsNoTracking().ToList();
    }
}
