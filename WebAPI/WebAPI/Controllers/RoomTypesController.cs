using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.Services;
using WebAPI.Dto.RoomTypes;

namespace WebAPI.Controllers;

[ApiController]
public class RoomTypesController : ControllerBase
{
    private readonly IRoomTypeService _roomTypeService;
    private readonly IPropertyService _propertyService;

    public RoomTypesController( IRoomTypeService roomTypeService, IPropertyService propertyService )
    {
        _roomTypeService = roomTypeService;
        _propertyService = propertyService;
    }

    [HttpGet( "api/properties/{propertyId:guid}/roomtypes" )]
    public ActionResult<IEnumerable<RoomTypeDto>> GetByProperty( Guid propertyId )
    {
        var prop = _propertyService.GetPropertyById( propertyId );
        if ( prop == null )
            return NotFound();

        var types = _roomTypeService.GetRoomTypesByProperty( propertyId );
        var dtos = types.Select( t => new RoomTypeDto
        {
            Id = t.Id,
            PropertyId = t.PropertyId,
            Name = t.Name,
            DailyPrice = t.DailyPrice,
            Currency = t.Currency,
            MinPersonCount = t.MinPersonCount,
            MaxPersonCount = t.MaxPersonCount,
            Services = t.Services,
            Amenities = t.Amenities
        } );

        return Ok( dtos );
    }

    [HttpGet( "api/roomtypes/{id:guid}" )]
    public ActionResult<RoomTypeDto> GetById( Guid id )
    {
        var rt = _roomTypeService.GetRoomTypeById( id );
        if ( rt == null )
            return NotFound();

        var dto = new RoomTypeDto
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
        };

        return Ok( dto );
    }

    [HttpPost( "api/properties/{propertyId:guid}/roomtypes" )]
    public ActionResult<RoomTypeDto> Create( Guid propertyId, [FromBody] CreateRoomTypeDto createDto )
    {
        var prop = _propertyService.GetPropertyById( propertyId );
        if ( prop == null )
            return NotFound( $"Property {propertyId} not found" );

        var created = _roomTypeService.CreateRoomType(
            propertyId,
            createDto.Name,
            createDto.DailyPrice,
            createDto.Currency,
            createDto.MinPersonCount,
            createDto.MaxPersonCount,
            createDto.Services,
            createDto.Amenities
        );

        var dto = new RoomTypeDto
        {
            Id = created.Id,
            PropertyId = created.PropertyId,
            Name = created.Name,
            DailyPrice = created.DailyPrice,
            Currency = created.Currency,
            MinPersonCount = created.MinPersonCount,
            MaxPersonCount = created.MaxPersonCount,
            Services = created.Services,
            Amenities = created.Amenities
        };

        return CreatedAtAction( nameof( GetById ), new { id = dto.Id }, dto );
    }

    [HttpPut( "api/roomtypes/{id:guid}" )]
    public ActionResult<RoomTypeDto> Update( Guid id, [FromBody] UpdateRoomTypeDto updateDto )
    {
        var existing = _roomTypeService.GetRoomTypeById( id );
        if ( existing == null )
            return NotFound();

        var updated = _roomTypeService.UpdateRoomType(
            id,
            updateDto.Name,
            updateDto.DailyPrice,
            updateDto.Currency,
            updateDto.MinPersonCount,
            updateDto.MaxPersonCount,
            updateDto.Services,
            updateDto.Amenities
        );

        var dto = new RoomTypeDto
        {
            Id = updated.Id,
            PropertyId = updated.PropertyId,
            Name = updated.Name,
            DailyPrice = updated.DailyPrice,
            Currency = updated.Currency,
            MinPersonCount = updated.MinPersonCount,
            MaxPersonCount = updated.MaxPersonCount,
            Services = updated.Services,
            Amenities = updated.Amenities
        };

        return Ok( dto );
    }

    [HttpDelete( "api/roomtypes/{id:guid}" )]
    public IActionResult Delete( Guid id )
    {
        var existing = _roomTypeService.GetRoomTypeById( id );
        if ( existing == null )
            return NotFound();

        _roomTypeService.DeleteRoomType( id );
        return NoContent();
    }
}
