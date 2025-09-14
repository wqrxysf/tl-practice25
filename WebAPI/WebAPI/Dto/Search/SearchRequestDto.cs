using WebAPI.Domain.ValueObjects;

namespace WebAPI.Dto.Search;

public class SearchRequestDto
{
    public string City { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public Guest Guests { get; set; }
    public decimal? MaxPrice { get; set; }
}