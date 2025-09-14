using WebAPI.Dto.Properties;
using WebAPI.Dto.RoomTypes;

namespace WebAPI.Dto.Search;

public class SearchResultDto
{
    public PropertyDto Property { get; set; }
    public RoomTypeDto RoomType { get; set; }
}
