using Shared.Core.Interfaces;

namespace Shared.Core.Dtos;

public static class TreeDto
{
    public static IEnumerable<TreeDto<TDto>> BuildTree<TDto>(IEnumerable<TDto> items, Func<TDto, Guid?> keySelector) where TDto : IDto
        => BuildTree(null, items.ToLookup(keySelector));

    private static IEnumerable<TreeDto<TDto>> BuildTree<TDto>(Guid? parentId, ILookup<Guid?, TDto> categoryLookup) where TDto : IDto
        => categoryLookup[parentId]
            .Select(x => new TreeDto<TDto>()
            {
                Item = x,
                SubItems = BuildTree(x.Id, categoryLookup)
            });
}

public class TreeDto<TDto> where TDto : IDto
{
    public TDto Item { get; set; }

    public IEnumerable<TreeDto<TDto>> SubItems { get; set; }
}