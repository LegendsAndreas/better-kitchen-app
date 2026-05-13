using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace RecipesApi.Data;

public class AppDBContext : DbContext
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<RecipesIngredients> RecipesIngredients { get; set; }

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasIndex(r => r.Id).IsUnique();
        });
        
        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasIndex(i => i.Id).IsUnique();
        });
        
        modelBuilder.Entity<RecipesIngredients>(entity =>
        {
            entity.HasIndex(ri => ri.Id).IsUnique();

            entity.HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipesIngredients)
                .HasForeignKey(ri => ri.RecipeId);

            entity.HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipesIngredients)
                .HasForeignKey(ri => ri.IngredientId);
        });
    }

    private void SeedRoles(ModelBuilder modelBuilder)
    {
    }
}