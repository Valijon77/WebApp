using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentController : ControllerBase
{
	private readonly DataContext context;
	//private HttpContext el_context;

	public ContentController(DataContext ctx)
	{
		context = ctx;
		//el_context = httpContext;
	}

	[HttpGet("string")]
	public string GetString() => "This is string response.";

	[HttpGet("object/{format?}")]
	[FormatFilter]
	[Produces("application/json", "application/xml")]
	public async Task<ProductBindingTarget?> GetObject()
	{
		Product p = await context.Products.FirstAsync();
		return new ProductBindingTarget()
		{
			Name = p.Name,
			Price = p.Price,
			CategoryId = p.CategoryId,
			SupplierId = p.SupplierId
		};
	}


	//[HttpPost]
	//[Consumes("application/json")]
	//public string SaveProductJson(ProductBindingTarget target)
	//{
	//	return $"JSON: {target.Name}";
	//}

	//[HttpPost]
	//[Consumes("application/xml")]
	//public string SaveProductXml(ProductBindingTarget target)
	//{
	//	return $"XML: {target.Name}";
	//}

	[HttpPost]
	[Consumes("application/json","application/xml")]
	public string SaveProductJsonXml(ProductBindingTarget product)
	{
		//if (Request.Headers["Content-Type"])
		//var httpCtx = HttpContext.Request.Headers["Content-Type"];
		//return $"{httpCtx}";

		string contentType = HttpContext.Request.Headers["Content-Type"];
		switch (contentType)
		{
			case "application/json":
				return $"JSON: {product.Name}";
			case "application/xml":
				return $"XML: {product.Name}";
			default:
				return "Unsuppooooooo!";
		}

	}

}

