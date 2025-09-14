using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.Services;
using WebAPI.Domain.ValueObjects;
using WebAPI.Dto.Reservations;

namespace WebAPI.Controllers;

[ApiController]
[Route( "api/reservations" )]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly IRoomTypeService _roomTypeService;
    private readonly IPropertyService _propertyService;

    public ReservationsController( IReservationService reservationService, IRoomTypeService roomTypeService, IPropertyService propertyService )
    {
        _reservationService = reservationService;
        _roomTypeService = roomTypeService;
        _propertyService = propertyService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReservationDto>> GetAll( [FromQuery] Guid? propertyId, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] string guestName = null, [FromQuery] string guestPhone = null )
    {
        Guest? guest = null;
        if ( !string.IsNullOrWhiteSpace( guestName ) || !string.IsNullOrWhiteSpace( guestPhone ) )
        {
            guest = new Guest( guestName ?? string.Empty, guestPhone ?? string.Empty );
        }

        var reservations = _reservationService.GetReservations( propertyId, from, to, guest );
        var dtos = reservations.Select( r => new ReservationDto
        {
            Id = r.Id,
            PropertyId = r.PropertyId,
            RoomTypeId = r.RoomTypeId,
            ArrivalDate = r.ArrivalDateAtUtc,
            DepartureDate = r.DepartureDateAtUtc,
            ArrivalTime = r.ArrivalTime,
            DepartureTime = r.DepartureTime,
            Guest = new Guest( r.GuestInfo.GuestName, r.GuestInfo.GuestPhoneNumber ),
            Total = r.Total,
            Currency = r.Currency
        } );

        return Ok( dtos );
    }

    [HttpGet( "{id:guid}" )]
    public ActionResult<ReservationDto> GetById( Guid id )
    {
        var r = _reservationService.GetReservationById( id );
        if ( r == null )
            return NotFound();

        var dto = new ReservationDto
        {
            Id = r.Id,
            PropertyId = r.PropertyId,
            RoomTypeId = r.RoomTypeId,
            ArrivalDate = r.ArrivalDateAtUtc,
            DepartureDate = r.DepartureDateAtUtc,
            ArrivalTime = r.ArrivalTime,
            DepartureTime = r.DepartureTime,
            Guest = new Guest( r.GuestInfo.GuestName, r.GuestInfo.GuestPhoneNumber ),
            Total = r.Total,
            Currency = r.Currency
        };

        return Ok( dto );
    }

    [HttpPost]
    public ActionResult<ReservationDto> Create( [FromBody] CreateReservationDto createDto )
    {
        var prop = _propertyService.GetPropertyById( createDto.PropertyId );
        if ( prop == null )
            return BadRequest( "Property not found" );

        var roomType = _roomTypeService.GetRoomTypeById( createDto.RoomTypeId );
        if ( roomType == null )
            return BadRequest( "RoomType not found" );

        var guest = new Guest( createDto.GuestName, createDto.GuestPhoneNumber );

        var currency = roomType.Currency;

        var arrivalUtc = createDto.ArrivalDate.ToUniversalTime();
        var departureUtc = createDto.DepartureDate.ToUniversalTime();

        var created = _reservationService.CreateReservation(
            createDto.PropertyId,
            createDto.RoomTypeId,
            arrivalUtc,
            departureUtc,
            createDto.ArrivalTime,
            createDto.DepartureTime,
            currency,
            guest
        );

        var dto = new ReservationDto
        {
            Id = created.Id,
            PropertyId = created.PropertyId,
            RoomTypeId = created.RoomTypeId,
            ArrivalDate = created.ArrivalDateAtUtc,
            DepartureDate = created.DepartureDateAtUtc,
            ArrivalTime = created.ArrivalTime,
            DepartureTime = created.DepartureTime,
            Guest = created.GuestInfo,
            Total = created.Total,
            Currency = created.Currency
        };

        return CreatedAtAction( nameof( GetById ), new { id = dto.Id }, dto );
    }

    [HttpDelete( "{id:guid}" )]
    public IActionResult Cancel( Guid id )
    {
        var r = _reservationService.GetReservationById( id );
        if ( r == null )
            return NotFound();

        _reservationService.CancelReservation( r );
        return NoContent();
    }
}
