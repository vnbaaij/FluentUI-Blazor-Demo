using System.ComponentModel.DataAnnotations;

namespace DemoFluentUI.Services;

public class Expense
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Please enter a description.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Between 2 and 80 characters.")]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 100_000, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Please pick a date.")]
    public DateTime? Date { get; set; } = DateTime.Today;

    public DateTime? Time { get; set; } = DateTime.Now;

    public Categories Category { get; set; } = Categories.General;

    public string Color { get; set; } = "#0078D4";

    public Expense Clone() => new()
    {
        Id = Id,
        Description = Description,
        Amount = Amount,
        Date = Date,
        Time = Time,
        Category = Category,
        Color = Color,
    };

    internal bool IsValid() => Validator.TryValidateObject(this, new ValidationContext(this), validationResults: null, validateAllProperties: true);   

    public static Categories[] AllCategories = [.. Enum.GetValues<Categories>().Cast<Expense.Categories>()];

    public enum Categories
    {
        General,
        Food,
        Transportation,
        Entertainment,
        Utilities,
        Healthcare,
        Education,
        Shopping,
        Travel,
        Other
    }
}
