using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels;

[Table("Ingredients")]
public class Ingredient
{
    [Key]
    public int Id { get; set; }
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;
    [StringLength(255)]
    public string ImagePath { get; set; } = string.Empty;
    public float Calories { get; set; }
    public float Protein { get; set; }
    public float Fat { get; set; }
    public float Carbs { get; set; }
    public float CostPr100G { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    
    public ICollection<RecipesIngredients> RecipesIngredients { get; set; } = new List<RecipesIngredients>();
}

public class NewIngredientDto
{
    public string Name { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public float Calories { get; set; }
    public float Protein { get; set; }
    public float Fat { get; set; }
    public float Carbs { get; set; }
    public float CostPr100G { get; set; }
}