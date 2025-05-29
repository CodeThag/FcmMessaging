using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Utilities.Helpers;

public class PaginatedList<T>
{
    public List<T> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }

    public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
public class PaginatedListViewModel<T, D>
{

    public List<D> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }

    public PaginatedListViewModel(List<D> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// This method executes the sql request before paginating
    /// </summary>
    /// <param name="source"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="mapper"></param>
    /// <returns></returns>
    public static async Task<PaginatedListViewModel<T, D>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, IMapper mapper)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ProjectTo<D>(mapper.ConfigurationProvider).ToListAsync();

        return new PaginatedListViewModel<T, D>(items, count, pageNumber, pageSize);
    }

    /// <summary>
    /// This receives an executed queue to paginate
    /// </summary>
    /// <param name="source"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="mapper"></param>
    /// <returns></returns>
    public static PaginatedListViewModel<T, D> Create(List<T> source, int pageNumber, int pageSize, IMapper mapper)
    {
        var items = mapper.Map<List<D>>(source);

        return new PaginatedListViewModel<T, D>(items, source.Count, pageNumber, pageSize);
    }
}
