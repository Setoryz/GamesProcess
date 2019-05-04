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
        public async Task<IActionResult> Search(int noOfSearchValues, int referenceValue, int? value2, int value2Week, int? value2Pos, int? value3, int value3Week, int? value3Pos, int? pageNumber, int? referencePos, int noOfWeeksToDisplay = 2)
        {
            ViewData["SearchParmAmt"] = noOfSearchValues;
            ViewData["DisplayWeeksParmAmt"] = noOfWeeksToDisplay;
            ViewData["DisplaySpaceParm"] = (noOfWeeksToDisplay * 2) + 1;
            int sizePerPage = ((noOfWeeksToDisplay * 2) + 1) * 10;

            // Reference Value
            ViewData["ReferenceParm"] = referenceValue;
            ViewData["ReferencePosParm"] = referencePos;

            // 2nd Search Value
            ViewData["Value2Parm"] = value2;
            ViewData["Value2WeekParm"] = value2Week;
            ViewData["Value2PosParm"] = value2Pos;

            // 3rd Search Value
            ViewData["Value3Parm"] = value3;
            ViewData["Value3WeekParm"] = value3Week;
            ViewData["Value3PosParm"] = value3Pos;

            referencePos = referencePos == 0 ? null : referencePos;
            value2Pos = value2Pos == 0 ? null : value2Pos;
            value3Pos = value3Pos == 0 ? null : value3Pos;

            var events = from s in _context.Events select s;

            IQueryable<Event> selectedEvents = Enumerable.Empty<Event>().AsQueryable();

            switch (noOfSearchValues)
            {
                case 1:
                    selectedEvents = AdvancedSearch.FindAsync(events, noOfWeeksToDisplay, referenceValue, referencePos);
                    break;
                case 2:
                    if (value2Pos.HasValue)
                    {
                        selectedEvents = AdvancedSearch.FindAsync(events, noOfWeeksToDisplay, referenceValue, referencePos, (int)value2, value2Week, (int)value2Pos);
                    }
                    else
                    {
                        selectedEvents = AdvancedSearch.FindAsync(events, noOfWeeksToDisplay, referenceValue, referencePos, (int)value2, value2Week);
                    }
                    break;
                case 3:
                    selectedEvents = AdvancedSearch.FindAsync(events, noOfWeeksToDisplay, referenceValue, referencePos, (int)value2, value2Week, value2Pos, (int)value3, value3Week, value3Pos);
                    break;
                default:
                    return View();
            }
            return View(await Task.Run(() => PaginatedList<Event>.Create(selectedEvents, pageNumber ?? 1, sizePerPage)));
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
