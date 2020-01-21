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
        #region GET INDEX CONTROLLER
        /// <summary>
        /// This is the default controller that returns the index method when the events page is requested
        /// A list of the events paginated by size of 50 per page is returned
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="currentFilter"></param>
        /// <param name="searchString"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
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
        #endregion

        #region GET SEARCH CONTROLLER
        /// <summary>
        /// Advanced Search to use parameters provided to get paginated values from DB
        /// </summary>
        /// <param name="noOfSearchValues"></param>
        /// <param name="gameSelection"></param>
        /// <param name="referenceValue"></param>
        /// <param name="referenceLocation"></param>
        /// <param name="referencePos"></param>
        /// <param name="value2"></param>
        /// <param name="val2WeekSelect"></param>
        /// <param name="val2Location"></param>
        /// <param name="value2Week"></param>
        /// <param name="value2Pos"></param>
        /// <param name="value3"></param>
        /// <param name="val3WeekSelect"></param>
        /// <param name="val3Location"></param>
        /// <param name="value3Week"></param>
        /// <param name="value3Pos"></param>
        /// <param name="pageNumber"></param>
        /// <param name="noOfWeeksToDisplay"></param>
        /// <returns></returns>
        // GET: Search
        public async Task<IActionResult> Search(
            int noOfSearchValues, int gameSelection,
            int referenceValue, int referenceLocation, int? referencePos,
            int? value2, int val2WeekSelect, int val2Location, int value2Week, int? value2Pos,
            int? value3, int val3WeekSelect, int val3Location, int value3Week, int? value3Pos,
            int? pageNumber, int noOfWeeksToDisplay = 5)
        {
            // Search Results Data
            ViewBag.SearchParmAmt = noOfSearchValues;
            ViewBag.DisplayWeeksParmAmt = noOfWeeksToDisplay;
            ViewBag.GameSelection = gameSelection;
            ViewBag.DisplaySpaceParm = (noOfWeeksToDisplay * 2) + 1;
            int sizePerPage = ((noOfWeeksToDisplay * 2) + 1) * 10;

            // Reference Value
            ViewBag.ReferenceValue = referenceValue;
            ViewBag.ReferenceLocation = referenceLocation; //
            ViewBag.ReferencePos = referencePos;

            // 2nd Search Value
            ViewBag.Value2 = value2;
            ViewBag.Val2WeekSelect = val2WeekSelect; //
            ViewBag.Val2Location = val2Location; // F
            ViewBag.Value2Week = value2Week;
            ViewBag.Value2Pos = value2Pos;
            ViewBag.Value2WeekAbs = value2Week >= 0 ? value2Week : ((noOfWeeksToDisplay * 2) + 1 + value2Week);

            // 3rd Search Value
            ViewBag.Value3 = value3;
            ViewBag.Val3WeekSelect = val3WeekSelect;
            ViewBag.Val3Location = val3Location;
            ViewBag.Value3Week = value3Week;
            ViewBag.Value3Pos = value3Pos;
            ViewBag.Value3WeekAbs = value3Week >= 0 ? value3Week : ((noOfWeeksToDisplay * 2) + 1 + value3Week);


            referencePos = referencePos == 0 ? null : referencePos;
            value2Pos = value2Pos == 0 ? null : value2Pos;
            value3Pos = value3Pos == 0 ? null : value3Pos;

            var events = from s in _context.Events select s;
            switch (gameSelection)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    events = from s in _context.Events
                             .Where(s => s.GameID == gameSelection)
                             select s;
                    break;
                default:
                    break;
            }
            //var selectedEvents = from s in _context.Events select s;

            //IQueryable<Event> selectedEvents = Enumerable.Empty<Event>().AsQueryable();
            List<Event> selectedEvents = new List<Event>();

            switch (noOfSearchValues)
            {
                case 1:
                    selectedEvents = AdvancedSearch.FindAsync(events, noOfWeeksToDisplay, referenceValue, referenceLocation, referencePos).ToList();
                    break;
                case 2:
                    if (val2WeekSelect == 2)
                    {
                        for (int i = -2; i <= 2; i++)
                        {
                            if (i != 0) selectedEvents.AddRange(AdvancedSearch.FindAsync(events, noOfWeeksToDisplay, referenceValue, referenceLocation, referencePos, (int)value2, i, val2Location, value2Pos).ToList());
                        }
                    }
                    else
                    {
                        selectedEvents = AdvancedSearch.FindAsync(events, noOfWeeksToDisplay, referenceValue, referenceLocation, referencePos, (int)value2, value2Week, val2Location, value2Pos).ToList();
                    }
                    break;
                case 3:
                    selectedEvents = AdvancedSearch.FindAsync(events, noOfWeeksToDisplay, referenceValue, referenceLocation, referencePos, (int)value2, value2Week, val2Location, value2Pos, (int)value3, value3Week, val3Location, value3Pos).ToList();
                    break;
                default:
                    return View();
            }
            return View(await Task.Run(() => PaginatedList<Event>.Create(selectedEvents.AsQueryable(), pageNumber ?? 1, sizePerPage)));
        }
        #endregion


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
