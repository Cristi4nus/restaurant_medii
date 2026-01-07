using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using restaurant_medii.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

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
    options.SignIn.RequireConfirmedAccount = true)
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
