namespace WebAPI.Dto.Reservations;

public class UpdateReservationDto
{
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public string GuestName { get; set; }
    public string GuestPhoneNumber { get; set; }
}
