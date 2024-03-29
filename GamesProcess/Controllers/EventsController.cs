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
        public async Task<IActionResult> Search(
            int? noOfSearchValues, int gameSelection, int groupSelection,
            int referenceValue, int referenceLocation, int? referencePos,
            int? value2, int val2WeekSelect, int val2Location, int value2Week, int? value2Pos,
            int? value3, int val3WeekSelect, int val3Location, int value3Week, int? value3Pos,
            int? pageNumber, int noOfWeeksToDisplay = 2)
        {
            // Games Name List
            List<Game> gamesList = new List<Game>();
            gamesList = (from games in _context.Games select games).ToList(); //collect data from game context into list
            gamesList.Insert(0, new Game { ID = 0, Name = "Select Groups" }); // add select option to list

            List<GamesClass> gamesGroups = new List<GamesClass>();
            gamesGroups = (from gamesClass in _context.GamesClass select gamesClass).ToList();
            gamesGroups.Insert(0, new GamesClass { ID = 0, Name = "Select All" });

            ViewBag.ListOfGames = gamesList;
            ViewBag.ListOfGroups = gamesGroups;

            // Search Results Data
            ViewBag.SearchParmAmt = noOfSearchValues;
            ViewBag.DisplayWeeksParmAmt = noOfWeeksToDisplay;
            ViewBag.GameSelection = gameSelection;
            ViewBag.GroupSelection = groupSelection;
            ViewBag.DisplaySpaceParm = (noOfWeeksToDisplay * 2) + 1;
            int sizePerPage = ((noOfWeeksToDisplay * 2) + 1) * 10;

            // Reference Value
            ViewBag.ReferenceValue = referenceValue;
            ViewBag.ReferenceLocation = referenceLocation; //
            ViewBag.ReferencePos = referencePos;

            // 2nd Search Value
            ViewBag.Value2 = value2;
            ViewBag.Val2WeekSelect = val2WeekSelect; //
            ViewBag.Val2Location = val2Location; //
            ViewBag.Value2Week = value2Week;
            ViewBag.Value2Pos = value2Pos;
            ViewBag.Value2WeekAbs = value2Week >= 0 ? value2Week : ((noOfWeeksToDisplay * 3) + 1 + value2Week);

            // 3rd Search Value
            ViewBag.Value3 = value3;
            ViewBag.Val3WeekSelect = val3WeekSelect;
            ViewBag.Val3Location = val3Location;
            ViewBag.Value3Week = value3Week;
            ViewBag.Value3Pos = value3Pos;
            ViewBag.Value3WeekAbs = value3Week >= 0 ? value3Week : ((noOfWeeksToDisplay * 3) + 1 + value3Week);

            if (noOfSearchValues == null)
            {
                return View();

            }


            referencePos = referencePos == 0 ? null : referencePos;
            value2Pos = value2Pos == 0 ? null : value2Pos;
            value3Pos = value3Pos == 0 ? null : value3Pos;

            //var events = from s in _context.Events select s;
            int[] groupGamesToSearchFrom = (gameSelection == 0) ? (from games in _context.Games.Where(s => s.GamesClassID == groupSelection) select games.ID).ToArray() : null;

            var events = (gameSelection > 0 && gameSelection < gamesList.Count) ? from s in _context.Events.Where(s => s.GameID == gameSelection) select s : ((groupGamesToSearchFrom == null) ? from s in _context.Events select s : from s in _context.Events.Where(s => groupGamesToSearchFrom.Contains(s.GameID)) select s );

            List<Event> selectedEvents = new List<Event>();

            List<AdvancedSearchResult> results = new List<AdvancedSearchResult>();

            switch (noOfSearchValues)
            {
                case 1:
                    results = await Task.Run(() => AdvSearch.FindResults(_context, noOfWeeksToDisplay, referenceValue, referenceLocation, referencePos, gameSelection, groupGamesToSearchFrom).ToList());

                    selectedEvents = AdvancedSearch.FindAsync(events, noOfWeeksToDisplay, referenceValue, referenceLocation, referencePos).ToList();
                    break;
                case 2:
                    results = await Task.Run(() => AdvSearch.FindResults(_context, noOfWeeksToDisplay, referenceValue, referenceLocation, referencePos, gameSelection, groupGamesToSearchFrom, (int)value2, val2WeekSelect, value2Week, val2Location, value2Pos).ToList());

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
                    results = await Task.Run(() => AdvSearch.FindResults(_context, noOfWeeksToDisplay, referenceValue, referenceLocation, referencePos, gameSelection, groupGamesToSearchFrom, (int)value2, val2WeekSelect, value2Week, val2Location, value2Pos, (int)value3, val3WeekSelect, value3Week, val3Location, value3Pos).ToList());

                    selectedEvents = AdvancedSearch.FindAsync(events, noOfWeeksToDisplay, referenceValue, referenceLocation, referencePos, (int)value2, value2Week, val2Location, value2Pos, (int)value3, value3Week, val3Location, value3Pos).ToList();
                    break;
                default:
                    return View();
            }
            return View(await Task.Run(() => PaginatedList<Event>.Create(selectedEvents.AsQueryable(), pageNumber ?? 1, sizePerPage, gamesList, gamesGroups, gameSelection, groupSelection)));
        }


        #region GET: Events/Details
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
        #endregion

        #region GET: Events/Create
        // GET: Events/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region POST: Events/Create
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
        #endregion

        #region GET: Events/Edit
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
        #endregion

        #region POST: Events/Edit
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
        #endregion

        #region GET: Events/Delete
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
        #endregion

        #region POST: Events/Delete
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
        #endregion
    }
}
