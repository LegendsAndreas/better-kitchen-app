using System.ComponentModel.DataAnnotations;

namespace DomainModels;

public class Instructions
{
    [Key]
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}