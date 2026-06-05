namespace DemoFluentUI.Services;

public class ExpenseService
{
    private readonly List<Expense> _items =
    [
        new() { Description = "Team lunch", Amount = 64.50m, Category = Expense.Categories.Food, Color = "#107C10" },
        new() { Description = "Taxi to airport", Amount = 38.00m, Category = Expense.Categories.Travel, Color = "#0078D4" },
        new() { Description = "Conference ticket", Amount = 499.00m, Category = Expense.Categories.Entertainment, Color = "#8764B8" },
    ];

    public IReadOnlyList<Expense> Items => _items;

    public event Action? Changed;

    public void Add(Expense expense)
    {
        _items.Insert(0, expense);
        Changed?.Invoke();
    }

    public void Update(Expense expense)
    {
        var index = _items.FindIndex(x => x.Id == expense.Id);
        if (index >= 0)
        {
            _items[index] = expense;
            Changed?.Invoke();
        }
    }

    public void Remove(Guid id)
    {
        _items.RemoveAll(x => x.Id == id);
        Changed?.Invoke();
    }

    public void ImportCsv(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var first = true;
        while (reader.ReadLine() is { } line)
        {
            if (first)
            {
                first = false;
                continue;
            }

            var parts = line.Split(',');
            if (parts.Length < 3) continue;
            if (!decimal.TryParse(parts[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var amount)) continue;

            if (!DateTime.TryParse(parts[2], System.Globalization.CultureInfo.InvariantCulture, out var date))
            {
                date = DateTime.Today;
            }

            _items.Insert(0, new Expense
            {
                Description = parts[0].Trim(),
                Amount = amount,
                Date = date,
                Category = parts.Length > 3 && Enum.TryParse<Expense.Categories>(parts[3].Trim(), out var category) ? category : Expense.Categories.Other,
                Color = "#605E5C",
            });
        }
        Changed?.Invoke();
    }
}
