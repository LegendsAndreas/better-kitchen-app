using DomainModels;
using Microsoft.AspNetCore.Mvc;
using RecipesApi.Data;

namespace RecipesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly AppDBContext _context;

    public RecipesController(AppDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("test-recipe")]
    public IActionResult TestRecipe()
    {
        Dictionary<int, float> ingredients = new Dictionary<int, float>()
        {
            { 1, 200 },
        };
        
        Recipe newRecipe = new Recipe()
        {
            Name = "Test Recipe",
            MealType = 'D',
            ImagePath = "images/placeholder.jpg",
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            RecipesIngredients = ingredients.Select(ingredient => new RecipesIngredients()
            {
                IngredientId = ingredient.Key,
                Grams = ingredient.Value,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            }).ToList()
        };

        _context.Recipes.Add(newRecipe);

        try
        {
            _context.SaveChanges();
            return Ok("Recipe created successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to create recipe: " + e.Message);
        }
    }

    [HttpGet]
    [Route("test")]
    public IActionResult TestConnection()
    {
        Recipe? recipe = _context.Recipes.FirstOrDefault();

        if (recipe == null)
        {
            return Ok("No recipes found");
        }

        return Ok("Connection successful");
    }

    [HttpGet]
    [Route("test-suite-2")]
    public IActionResult TestSuite2()
    {
        return Ok("Test suite 2 completed successfully");
    }

    [HttpPost("add")]
    public IActionResult AddRecipe([FromBody] NewRecipeDto newRecipeDto)
    {
        Recipe newRecipe = new Recipe()
        {
            Name = newRecipeDto.Name,
            MealType = newRecipeDto.MealType,
            ImagePath = newRecipeDto.ImagePath,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            RecipesIngredients = newRecipeDto.Ingredients.Select(ingredient => new RecipesIngredients()
            {
                IngredientId = ingredient.Key,
                Grams = ingredient.Value,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            }).ToList()
        };

        _context.Recipes.Add(newRecipe);

        try
        {
            _context.SaveChanges();
            return Ok("Recipe created successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Failed to create recipe: " + e.Message);
        }
    }
}