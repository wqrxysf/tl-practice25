namespace WebAPI.Domain.Entities;

public class Property
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }
    public string Address { get; private set; }
    public decimal Latitude { get; private set; }
    public decimal Longitude { get; private set; }

    private readonly List<RoomType> _roomTypes = new List<RoomType>();
    public IReadOnlyCollection<RoomType> RoomTypes => _roomTypes.AsReadOnly();

    protected Property() { }

    public Property( string name,
        string counrty,
        string city,
        string address,
        decimal latitude,
        decimal longitude )
    {
        Id = Guid.NewGuid();
        UpdateName( name );
        UpdateCountry( counrty );
        UpdateCity( city );
        UpdateAddress( address );
        UpdateCoordinates( latitude, longitude );
    }

    public void UpdateName( string newName )
    {
        if ( string.IsNullOrWhiteSpace( newName ) )
        {
            throw new ArgumentException( "Name required" );
        }
        Name = newName;
    }

    public void UpdateCountry( string newCountry )
    {
        if ( string.IsNullOrWhiteSpace( newCountry ) )
        {
            throw new ArgumentException( "Country required" );
        }
        Country = newCountry;
    }

    public void UpdateCity( string newCity )
    {
        if ( string.IsNullOrWhiteSpace( newCity ) )
        {
            throw new ArgumentException( "City required" );
        }
        City = newCity;
    }

    public void UpdateAddress( string newAddress )
    {
        if ( string.IsNullOrWhiteSpace( newAddress ) )
        {
            throw new ArgumentException( "Address required" );
        }
        Address = newAddress;
    }

    public void UpdateCoordinates( decimal newLatitude, decimal newLongtitude )
    {
        Latitude = newLatitude;
        Longitude = newLongtitude;
    }

    public void AddRoomType( RoomType roomType )
    {
        if ( roomType == null )
        {
            throw new ArgumentNullException( nameof( roomType ) );
        }
        _roomTypes.Add( roomType );
    }

    public void RemoveRoomType( RoomType roomType )
    {
        if ( roomType == null )
        {
            throw new ArgumentNullException( nameof( roomType ) );
        }
        _roomTypes.Remove( roomType );
    }
}
