using WebAPI.Domain.Entities;

namespace WebAPI.Domain.Repositories;

public interface IPropertyRepository
{
    Property? GetById( Guid id );
    IEnumerable<Property> GetAll();
    void Add( Property property );
    void Update( Property property );
    void Delete( Property property );
}
