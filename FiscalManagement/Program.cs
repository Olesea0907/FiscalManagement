using System.Globalization;
using FiscalManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cultureInfo = new CultureInfo("ro-RO");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddDbContext<FiscalDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging() 
           .LogTo(Console.WriteLine);
});

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()                 
.AddEntityFrameworkStores<FiscalDbContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Inspector", "SefDeSectie" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    //  userul Șef de secție
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

    // userul Inspector
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
