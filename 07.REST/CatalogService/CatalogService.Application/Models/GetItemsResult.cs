namespace CatalogService.Application.Models;

public class GetItemsResult
{
    public int Page { get; set; }

    public int ItemsOnPage { get; set; }

    public List<ItemGet> Items { get; set; }

    public CategoryDto Category { get; set; }
}
