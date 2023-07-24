using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.OpenApi.Models;
//using System.Text.Json.Serialization;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Razor.TagHelpers;
//using WebApp.TagHelpers;
using Microsoft.AspNetCore.Antiforgery;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

//builder.Services.AddControllers();//.AddNewtonsoftJson()
//.AddXmlDataContractSerializerFormatters();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(opts =>
//{
//    opts.Cookie.IsEssential = true;
//});

//builder.Services.Configure<RazorPagesOptions>(opts =>
//{
//    opts.Conventions.AddPageRoute("/Index", "/extra/page/{id:long?}");
//});

//builder.Services.Configure<MvcNewtonsoftJsonOptions>(opts =>
//{
//    opts.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
//});

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp", Version = "v1" });
//});

//builder.Services.Configure<JsonOptions>(opts =>
//{
//    opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//});

//builder.Services.Configure<MvcOptions>(opts =>
//{
//    opts.RespectBrowserAcceptHeader = true;
//    opts.ReturnHttpNotAcceptable = true;
//});

builder.Services.AddSingleton<CitiesData>();
//builder.Services.AddTransient<ITagHelperComponent, TimeTagHelperComponent>();
//builder.Services.AddTransient<ITagHelperComponent, TableFooterTagHelperComponent>();

builder.Services.Configure<AntiforgeryOptions>(opts =>
{
    opts.HeaderName = "X-XSRF-TOKEN";
});

builder.Services.Configure<MvcOptions>(opts =>
{
    opts.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Please enter a value!!!");
});

var app = builder.Build();

app.UseStaticFiles();

IAntiforgery antiforgery = app.Services.GetRequiredService<IAntiforgery>();
app.Use(async (context, next) =>
{
    if (!context.Request.Path.StartsWithSegments("/api"))
    {
        string? token = antiforgery.GetAndStoreTokens(context).RequestToken;
        if (token != null)
        {
            context.Response.Cookies.Append("XSRF-TOKEN", token, new CookieOptions { HttpOnly = false });
        }
    }
    await next();
});

//app.UseSession();
//app.MapControllers();
//app.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
//app.MapDefaultControllerRoute();
app.MapControllerRoute("forms", "controllers/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

//const string BASE_URL = "api/products";

//app.UseMiddleware<WebApp.TestMiddleware>();

//app.MapGet($"{BASE_URL}/{{id}}", async (HttpContext context, DataContext dataContext) =>
//{
//    string? id = context.Request.RouteValues["id"] as string;
//    if (id != null)
//    {
//        Product? p = dataContext.Products.Find(long.Parse(id));
//        if (p == null)
//        {
//            context.Response.StatusCode = StatusCodes.Status404NotFound;
//        }
//        else
//        {
//            context.Response.ContentType = "application/json";
//            await context.Response.WriteAsync(JsonSerializer.Serialize<Product>(p));
//        }
//    }
//});

//app.MapGet(BASE_URL, async (HttpContext context,DataContext dataContext) =>
//{
//    context.Response.ContentType = "application/json";
//    await context.Response.WriteAsync(JsonSerializer.Serialize<IEnumerable<Product>>(dataContext.Products));

//});

//app.MapPost(BASE_URL, async (HttpContext context, DataContext data) =>
//{
//    Product? p = await JsonSerializer.DeserializeAsync<Product>(context.Request.Body);
//    if (p != null)
//    {
//        await data.AddAsync(p);
//        await data.SaveChangesAsync();
//        context.Response.StatusCode = StatusCodes.Status200OK;
//    }
//});

//app.MapGet("/", () => "Hello World!");

//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp");
//});


var context = app.Services.CreateScope().ServiceProvider
    .GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

app.Run();
