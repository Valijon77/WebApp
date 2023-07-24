using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers;


public class HomeController : Controller
{
	private readonly DataContext context;

	public HomeController(DataContext ctx)
	{
		context = ctx;
	}

	public async Task<IActionResult> Index(long id = 1)
	{
		ViewBag.AveragePrice = await context.Products.AverageAsync(p => p.Price);

		return View(await context.Products.FindAsync(id));
		//Product? prod = await context.Products.FindAsync(id);
		//if (prod?.CategoryId == 1)
		//{
		//	return View("Watersports", prod);
		//}
		//else
		//{
		//	return View(prod);
		//}
	}

	//public IActionResult Common()
	//{
	//	return View();
	//}

	public IActionResult List()
	{
		return View(context.Products);
	}

	public IActionResult Html()
	{
		return View((object)"This is a <i><h3>string</h3></i>");
	}
}

