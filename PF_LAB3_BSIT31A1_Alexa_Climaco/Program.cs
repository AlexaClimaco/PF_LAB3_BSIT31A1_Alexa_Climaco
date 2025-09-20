using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PF_LAB3_BSIT31A1_Alexa_Climaco.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Required for Identity UI

// GreedDbContext (cards)
builder.Services.AddDbContext<GreedDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GreedConnection")));

// Identity DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // optional, change if needed
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders(); // Enables password reset, email confirmation, etc.

var app = builder.Build();

// Configure pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Required for login/logout
app.UseAuthorization();

// Default route for MVC controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Enables Razor Pages (Identity pages like Login/Register)
app.MapRazorPages();

app.Run();
