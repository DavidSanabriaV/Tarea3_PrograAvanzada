using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tarea3.Constants;
using Tarea3.Data;
using Tarea3.Models;
using Tarea3.Repositories;
using Tarea3.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddIdentity<Persona, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/auth/login";
    options.AccessDeniedPath = "/auth/login";
});

// Repositories
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<ICompraRepository, CompraRepository>();

// Services
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<ICompraService, CompraService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed roles y admin
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Persona>>();

    foreach (var role in new[] { Roles.Admin, Roles.User })
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    var adminEmail = "admin@tiquetes.com";
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var admin = new Persona
        {
            UserName = adminEmail,
            Email = adminEmail,
            Nombre = "Administrador",
            Cedula = "000000000",
            Edad = 30
        };
        await userManager.CreateAsync(admin, "Admin123!");
        await userManager.AddToRoleAsync(admin, Roles.Admin);
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();