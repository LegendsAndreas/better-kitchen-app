using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels;

[Table("Recipes")]
public class Recipe
{
    [Key]
    public int Id { get; set; }
    public char MealType { get; set; }
    [StringLength(255)]
    public string Name { get; set; }
    [StringLength(255)]
    public string ImagePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    
    public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    // ... existing code ...
    public ICollection<RecipesIngredients> RecipesIngredients { get; set; } = new List<RecipesIngredients>();
}

public class NewRecipeDto
{
    public char MealType { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public Dictionary<int, float> Ingredients { get; set; }
}
