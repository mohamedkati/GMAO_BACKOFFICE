namespace GMAO.Application.Helpers.Request
{
    public abstract class PagedRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
