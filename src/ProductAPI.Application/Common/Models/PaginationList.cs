namespace ProductAPI.Application.Common.Models;

public class PaginationList<T>
{
    public List<T> Elements { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }

     public PaginationList(List<T> elements, int count, int pageNumber, int pageSize)
    {
        Elements = elements;
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
       
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static PaginationList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PaginationList<T>(items, count, pageNumber, pageSize);
    }
}