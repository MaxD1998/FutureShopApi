namespace Shared.Infrastructure.Extensions;

public class PageDto<T>
{
    public PageDto(int currentPage, IEnumerable<T> items, int totalPages)
    {
        CurrentPage = currentPage;
        Items = items;
        TotalPages = totalPages;
    }

    public int CurrentPage { get; }

    public IEnumerable<T> Items { get; }

    public int TotalPages { get; }
}