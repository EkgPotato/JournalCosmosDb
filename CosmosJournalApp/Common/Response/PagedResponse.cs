namespace WebApi.Common.Response
{
    public class PagedResponse<T> where T : class
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalItemsCount { get; set; }
    }
}
