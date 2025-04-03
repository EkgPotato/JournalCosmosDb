using WebApi.Common.Enum;

namespace WebApi.Common.Request;

public class SortRequest
{
    public string Field { get; set; } = null!;
    public SortOrder Order { get; set; }
}
