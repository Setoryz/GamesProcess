using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamesProcess.Data;
using GamesProcess.Models;
using GamesProcess.Libs;
using System.Collections;

namespace GamesProcess.Controllers
{
    public class EventsController : Controller
    {
        private readonly GameContext _context;
        public int pageSize = 50;

        public EventsController(GameContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var events = from s in _context.Events select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(s => s.Winning.Contains(int.Parse(searchString)) || s.Machine.Contains(int.Parse(searchString)));
            }

            switch (sortOrder)
            {
                case "date_desc":
                    events = events.OrderByDescending(s => s.Date);
                    break;
                default:
                    events = events.OrderBy(s => s.Date);
                    break;
            }

            return View(await PaginatedList<Event>.CreateAsync(events.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: Search
        public async Task<IActionResult> Search(int noOfSearchValues, int referenceValue, int? value2, int value2Week, int? value2Pos, int referencePos = 0)
        {
            ViewData["SearchParmAmt"] = noOfSearchValues;

            // Reference Value
            ViewData["ReferenceParm"] = referenceValue;
            ViewData["ReferencePosParm"] = referencePos;

            // 2nd Search Value
            ViewData["Value2Parm"] = value2;
            ViewData["Value2WeekParm"] = value2Week;
            ViewData["Value2PosParm"] = value2Pos;

            var events = from s in _context.Events select s;

            switch (noOfSearchValues)
            {
                case 2:
                    if (value2Pos.HasValue)
                    {
                        return View(await Task.Run(() => AdvancedSearch.FindAsync(events, referenceValue, referencePos, (int)value2, value2Week, (int)value2Pos)));
                    }
                    else
                    {
                        return View(await Task.Run(() => AdvancedSearch.FindAsync(events, referenceValue, referencePos, (int)value2, value2Week)));
                    }
                case 1:
                    return View(await Task.Run(() => AdvancedSearch.FindAsync(events, referenceValue, referencePos)));
                default:
                    return View();
            }
        }


        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .SingleOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventID,GameID,Date,WinningValues,MachineValues")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.SingleOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,GameID,Date,WinningValues,MachineValues")] Event @event)
        {
            if (id != @event.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .SingleOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.SingleOrDefaultAsync(m => m.EventID == id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }
    }
}
