using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GamesProcess.Data;
using Microsoft.EntityFrameworkCore;
using GamesProcess.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GamesProcess.Controllers
{
    public class GamesController : Controller
    {
        private GameContext _context;
        public GamesController(GameContext gameContext)
        {
            _context = gameContext;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View(await _context.Games.ToListAsync());
        }

        // GET: /Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .Include(s => s.Events)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Game game)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(game);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes, " + "Try again, and if the problem persists , " + "see your system administrawor.");
            }
            return View(game);
        }

        //[ActionName("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameToUpdate = await _context.Games.SingleOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Game>(gameToUpdate, "", s => s.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes, " + "Try again, and if the problem persists , " + "see your system administrawor.");
                }
            }
            return View(gameToUpdate);
        }

        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (game == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again, and if the problem persists " + "see your system administrator.";
            }

            return View(game);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.AsNoTracking().SingleOrDefaultAsync(m => m.ID == id);

            if (game == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }

}
