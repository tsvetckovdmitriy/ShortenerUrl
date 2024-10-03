using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using ShortenerUrl.Models;

namespace ShortenerUrl.Controllers;

public class HomeController : Controller
{
	public IActionResult Index()
	{
		return RedirectToRoute(new { controller = "ShortLinks", action = "Index"});
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
