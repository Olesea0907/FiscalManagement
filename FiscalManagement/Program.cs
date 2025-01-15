using System.Globalization;
using FiscalManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1) Opțional: setează cultura "ro-RO"
var cultureInfo = new CultureInfo("ro-RO");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// 2) Configurezi DbContext (SQL Server)
builder.Services.AddDbContext<FiscalDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging() // doar pe Development
           .LogTo(Console.WriteLine);
});

// 3) Configurezi Identity (user + roluri)
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    // Poți pune true dacă vrei să fie confirmat contul prin email.
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()                 // suport pentru roluri
.AddEntityFrameworkStores<FiscalDbContext>();

// 4) Activezi paginile Razor
builder.Services.AddRazorPages();

var app = builder.Build();

// 5) Seeding pentru roluri și useri
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Pasul A: creezi rolurile, dacă nu există
    string[] roles = { "Inspector", "SefDeSectie" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Pasul B: creezi userul Șef de secție
    var sefUser = await userManager.FindByEmailAsync("sef@gmail.com");
    if (sefUser == null)
    {
        sefUser = new IdentityUser
        {
            UserName = "sef@gmail.com",
            Email = "sef@gmail.com",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(sefUser, "Parola123!");
    }

    // Atribui rolul "SefDeSectie"
    if (!await userManager.IsInRoleAsync(sefUser, "SefDeSectie"))
    {
        await userManager.AddToRoleAsync(sefUser, "SefDeSectie");
    }

    // Pasul C: creezi userul Inspector
    var inspUser = await userManager.FindByEmailAsync("insp@gmail.com");
    if (inspUser == null)
    {
        inspUser = new IdentityUser
        {
            UserName = "insp@gmail.com",
            Email = "insp@gmail.com",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(inspUser, "Parola123!");
    }

    // Atribui rolul "Inspector"
    if (!await userManager.IsInRoleAsync(inspUser, "Inspector"))
    {
        await userManager.AddToRoleAsync(inspUser, "Inspector");
    }
}

// 6) Pipeline standard
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Run();
