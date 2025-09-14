namespace WebAPI.Dto.Reservations;

public class CreateReservationDto
{
    public Guid PropertyId { get; set; }
    public Guid RoomTypeId { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public string GuestName { get; set; }
    public string GuestPhoneNumber { get; set; }
}
