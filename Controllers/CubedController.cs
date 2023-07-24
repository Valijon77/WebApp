using WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class CubedController : Controller
{
	private DataContext context;

	public CubedController(DataContext ctx)
	{
		context = ctx;
	}

	public IActionResult Index()
	{
		return View("Cubed");
	}

	public IActionResult Cube(double num)
	{
		TempData["value"] = num.ToString();
		TempData["result"] = Math.Pow(num, 3).ToString();
		return RedirectToAction(nameof(Index));
	}

}

