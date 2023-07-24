using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages;

public class EditorModel : PageModel
{
	private readonly DataContext context;
	private ILogger<EditorModel> _logger;

	public Product Product_ { get; set; } = new();

	public EditorModel(DataContext ctx, ILogger<EditorModel> logger)
	{
		context = ctx;
		_logger = logger;
	}

	public async Task OnGetAsync(long id)
	{
		Product_ = await context.Products.FindAsync(id) ?? new();
        _logger.LogWarning("GET REQUEST SENDING.");

    }

    public async Task OnPostAsync(long id, decimal price)
	{
		Product? p = await context.Products.FindAsync(id);

		if (p != null)
		{
			p.Price = price;
		}

		_logger.LogWarning("POST REQUEST SENDING.");

        await context.SaveChangesAsync();

        //return RedirectToPage();
	}

}

