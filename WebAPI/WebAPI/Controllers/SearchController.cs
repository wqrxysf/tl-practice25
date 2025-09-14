using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.Services;
using WebAPI.Dto.Search;
using WebAPI.Dto.Properties;
using WebAPI.Dto.RoomTypes;

namespace WebAPI.Controllers;

[ApiController]
[Route( "api/search" )]
public class SearchController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IRoomTypeService _roomTypeService;
    private readonly IReservationService _reservationService;

    public SearchController( IPropertyService propertyService, IRoomTypeService roomTypeService, IReservationService reservationService )
    {
        _propertyService = propertyService;
        _roomTypeService = roomTypeService;
        _reservationService = reservationService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<SearchResultDto>> Search(
        [FromQuery] string city,
        [FromQuery] DateTime arrivalDate,
        [FromQuery] DateTime departureDate,
        [FromQuery] int guests = 1,
        [FromQuery] decimal? maxPrice = null )
    {
        if ( string.IsNullOrWhiteSpace( city ) )
            return BadRequest( "City is required" );
        if ( departureDate <= arrivalDate )
            return BadRequest( "Departure must be after arrival" );

        var properties = _propertyService.GetAllProperties()
            .Where( p => p.City.Equals( city, StringComparison.OrdinalIgnoreCase ) )
            .ToList();

        var results = new List<SearchResultDto>();

        foreach ( var prop in properties )
        {
            var roomTypes = _roomTypeService.GetRoomTypesByProperty( prop.Id );

            foreach ( var rt in roomTypes )
            {
                if ( guests < rt.MinPersonCount || guests > rt.MaxPersonCount )
                    continue;

                if ( maxPrice.HasValue && rt.DailyPrice > maxPrice.Value )
                    continue;

                var overlapping = _reservationService
                    .GetReservations( prop.Id, arrivalDate.ToUniversalTime(), departureDate.ToUniversalTime(), null )
                    .Where( r => r.RoomTypeId == rt.Id && !r.IsCanceled );

                if ( overlapping.Any() )
                    continue;

                var result = new SearchResultDto
                {
                    Property = new PropertyDto
                    {
                        Id = prop.Id,
                        Name = prop.Name,
                        Country = prop.Country,
                        City = prop.City,
                        Address = prop.Address,
                        Latitude = prop.Latitude,
                        Longitude = prop.Longitude
                    },
                    RoomType = new RoomTypeDto
                    {
                        Id = rt.Id,
                        PropertyId = rt.PropertyId,
                        Name = rt.Name,
                        DailyPrice = rt.DailyPrice,
                        Currency = rt.Currency,
                        MinPersonCount = rt.MinPersonCount,
                        MaxPersonCount = rt.MaxPersonCount,
                        Services = rt.Services,
                        Amenities = rt.Amenities
                    }
                };

                results.Add( result );
            }
        }

        return Ok( results );
    }
}
