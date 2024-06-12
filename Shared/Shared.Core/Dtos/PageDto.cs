namespace Shared.Core.Dtos;

public class PageDto<T> where T : class
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