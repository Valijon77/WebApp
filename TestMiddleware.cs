using WebApp.Models;

namespace WebApp;

public class TestMiddleware
{
	private readonly RequestDelegate next;

	public TestMiddleware(RequestDelegate requestDelegate)
	{
		next = requestDelegate;
	}

	public async Task Invoke(HttpContext context, DataContext dataContext)
	{
		if (context.Request.Path == "/test")
		{
			await context.Response.WriteAsync($"Products: {dataContext.Products.Count()}\n");
            await context.Response.WriteAsync($"Suppliers: {dataContext.Suppliers.Count()}\n");
            await context.Response.WriteAsync($"Categories: {dataContext.Categories.Count()}\n");


        }
        else
		{
			await next(context);
		}
	}
}

