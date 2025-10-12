using Smidge;
using Smidge.Cache;
using Smidge.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSmidge(builder.Configuration.GetSection("smidge"));
builder.Services.AddControllersWithViews();

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

app.UseSmidge(bundle =>
{
    // Create a JavaScript bundle named "bundle-js" that includes all files in the ~/js/ directory
    bundle.CreateJs("bundle-js", "~/js/")
        // Define environment-specific options for how the bundle should behave in Debug mode
        .WithEnvironmentOptions(BundleEnvironmentOptions.Create()
            // Configure options that apply only when the application is running in Debug mode
            .ForDebug(builder => builder
                // Enable combining multiple JS files into one bundle
                .EnableCompositeProcessing()
                // Enable watching JS files for changes, so the bundle updates automatically during development
                .EnableFileWatcher()
                // Use AppDomainLifetimeCacheBuster to ensure bundle cache resets when the app restarts
                .SetCacheBusterType<AppDomainLifetimeCacheBuster>()
                // Disable ETag headers and set Cache-Control max-age to 0 (for no caching during development)
                .CacheControlOptions(enableEtag: false, cacheControlMaxAge: 0))
            // Build the environment configuration
            .Build());

    bundle.CreateCss("bundle-css", "~/css/site.css", "~/lib/bootstrap/dist/css/bootstrap.min.css");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Lifetime.ApplicationStarted.Register(() =>
{
    Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
});


app.Run();

