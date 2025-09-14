using WebAPI.Domain.Enums;
using WebAPI.Domain.ValueObjects;

namespace WebAPI.Dto.Reservations;

public class ReservationDto
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Guid RoomTypeId { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public Guest Guest { get; set; }
    public decimal Total { get; set; }
    public Currency Currency { get; set; }
}
