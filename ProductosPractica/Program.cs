using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar cultura personalizada basada en "es-ES" pero con d�lares en lugar de euros
var customCulture = new CultureInfo("es-ES")
{
    NumberFormat = new NumberFormatInfo
    {
        CurrencySymbol = "$", // S�mbolo de d�lar
        CurrencyDecimalSeparator = ",",
        CurrencyGroupSeparator = ".",
        CurrencyDecimalDigits = 2
    }
};

// Configurar las opciones de localizaci�n
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

// Aplicar la configuraci�n de localizaci�n
app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
