using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using restaurant_medii.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
   policy.RequireRole("Admin"));
});


builder.Services.AddRazorPages(options =>
{
    options.Conventions.AllowAnonymousToPage("/Produse");
    options.Conventions.AuthorizePage("/Comenzi");
    options.Conventions.AuthorizePage("/Produse/Create", "AdminPolicy");
    options.Conventions.AuthorizePage("/Clienti/Delete", "AdminPolicy");
    options.Conventions.AllowAnonymousToPage("/Alergeni/Index");
    options.Conventions.AuthorizePage("/Alergeni/Create", "AdminPolicy");
    options.Conventions.AuthorizePage("/Alergeni/Edit", "AdminPolicy");
    options.Conventions.AuthorizePage("/Alergeni/Delete", "AdminPolicy");
    options.Conventions.AuthorizePage("/Categorii/Edit", "AdminPolicy");
    options.Conventions.AuthorizePage("/Categorii/Delete", "AdminPolicy");
});

builder.Services.AddDbContext<restaurant_mediiContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("restaurant_mediiContext")
        ?? throw new InvalidOperationException("Connection string 'restaurant_mediiContext' not found.")
    )
);

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("restaurant_mediiContext")
        ?? throw new InvalidOperationException("Connection string 'restaurant_mediiContext' not found.")
    )
);

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();

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
