using WebAPI.Domain.Entities;

namespace WebAPI.Domain.Services;

public interface IPropertyService
{
    Property CreateProperty( string name, string country, string city, string address, decimal latitude, decimal longitude );
    Property UpdateProperty( Guid propertyId, string name, string country, string city, string address, decimal latitude, decimal longitude );
    void DeleteProperty( Guid propertyId );
    Property? GetPropertyById( Guid propertyId );
    IEnumerable<Property> GetAllProperties();
}
