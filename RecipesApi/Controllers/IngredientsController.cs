using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipesApi.Data;

namespace RecipesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly AppDBContext _context;

    public IngredientsController(AppDBContext context)
    {
        _context = context;
    }
    
    /**
     * Tests the full functionality of the API.
     * - Creats a new recipe
     * - Creates a new ingredient
     */
    [HttpGet]
    [Route("test-suite")]
    public IActionResult TestSuite()
    {
        _context.Ingredients.Add(new Ingredient
        {
            Name = "Test Ingredient",
            ImagePath = "https://via.placeholder.com/150",
            Calories = 100,
            Protein = 10,
            Fat = 5,
            Carbs = 20,
            CostPr100G = 1.99f,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        });
        try
        {
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to create ingredient: " + e.Message);
        }

        return Ok("Test suite completed successfully");
    }
    
    [HttpGet("{name}")]
    public async Task<ActionResult<Ingredient>> GetIngredientByName(string name)
    {
        var ingredient = await _context.Ingredients
            .AsNoTracking()
            .Select(i => new Ingredient
            {
                Id = i.Id,
                Name = i.Name,
                // Add any other scalar properties you need...
            })
            .FirstOrDefaultAsync(i => i.Name == name);

        if (ingredient == null)
        {
            return NotFound();
        }
        return Ok(ingredient);
    }
    
    [HttpGet("all-ingredients")]
    public async Task<ActionResult<Ingredient>> GetAllIngredients()
    {
        var ingredient = await _context.Ingredients.ToListAsync();
        if (ingredient == null)
        {
            return NotFound();
        }
        return Ok(ingredient);
    }
}