
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using day6_movie.Models;

public class MovieController : Controller
{
    private readonly day6_movieContext _context;

    public MovieController(day6_movieContext context)
    {
        _context = context;
    }

    // GET: MOVIES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Movie.ToListAsync());
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: MOVIES/Create
    public async Task<IActionResult> Create()
    {
        await LoadGenresAsync();
        return View(new Movie { ReleaseDate = DateTime.Today });
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,ReleaseDate,Price,GenreId")] Movie movie)
    {
        await ValidateGenreAsync(movie.GenreId);
        if (ModelState.IsValid)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        await LoadGenresAsync(movie.GenreId);
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        await LoadGenresAsync(movie.GenreId);
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,ReleaseDate,Price,GenreId")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (!MovieExists(movie.Id))
        {
            return NotFound();
        }

        await ValidateGenreAsync(movie.GenreId);
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        await LoadGenresAsync(movie.GenreId);
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadGenresAsync(int? selectedGenreId = null)
    {
        var genres = await _context.Genre.AsNoTracking().OrderBy(g => g.Name).ToListAsync();
        ViewData["GenreId"] = new SelectList(genres, "Id", "Name", selectedGenreId);
    }

    private async Task ValidateGenreAsync(int genreId)
    {
        if (!await _context.Genre.AnyAsync(g => g.Id == genreId))
        {
            ModelState.AddModelError(nameof(Movie.GenreId), "Please select an existing genre.");
        }
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}
