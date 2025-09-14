using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.Services;
using WebAPI.Dto.Properties;

namespace WebAPI.Controllers;

[ApiController]
[Route( "api/properties" )]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertiesController( IPropertyService propertyService )
    {
        _propertyService = propertyService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PropertyDto>> GetAll()
    {
        var props = _propertyService.GetAllProperties();
        var dtos = props.Select( p => new PropertyDto
        {
            Id = p.Id,
            Name = p.Name,
            Country = p.Country,
            City = p.City,
            Address = p.Address,
            Latitude = p.Latitude,
            Longitude = p.Longitude
        } );

        return Ok( dtos );
    }

    [HttpGet( "{id:guid}" )]
    public ActionResult<PropertyDto> GetById( Guid id )
    {
        var p = _propertyService.GetPropertyById( id );
        if ( p == null )
            return NotFound();

        var dto = new PropertyDto
        {
            Id = p.Id,
            Name = p.Name,
            Country = p.Country,
            City = p.City,
            Address = p.Address,
            Latitude = p.Latitude,
            Longitude = p.Longitude
        };

        return Ok( dto );
    }

    [HttpPost]
    public ActionResult<PropertyDto> Create( [FromBody] CreatePropertyDto createDto )
    {
        var created = _propertyService.CreateProperty(
            createDto.Name,
            createDto.Country,
            createDto.City,
            createDto.Address,
            createDto.Latitude,
            createDto.Longitude
        );

        var dto = new PropertyDto
        {
            Id = created.Id,
            Name = created.Name,
            Country = created.Country,
            City = created.City,
            Address = created.Address,
            Latitude = created.Latitude,
            Longitude = created.Longitude
        };

        return CreatedAtAction( nameof( GetById ), new { id = dto.Id }, dto );
    }

    [HttpPut( "{id:guid}" )]
    public ActionResult<PropertyDto> Update( Guid id, [FromBody] UpdatePropertyDto updateDto )
    {
        var existing = _propertyService.GetPropertyById( id );
        if ( existing == null )
            return NotFound();

        var updated = _propertyService.UpdateProperty(
            id,
            updateDto.Name,
            updateDto.Country,
            updateDto.City,
            updateDto.Address,
            updateDto.Latitude,
            updateDto.Longitude
        );

        var dto = new PropertyDto
        {
            Id = updated.Id,
            Name = updated.Name,
            Country = updated.Country,
            City = updated.City,
            Address = updated.Address,
            Latitude = updated.Latitude,
            Longitude = updated.Longitude
        };

        return Ok( dto );
    }

    [HttpDelete( "{id:guid}" )]
    public IActionResult Delete( Guid id )
    {
        var existing = _propertyService.GetPropertyById( id );
        if ( existing == null )
            return NotFound();

        _propertyService.DeleteProperty( id );
        return NoContent();
    }
}
