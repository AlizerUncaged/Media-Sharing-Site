using Media_Sharing_Site.Data;
using Media_Sharing_Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utilities.Models;

namespace Media_Sharing_Site.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly ApplicationDbContext _dbContext;

    public AdminController(ILogger<AdminController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost("/admin/add")]
    public async Task<IActionResult> AddMediaPost([FromForm] string name, [FromForm] string author,
        [FromForm] double price, [FromForm] int mediaType, [FromForm] long? targetId, [FromForm] IFormFile? file,
        [FromForm] IFormFile? thumbnail)
    {
        // Upload Media File.
        string? relativeFilename = null;
        string? relativeThumbnailFilename = null;
        var fileName = StringExtensions.RandomString(64);
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files");
        if (file is { })
        {
            // Create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            // Get file extension
            fileName += Path.GetExtension(file.FileName);

            string fileNameWithPath = Path.Combine(path, fileName);
            await using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await file.CopyToAsync(stream);

            relativeFilename = Path.Combine("files", fileName);
        }

        if (thumbnail is { })
        {
            // Upload Thumbnail Image.
            var thumbnailFilename = fileName + ".png";
            var fileNameWithPath = Path.Combine(path, thumbnailFilename);
            await using var stream2 = new FileStream(fileNameWithPath, FileMode.Create);
            await thumbnail.CopyToAsync(stream2);
            relativeThumbnailFilename = Path.Combine("files", thumbnailFilename);
        }

        if (targetId is { })
        {
            // Target Id is not null, update.
            var currentMedia = await _dbContext.Medias.FirstOrDefaultAsync(x => x.Id == targetId);
            if (currentMedia is null)
                return Content("Target media does not exist.");

            currentMedia.Name = name;
            currentMedia.Author = author;
            currentMedia.Price = price;
            currentMedia.MediaType = (MediaType)mediaType;
            
            if (relativeFilename is { })
            {
                currentMedia.FileName = relativeFilename;
            }

            if (relativeThumbnailFilename is { })
            {
                currentMedia.Thumbnail = relativeThumbnailFilename;
            }

            _dbContext.Update(currentMedia);
        }
        else
        {
            var f = new Media()
            {
                Name = name, Author = author, Price = price, MediaType = (MediaType)mediaType,
            };

            if (relativeFilename is { })
            {
                f.FileName = relativeFilename;
            }

            if (relativeThumbnailFilename is { })
            {
                f.Thumbnail = relativeThumbnailFilename;
            }

            await _dbContext.Medias.AddAsync(f);
        }


        await _dbContext.SaveChangesAsync();
        return Redirect("/admin/add?success=true");
    }

    [HttpGet("/admin/transactions")]
    public async Task<IActionResult> ViewTransactions()
    {
        return View();
    }
    
    [HttpGet("/admin/add")]
    public async Task<IActionResult> AddMedia([FromQuery] bool? success, [FromQuery] long? targetId)
    {
        ViewData["TargetId"] = targetId;
        ViewData["Success"] = success ?? false;
        return View();
    }
}