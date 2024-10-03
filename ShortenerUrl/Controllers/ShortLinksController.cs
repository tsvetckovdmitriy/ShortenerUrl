using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ShortenerUrl.Entities;
using ShortenerUrl.Interfaces;
using ShortenerUrl.Models;
using ShortenerUrl.Services;

namespace ShortenerUrl.Controllers;

public class ShortLinksController : Controller
{
    private readonly IApplicationDbContext _context;
    private readonly ShortUrlService _shortUrlService;

    public ShortLinksController(IApplicationDbContext context, ShortUrlService shortUrlService)
    {
        _shortUrlService = shortUrlService;
        _context = context;
    }

    [Route("s/{code}")]
    public async Task<IActionResult> ShortUrl(string code)
    {
        var link = await _context.ShortLinks.SingleOrDefaultAsync(x => x.Code == code);

        if (link != null)
        {
            link.CountClicks++;

            await _context.SaveChangesAsync(default);

            return Redirect(link.FullUrl);
        }
        else return NotFound();
    }

    /// <summary>
    /// Вывод списка коротких ссылок
    /// </summary>
    public async Task<IActionResult> Index()
    {
        var links = await _context.ShortLinks.ToListAsync();

        var models = links.Select(l => new ShortLinkItemViewModel
        {
            Id = l.Id,
            FullUrl = l.FullUrl,
            ShortUrl = l.ShortUrl,
            CountClicks = l.CountClicks,
            DataCreted = l.DataCreted
        });

        return View(models);
    }

    /// <summary>
    /// Вывод деталей короткой ссылки
    /// </summary>
    public async Task<IActionResult> Details(long? id)
    {
        var shortLink = await _context.ShortLinks
            .FirstOrDefaultAsync(m => m.Id == id);
        if (shortLink == null)
        {
            return NotFound();
        }

        var model = new ShortLinkItemViewModel
        {
            Id = shortLink.Id,
            FullUrl = shortLink.FullUrl,
            ShortUrl = shortLink.ShortUrl,
            CountClicks = shortLink.CountClicks,
            DataCreted = shortLink.DataCreted
        };

        return View(model);
    }

    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Создание новой короткой ссылки
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ShortLinkViewModel model)
    {
        if (ModelState.IsValid)
        {
            var code = await _shortUrlService.GenerateUniqueCode();

            var shortUrl = Url.Action("ShortUrl", "ShortLinks", new { code = code }, Request.Scheme);

            var link = new ShortLink
            {
                FullUrl = model.FullUrl,
                ShortUrl = shortUrl,
                Code = code
            };

            await _context.ShortLinks.AddAsync(link);
            await _context.SaveChangesAsync(default);

            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(long? id)
    {
        var shortLink = await _context.ShortLinks.FindAsync(id);
        if (shortLink == null)
        {
            return NotFound();
        }

        var model = new ShortLinkViewModel
        {
            Id = shortLink.Id,
            FullUrl = shortLink.FullUrl,
        };

        return View(model);
    }

    /// <summary>
    /// Обновление короткой ссылки
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, ShortLinkViewModel model)
    {
        if (ModelState.IsValid)
        {
            var link = await _context.ShortLinks.FindAsync(id);

            if (link == null)
            {
                return NotFound();
            }

            link.FullUrl = model.FullUrl;
            await _context.SaveChangesAsync(default);

            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Delete(long? id)
    {
        var shortLink = await _context.ShortLinks
            .FirstOrDefaultAsync(m => m.Id == id);
        if (shortLink == null)
        {
            return NotFound();
        }

        var model = new ShortLinkItemViewModel
        {
            Id = shortLink.Id,
            FullUrl = shortLink.FullUrl,
            ShortUrl = shortLink.ShortUrl,
            CountClicks = shortLink.CountClicks,
            DataCreted = shortLink.DataCreted
        };

        return View(model);
    }

    /// <summary>
    /// Удаление короткой ссылки
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var shortLink = await _context.ShortLinks.FindAsync(id);
        if (shortLink != null)
        {
            _context.ShortLinks.Remove(shortLink);
        }

        await _context.SaveChangesAsync(default);
        return RedirectToAction(nameof(Index));
    }
}
