using ErrorHandling.Filter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new CustomExceptionFilterAttributeHandler() { ErrorPage = "Error1"})
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Shows the detailed developer exception page in Development mode.
    // This displays full stack trace and debug information when an exception occurs.
    app.UseDeveloperExceptionPage();

    // Handles non-exception status codes (e.g., 404, 403).
    // Responds with plain text and inserts the status code into the message.
    // "{0}" will be replaced with the actual status code.
    //app.UseStatusCodePages("text/plain", "There is an error. Status Code : {0}");

    // A lambda-based version of the StatusCodePages middleware.
    // Gives more control over the response (e.g., setting headers, writing custom messages).
    app.UseStatusCodePages(async context =>
    {
        // Set the response Content-Type to plain text.
        context.HttpContext.Response.ContentType = "text/plain";

        // Write a custom message containing the actual status code.
        await context.HttpContext.Response.WriteAsync(
            $"There is an error. Status Code : {context.HttpContext.Response.StatusCode}"
        );
    });

    //Also you can add it without customizing
    //app.UseStatusCodePages();
}
else
{
    app.UseHsts();
}
//app.UseExceptionHandler("/Home/Error");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();