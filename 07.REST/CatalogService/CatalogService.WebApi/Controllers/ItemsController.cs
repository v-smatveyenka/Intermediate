using CatalogService.Application.Models;
using CatalogService.Persistence.Infrastructure;
using CatalogService.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : Controller
{
    private readonly ICatalogDbContext _dbContext;

    public ItemsController(ICatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<GetItemsResult>> GetItems([FromQuery] int? categoryId = null, [FromQuery] int page = 1, [FromQuery] int itemsOnPage = 5)
    {
        var items = _dbContext
            .Items
            .Where(x => categoryId == null || x.CategoryId == categoryId)
            .Skip((page - 1) * itemsOnPage)
            .Take(itemsOnPage)
            .Select(x => new ItemGet
            {
                Id = x.Id,
                Name = x.Name,
                Category = new()
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                }
            });

        var result = new GetItemsResult
        {
            Page = page,
            ItemsOnPage = itemsOnPage,
            Items = items.ToList()
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddItem([FromBody] ItemDto item)
    {
        var existingItems = _dbContext.Items.Where(i => i.Name == item.Name && i.Category.Id == item.CategoryId);
        if (existingItems.Any())
        {
            return BadRequest($"Item with the name \"{item.Name}\" already exists");
        }

        var newCategory = _dbContext.Categories.FirstOrDefault(c => c.Id == item.CategoryId);

        if (newCategory == null)
        {
            return NotFound("Requested category does not exist");
        }

        _dbContext.Items.Add(new Item()
        {
            Name = item.Name,
            CategoryId = item.CategoryId
        });

        await _dbContext.SaveAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] ItemDto itemUpdateDto)
    {
        var item = _dbContext.Items.FirstOrDefault(x => x.Id == id);

        if (item == null)
        {
            return NotFound("Item does not exist");
        }

        var newCategory = _dbContext.Categories.FirstOrDefault(c => c.Id == itemUpdateDto.CategoryId);
        
        if (newCategory == null)
        {
            return NotFound("Requested category does not exist");
        }

        item.Name = itemUpdateDto.Name;
        item.CategoryId = itemUpdateDto.CategoryId;

        await _dbContext.SaveAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem([FromRoute] int id)
    {
        var item = _dbContext.Items.FirstOrDefault(x => x.Id == id);

        if (item == null)
        {
            return NotFound("Item does not exist");
        }

        _dbContext.Items.Remove(item);
        await _dbContext.SaveAsync();

        return Ok();
    }
}
