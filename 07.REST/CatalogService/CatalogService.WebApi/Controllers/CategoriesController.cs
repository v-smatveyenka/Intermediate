using CatalogService.Application.Models;
using CatalogService.Persistence.Infrastructure;
using CatalogService.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICatalogDbContext _dbContext;

    public CategoriesController(ICatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CategoryDto>> GetCategories()
    {
        var categories = _dbContext.Categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name });

        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(string categoryName)
    {
        var existingCategories = _dbContext.Categories.Where(x => x.Name == categoryName);
        if (existingCategories.Any())
        {
            return BadRequest($"Category with the name \"{categoryName}\" already exists");
        }

        _dbContext.Categories.Add(new Category
        {
            Name = categoryName
        });

        await _dbContext.SaveAsync();

        return Ok();

    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto categoryDto)
    {
        var category = _dbContext.Categories.FirstOrDefault(c => c.Id == categoryDto.Id);

        if (category == null)
        {
            return NotFound("Category does not exist");
        }

        category.Name = categoryDto.Name;

        await _dbContext.SaveAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound("Category does not exist");
        }

        var categoryItems = _dbContext.Items.Where(x => x.CategoryId == id);

        _dbContext.Items.RemoveRange(categoryItems);

        _dbContext.Categories.Remove(category);

        await _dbContext.SaveAsync();

        return Ok();
    }
}
