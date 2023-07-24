using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
	private DataContext context;
	private ILogger<IndexModel> logger_;

	public Product? Product_ { get; set; }

	public IndexModel(DataContext ctx, ILogger<IndexModel> logger)
	{
		context = ctx;
		logger_ = logger;
	}

	public async Task<IActionResult> OnGetAsync(long id = 1)
	{
		Product_ = await context.Products.FindAsync(id);

		logger_.LogWarning("Line BEFORE IF Statement.");

        if (Product_ == null)
		{
			logger_.LogWarning("Inside the IF Statement.");

			return RedirectToPage("NotFound");
		}

        logger_.LogWarning("Line AFTER IF Statement.");

        return Page();
	}
}

