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