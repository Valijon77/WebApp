using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebApp.Models;


namespace WebApp.Components;

public class CitySummary : ViewComponent
{
	private readonly CitiesData data;

	public CitySummary(CitiesData cdata)
	{
		data = cdata;
	}

	//public string Invoke()
	//{
	//	return $"{cdata.Cities.Count()} cities, {cdata.Cities.Sum(p => p.Population)} population";
	//}

	//public IViewComponentResult Invoke()
	//{
	//	return View(new CityViewModel
	//	{
	//		Cities = data.Cities.Count(),
	//		Population = data.Cities.Sum(c => c.Population),
	//	});
	//}

	//public IViewComponentResult Invoke()
	//{
	//	return Content("This is a <i><h3>string</h3></i>");
	//}

	//public IViewComponentResult Invoke()
	//{
	//	return new HtmlContentViewComponentResult(
	//		new HtmlString("This is a <i><h3>string</h3></i>")
	//	);
	//}

	//public string Invoke()
	//{
	//	if (RouteData.Values["controller"] != null)
	//	{
	//		return "Controller Request";
	//	}
	//	else
	//	{
	//		return "Razor Page Request";
	//	}
	//}

	public IViewComponentResult Invoke(string themeName="success")
	{
		ViewBag.Theme = themeName;
		return View(new CityViewModel
		{
			Cities = data.Cities.Count(),
			Population = data.Cities.Sum(c => c.Population),
		});

	}

}

