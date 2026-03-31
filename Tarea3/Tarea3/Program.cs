using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tarea3.Constants;
using Tarea3.Data;
using Tarea3.Models;
using Tarea3.Repositories;
using Tarea3.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    )
);

builder.Services.AddIdentity<Persona, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/Login";
});


builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<ICompraRepository, CompraRepository>();

builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<ICompraService, CompraService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddControllersWithViews();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = services.GetRequiredService<UserManager<Persona>>();

    string[] roles = { Roles.Admin, Roles.User };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
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
            Edad = 30,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(admin, "Admin123!");
        await userManager.AddToRoleAsync(admin, Roles.Admin);
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run(); ;