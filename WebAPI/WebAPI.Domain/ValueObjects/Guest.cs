namespace WebAPI.Domain.ValueObjects;

public class Guest
{
    public string GuestName { get; private set; }
    public string GuestPhoneNumber { get; private set; }

    public Guest( string guestName, string guestPhoneNumber )
    {
        if ( string.IsNullOrWhiteSpace( guestName ) )
        {
            throw new ArgumentException( "Guest name is required", nameof( guestName ) );
        }
        if ( string.IsNullOrWhiteSpace( guestPhoneNumber ) )
        {
            throw new ArgumentException( "Guest phone is required", nameof( guestPhoneNumber ) );
        }

        GuestName = guestName;
        GuestPhoneNumber = guestPhoneNumber;
    }

    public Guest UpdateGuestName( string newName )
    {
        if ( string.IsNullOrWhiteSpace( newName ) )
        {
            throw new ArgumentException( "Guest name is required", nameof( newName ) );
        }
        return new Guest( newName, GuestPhoneNumber );
    }

    public Guest UpdateGuestPhoneNumber( string newPhoneNumber )
    {
        if ( string.IsNullOrWhiteSpace( newPhoneNumber ) )
        {
            throw new ArgumentException( "Guest phone is required", nameof( newPhoneNumber ) );
        }

        return new Guest( GuestName, newPhoneNumber );
    }
}
