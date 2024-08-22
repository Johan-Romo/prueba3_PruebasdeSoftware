using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar cultura personalizada basada en "es-ES" pero con dólares en lugar de euros
var customCulture = new CultureInfo("es-ES")
{
    NumberFormat = new NumberFormatInfo
    {
        CurrencySymbol = "$", // Símbolo de dólar
        CurrencyDecimalSeparator = ",",
        CurrencyGroupSeparator = ".",
        CurrencyDecimalDigits = 2
    }
};

// Configurar las opciones de localización
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(customCulture),
    SupportedCultures = new List<CultureInfo> { customCulture },
    SupportedUICultures = new List<CultureInfo> { customCulture }
};

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Aplicar la configuración de localización
app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
