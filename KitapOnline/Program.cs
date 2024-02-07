using Microsoft.EntityFrameworkCore;
using KitapOnline.Repositories.Abstract;
using KitapOnline.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;

using KitapOnline;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDefaultIdentity<IdentityUser>
       (options => options.SignIn.RequireConfirmedAccount = true)
       .AddRoles<IdentityRole>()
       .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();


builder.Services.Configure<IdentityOptions>(options =>
{     
 options.User.RequireUniqueEmail = true;
}
);
builder.Services.AddAuthentication();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";

}

);
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IRoleService, RoleService>();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );
    endpoints.MapControllerRoute(
       name: "home",
       pattern: "{controller=Home}/{action=Index}/{id?}"
       );
    endpoints.MapRazorPages();
    endpoints.MapAreaControllerRoute(
         name: "Admin",
         areaName: "Admin",
         pattern: "/Admin/{controller=Admin}/{action=Index}/{id?}");
});

await AppDbInitializer.SeedUsersAndRolesAsync(app);
AppDbInitializer.Seed(app);


app.Run();
