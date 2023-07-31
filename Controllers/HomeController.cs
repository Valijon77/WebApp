using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers;

[AutoValidateAntiforgeryToken]
public class HomeController : Controller
{
	private DataContext context;
	private readonly ILogger _logger;

	private IEnumerable<Category> Categories => context.Categories;
	private IEnumerable<Supplier> Suppliers => context.Suppliers;

	public HomeController(DataContext data, ILogger<HomeController> logger)
	{
		context = data;
		_logger = logger;
	}

	public IActionResult Index()
	{
		return View(context.Products.Include(p => p.Category).Include(p => p.Supplier));
    }

	public async Task<IActionResult> Details(long id)
	{
		Product? p = await context.Products.Include(p => p.Category).Include(p => p.Supplier)
			.FirstOrDefaultAsync(p => p.ProductId == id) ?? new Product();

		ProductViewModel model = ViewModelFactory.Details(p);
		return View("ProductEditor", model);

	}

	public IActionResult Create()
	{
		return View("ProductEditor", ViewModelFactory.Create(new Product(), Categories, Suppliers));
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromForm] Product product)
	{
		_logger.LogInformation($"Product Information:\n{product.ProductId}\n{product.Name}\n{product.Price}\n{product.Category}\n{product.Supplier}");
		_logger.LogInformation("Before checking Model Validation.");
		if (ModelState.IsValid)
		{
			_logger.LogInformation("Inside the Model Validation.");
			product.ProductId = default;
			product.Category = default;
			product.Supplier = default;
			context.Products.Add(product);
			await context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		_logger.LogInformation("After checking Model Validation.");
		return View("ProductEditor", ViewModelFactory.Create(product, Categories, Suppliers));
	}

	public async Task<IActionResult> Edit(long id)
	{
		Product? p = await context.Products.FindAsync(id);
		if (p != null)
		{
			ProductViewModel model = ViewModelFactory.Edit(p, Categories, Suppliers);
			return View("ProductEditor", model);
		}
		return NotFound();
	}

	[HttpPost]
	public async Task<IActionResult> Edit([FromForm] Product product)
	{
		if (ModelState.IsValid)
		{
			product.Category = default;
			product.Supplier = default;
			context.Products.Update(product);
			await context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		return View("ProductEditor", ViewModelFactory.Edit(product, Categories, Suppliers));
	}

	public async Task<IActionResult> Delete(long id)
	{
		Product? p = await context.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefaultAsync(p => p.ProductId == id);
		if (p != null)
		{
			ProductViewModel model = ViewModelFactory.Delete(p);
			return View("ProductEditor", model);
		}
		return NotFound();
	}

	[HttpPost]
	public async Task<IActionResult> Delete([FromForm] Product product)
	{
		context.Products.Remove(product);
		await context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}


}

