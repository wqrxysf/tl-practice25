using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.UnitOfWork;

namespace WebAPI.Domain.Services;

public class PropertyService : IPropertyService
{
    private readonly IUnitOfWork _uow;

    public PropertyService( IUnitOfWork uow )
    {
        _uow = uow;
    }

    public Property CreateProperty( string name, string country, string city, string address, decimal latitude, decimal longitude )
    {
        var property = new Property( name, country, city, address, latitude, longitude );
        _uow.Properties.Add( property );
        _uow.SaveChanges();
        return property;
    }

    public Property UpdateProperty( Guid propertyId, string name, string country, string city, string address, decimal latitude, decimal longitude )
    {
        var property = _uow.Properties.GetById( propertyId )
            ?? throw new FileNotFoundException( "Property not found" );

        property.UpdateName( name );
        property.UpdateCountry( country );
        property.UpdateCity( city );
        property.UpdateAddress( address );
        property.UpdateCoordinates( latitude, longitude );

        _uow.Properties.Update( property );
        _uow.SaveChanges();
        return property;
    }

    public void DeleteProperty( Guid propertyId )
    {
        var property = _uow.Properties.GetById( propertyId )
            ?? throw new FileNotFoundException( "Property not found" );

        _uow.Properties.Delete( property );
        _uow.SaveChanges();
    }

    public Property? GetPropertyById( Guid propertyId )
    {
        return _uow.Properties.GetById( propertyId );
    }

    public IEnumerable<Property> GetAllProperties()
    {
        return _uow.Properties.GetAll();
    }
}
