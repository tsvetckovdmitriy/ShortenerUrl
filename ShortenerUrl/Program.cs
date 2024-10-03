using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

using ShortenerUrl.Data;
using ShortenerUrl.Interfaces;
using ShortenerUrl.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
builder.Services.AddDbContextPool<ApplicationDbContext>(options => {
		var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
		options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
	}
);
builder.Services.AddTransient<ShortUrlService>();
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

    try
    {
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.Run();
