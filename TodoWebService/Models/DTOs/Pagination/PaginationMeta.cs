namespace TodoWebService.Models.DTOs.Pagination
{
    public class PaginationMeta
    {
        public PaginationMeta(int page, int pageSize, int count)
        {
            Page = page;
            PageSize = pageSize;
            TotalPages = (count + pageSize - 1) / pageSize;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
