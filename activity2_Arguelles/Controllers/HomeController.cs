using System.Diagnostics;
using Media_Sharing_Site.Data;
using Microsoft.AspNetCore.Mvc;
using Media_Sharing_Site.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utilities.Models;

namespace Media_Sharing_Site.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext,
        UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet("/items/{id}")]
    public async Task<IActionResult> Purchase(int id)
    {
        ViewData["Item"] = await _dbContext.Medias.FirstOrDefaultAsync(i => i.Id == id);
        return View();
    }

    [HttpPost("/buy")]
    public async Task<IActionResult> Purchase([FromForm] int id, [FromForm] int qty)
    {
        var media = await _dbContext.Medias.FirstOrDefaultAsync(i => i.Id == id);
        
        if (media.Stocks < qty)
        {
            ViewData["Success"] = false;
            goto SendView;
        }

        ViewData["Success"] = true;

        ViewData["Item"] = media;

        var currentUser = await _userManager.GetUserAsync(User);

        var item = await _dbContext.Medias.FirstOrDefaultAsync(x => x.Id == id);

        var transaction = new Transaction()
        {
            Quantity = qty,
            TransactionDate = DateTime.Now,
            MediaBought = item,
            Buyer = currentUser
        };

        media.Stocks -= qty;

        _dbContext.Update(media);

        await _dbContext.Transactions.AddAsync(transaction);

        await _dbContext.SaveChangesAsync();

        SendView:
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}