using Microsoft.EntityFrameworkCore;
using SIA_2026_Zapatero.Shared.Services;
using SIA_2026_Zapatero.Web.Components;
using SIA_2026_Zapatero.Web.Services;
using SIA_2026_Zapatero.Web.Data;
using Microsoft.AspNetCore.Identity;
using SIA_2026_Zapatero.Web.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add device-specific services used by the SIA_2026_Zapatero.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();


builder.Services.AddDbContextFactory<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    options.UseSqlServer(connectionString);
});

builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();
//Auto db migration
AutoMigrationDb(app.Services);

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
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(
        typeof(SIA_2026_Zapatero.Shared._Imports).Assembly);

app.Run();

static void AutoMigrationDb(IServiceProvider sp)
{
    using var scope = sp.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DataContext>>().CreateDbContext();

    if (context.Database.GetPendingMigrations().Any())
        context.Database.Migrate();
}
