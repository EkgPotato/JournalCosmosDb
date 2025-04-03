using System.Reflection;
using WebApi.Common.Enum;

namespace WebApi.Common.Request;

public class GridRequest
{
    public PageRequest Paging { get; set; } = null!;
    public List<SortRequest> Sorts { get; set; } = new List<SortRequest>();
    public string? Search { get; set; }

    public static async ValueTask<GridRequest?> BindAsync(HttpContext httpContext, ParameterInfo parameter)
    {
        var query = httpContext.Request.Query;

        int.TryParse(query["paging[pageNumber]"], out var pageNumber);
        int.TryParse(query["paging[pageSize]"], out var pageSize);

        var paging = new PageRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var sorts = new List<SortRequest>();
        int i = 0;

        while (true)
        {
            var field = query[$"sorts[{i}].field"];
            var order = query[$"sorts[{i}].order"];

            if (string.IsNullOrEmpty(field) && string.IsNullOrEmpty(order))
                break;

            if (!SortOrder.TryParse(order, ignoreCase: true, out SortOrder parsedOrder))
            {
                parsedOrder = SortOrder.Asc;
            }

            sorts.Add(new SortRequest
            {
                Field = field,
                Order = parsedOrder
            });

            i++;
        }

        var search = query["search"].ToString();

        return new GridRequest
        {
            Paging = paging,
            Sorts = sorts,
            Search = search
        };
    }
}
