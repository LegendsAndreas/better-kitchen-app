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