using DemoFluentUI.Components;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register Fluent UI services
builder.Services.AddFluentUIComponents(config =>
{
    // Set default values for FluentButton component
    config.DefaultValues.For<FluentButton>().Set(p => p.Shape, ButtonShape.Circular);

    // Set default values for FluentDatePicker component
    config.DefaultValues.ForAny<FluentDatePicker<object>>().Set(p => p.Culture, System.Globalization.CultureInfo.GetCultureInfo("fr-FR"));
});

builder.Services.AddScoped<DemoFluentUI.Services.ExpenseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
