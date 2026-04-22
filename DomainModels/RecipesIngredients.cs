using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels;

[Table("RecipesIngredients")]
public class RecipesIngredients
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
    
    public float Grams { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}