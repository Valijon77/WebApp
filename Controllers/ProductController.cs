using WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
	private readonly DataContext context;

	public ProductsController(DataContext ctx)
	{
		context = ctx;
	}

	[HttpGet]
	public IAsyncEnumerable<Product> GetProducts()
	{
		return context.Products.AsAsyncEnumerable();
	}

	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetProduct(long id)
	{
		//logger.LogDebug("GetProduct Action Invoked.");

		Product? p = await context.Products.FindAsync(id);

		if (p == null)
		{
			return NotFound();
		}

		return Ok(p);//new
		//{
		//	ProductId = p.ProductId,
		//	Name = p.Name,
		//	Price = p.Price,
		//	CategoryId = p.CategoryId,
		//	SupplierId = p.SupplierId
		//});

	}

	[HttpPost]
	public async Task<IActionResult> PostProduct(ProductBindingTarget target)
	{
		//await context.Products.AddAsync(target.ToProduct());
		//await context.SaveChangesAsync();

		Product p = target.ToProduct();
		await context.Products.AddAsync(p);
		await context.SaveChangesAsync();
		return Ok(p);

	}

	[HttpPut]
	public async Task UpdateProduct(Product product)
	{
		context.Products.Update(product);
		await context.SaveChangesAsync();
	}

	[HttpDelete("{id}")]
	public async Task DeleteProduct(long id)
	{
		context.Products.Remove(new Product { ProductId = id });
		await context.SaveChangesAsync();
	}

	[HttpGet("redirect")]
	public IActionResult RedirectP()
	{
		//return Redirect("/api/products/1");
		return RedirectToAction(nameof(GetProduct), new { Id = 1 });
	}

}

