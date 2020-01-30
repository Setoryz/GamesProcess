using GamesProcess.Data;
using GamesProcess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProcess.Libs
{
    public class AdvSearch
    {
        //private static readonly GameContext _context;

        #region Find Search Results when only one number is provided
        /// <summary>
        /// Method to find search result when only one number is provided. 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="noOfWeeksToDisplay"></param>
        /// <param name="referenceValue"></param>
        /// <param name="referenceLocation"></param>
        /// <param name="referencePos"></param>
        /// <param name="gameSelection"></param>
        /// <param name="groupGamesToSearchFrom"></param>
        /// <returns></returns>
        internal static List<AdvancedSearchResult> FindResults(GameContext context, int noOfWeeksToDisplay, int referenceValue, int referenceLocation, int? referencePos, int gameSelection, int[] groupGamesToSearchFrom)
        {
            int noOfGamesInDB = (from games in context.Games select games.ID).ToList().Count;
            int[] idToSelectFrom = RefIdToSearchFrom(context, referenceValue, referenceLocation, referencePos, gameSelection, groupGamesToSearchFrom, noOfGamesInDB);
            List<AdvancedSearchResult> results = new List<AdvancedSearchResult>();


            int asrID = 0;
            foreach (int eventID in idToSelectFrom)
            {
                int gameID = context.Events.Where(s => s.EventID == eventID).FirstOrDefault().GameID;

                int totalGameEventCount = context.Games
                    .Include(s => s.Events)
                    .Where(s => s.ID == gameID).First().Events.Count;
                int firstGameEventInDB = context.Events.Where(s => s.GameID == gameID).First().EventID;

                results.Add(
                    new AdvancedSearchResult
                    {
                        ID = asrID,
                        Events = (context.Events.Skip(GetFirstExistingPrevID(eventID, noOfWeeksToDisplay) - 1).Take(AmountOfEventsToTake(eventID, noOfWeeksToDisplay, (firstGameEventInDB + totalGameEventCount) - 1)).ToList()),
                        ReferenceEventID = eventID
                    });
                asrID++;
            }
            return results;
        }
        #endregion

        /// <summary>
        /// Method to find search results when two reference values are provided
        /// </summary>
        /// <param name="context"></param>
        /// <param name="noOfWeeksToDisplay"></param>
        /// <param name="referenceValue"></param>
        /// <param name="referenceLocation"></param>
        /// <param name="referencePos"></param>
        /// <param name="gameSelection"></param>
        /// <param name="groupGamesToSearchFrom"></param>
        /// <param name="value2"></param>
        /// <param name="val2WeekSelect"></param>
        /// <param name="value2Week"></param>
        /// <param name="val2Location"></param>
        /// <param name="value2Pos"></param>
        /// <returns></returns>
        internal static List<AdvancedSearchResult> FindResults(GameContext context, int noOfWeeksToDisplay, int referenceValue, int referenceLocation, int? referencePos, int gameSelection, int[] groupGamesToSearchFrom, int value2, int val2WeekSelect, int value2Week, int val2Location, int? value2Pos)
        {
            int noOfGamesInDB = (from games in context.Games select games.ID).ToList().Count;
            int[] idToSelectFrom = RefIdToSearchFrom(context, referenceValue, referenceLocation, referencePos, gameSelection, groupGamesToSearchFrom, noOfGamesInDB);
            List<AdvancedSearchResult> results = new List<AdvancedSearchResult>();

            int asrID = 0;

            if (val2WeekSelect == 2)
            {
                // when we are using range of weeks for value 2

                if (value2Pos == 0 || !(value2Pos.HasValue))
                {
                    switch (val2Location)
                    {
                        case 1:
                            foreach (int eventID in idToSelectFrom)
                            {
                                int gameID = context.Events.Where(s => s.EventID == eventID).FirstOrDefault().GameID;
                                int totalGameEventCount = context.Games
                                    .Include(s => s.Events)
                                    .Where(s => s.ID == gameID).First().Events.Count;
                                int firstGameEventInDB = context.Events.Where(s => s.GameID == gameID).First().EventID;

                                for (int week2Loop = -value2Week; week2Loop <= value2Week; week2Loop++)
                                {
                                    if ((((eventID + week2Loop) > 0) && ((eventID + week2Loop) >= (firstGameEventInDB)) && (eventID + week2Loop) < (firstGameEventInDB + totalGameEventCount)) && context.Events.Skip(eventID + week2Loop - 1).First().Winning.Contains(value2))
                                    {
                                        results.Add(
                                                new AdvancedSearchResult
                                                {
                                                    ID = asrID,
                                                    Events = (context.Events.Skip(GetFirstExistingPrevID(eventID, noOfWeeksToDisplay) - 1).Take(AmountOfEventsToTake(eventID, noOfWeeksToDisplay, (firstGameEventInDB + totalGameEventCount) - 1)).ToList()),
                                                    ReferenceEventID = eventID,
                                                    Value2EventID = context.Events.Skip(eventID + week2Loop - 1).First().EventID
                                                });
                                        asrID++;
                                    }

                                }
                            }
                            break;
                        case 2:
                            foreach (int eventID in idToSelectFrom)
                            {
                                int gameID = context.Events.Where(s => s.EventID == eventID).FirstOrDefault().GameID;
                                int totalGameEventCount = context.Games
                                    .Include(s => s.Events)
                                    .Where(s => s.ID == gameID).First().Events.Count;
                                int firstGameEventInDB = context.Events.Where(s => s.GameID == gameID).First().EventID;

                                for (int week2Loop = -value2Week; week2Loop <= value2Week; week2Loop++)
                                {
                                    if ((((eventID + week2Loop) > 0) && (eventID + week2Loop) >= (firstGameEventInDB) && ((eventID + week2Loop) < (firstGameEventInDB + totalGameEventCount))) && context.Events.Skip(eventID + week2Loop - 1).First().Machine.Contains(value2))
                                    {
                                        results.Add(
                                                new AdvancedSearchResult
                                                {
                                                    ID = asrID,
                                                    Events = (context.Events.Skip(GetFirstExistingPrevID(eventID, noOfWeeksToDisplay) - 1).Take(AmountOfEventsToTake(eventID, noOfWeeksToDisplay, (firstGameEventInDB + totalGameEventCount) - 1)).ToList()),
                                                    ReferenceEventID = eventID,
                                                    Value2EventID = context.Events.Skip(eventID + week2Loop - 1).First().EventID
                                                });
                                        asrID++;
                                    }
                                }
                            }
                            break;
                        default:
                            foreach (int eventID in idToSelectFrom)
                            {
                                int gameID = context.Events.Where(s => s.EventID == eventID).FirstOrDefault().GameID;
                                int totalGameEventCount = context.Games
                                    .Include(s => s.Events)
                                    .Where(s => s.ID == gameID).First().Events.Count;
                                int firstGameEventInDB = context.Events.Where(s => s.GameID == gameID).First().EventID;

                                for (int week2Loop = -value2Week; week2Loop <= value2Week; week2Loop++)
                                {
                                    if ((((eventID + week2Loop) > 0) && (eventID + week2Loop) >= (firstGameEventInDB) && ((eventID + week2Loop) < (firstGameEventInDB + totalGameEventCount))) && (context.Events.Skip(eventID + week2Loop - 1).First().Machine.Contains(value2) || context.Events.Skip(eventID + week2Loop - 1).First().Winning.Contains(value2)))
                                    {
                                        results.Add(
                                                new AdvancedSearchResult
                                                {
                                                    ID = asrID,
                                                    Events = (context.Events.Skip(GetFirstExistingPrevID(eventID, noOfWeeksToDisplay) - 1).Take(AmountOfEventsToTake(eventID, noOfWeeksToDisplay, (firstGameEventInDB + totalGameEventCount) - 1)).ToList()),
                                                    ReferenceEventID = eventID,
                                                    Value2EventID = context.Events.Skip(eventID + week2Loop - 1).First().EventID
                                                });
                                        asrID++;
                                    }
                                }
                            }
                            break;
                    }
                }
                else
                {
                    switch (val2Location)
                    {
                        case 1:
                            foreach (int eventID in idToSelectFrom)
                            {
                                int gameID = context.Events.Where(s => s.EventID == eventID).FirstOrDefault().GameID;
                                int totalGameEventCount = context.Games
                                    .Include(s => s.Events)
                                    .Where(s => s.ID == gameID).First().Events.Count;
                                int firstGameEventInDB = context.Events.Where(s => s.GameID == gameID).First().EventID;

                                for (int week2Loop = -value2Week; week2Loop <= value2Week; week2Loop++)
                                {
                                    if ((((eventID + week2Loop) > 0) && ((eventID + week2Loop) < (firstGameEventInDB + totalGameEventCount))) && (context.Events.Skip(eventID + week2Loop - 1).First().Winning[(int)value2Pos - 1]) == value2)
                                    {
                                        results.Add(
                                                new AdvancedSearchResult
                                                {
                                                    ID = asrID,
                                                    Events = (context.Events.Skip(GetFirstExistingPrevID(eventID, noOfWeeksToDisplay) - 1).Take(AmountOfEventsToTake(eventID, noOfWeeksToDisplay, (firstGameEventInDB + totalGameEventCount) - 1)).ToList()),
                                                    ReferenceEventID = eventID,
                                                    Value2EventID = context.Events.Skip(eventID + week2Loop - 1).First().EventID
                                                });
                                        asrID++;
                                    }
                                }
                            }
                            break;
                        case 2:
                            foreach (int eventID in idToSelectFrom)
                            {
                                int gameID = context.Events.Where(s => s.EventID == eventID).FirstOrDefault().GameID;
                                int totalGameEventCount = context.Games
                                    .Include(s => s.Events)
                                    .Where(s => s.ID == gameID).First().Events.Count;
                                int firstGameEventInDB = context.Events.Where(s => s.GameID == gameID).First().EventID;

                                for (int week2Loop = -value2Week; week2Loop <= value2Week; week2Loop++)
                                {
                                    if ((((eventID + week2Loop) > 0) && ((eventID + week2Loop) < (firstGameEventInDB + totalGameEventCount))) && context.Events.Skip(eventID + week2Loop - 1).First().Machine[(int)value2Pos - 1] == value2)
                                    {
                                        results.Add(
                                                new AdvancedSearchResult
                                                {
                                                    ID = asrID,
                                                    Events = (context.Events.Skip(GetFirstExistingPrevID(eventID, noOfWeeksToDisplay) - 1).Take(AmountOfEventsToTake(eventID, noOfWeeksToDisplay, (firstGameEventInDB + totalGameEventCount) - 1)).ToList()),
                                                    ReferenceEventID = eventID,
                                                    Value2EventID = context.Events.Skip(eventID + week2Loop - 1).First().EventID
                                                });
                                        asrID++;
                                    }
                                }
                            }
                            break;
                        default:
                            foreach (int eventID in idToSelectFrom)
                            {
                                int gameID = context.Events.Where(s => s.EventID == eventID).FirstOrDefault().GameID;
                                int totalGameEventCount = context.Games
                                    .Include(s => s.Events)
                                    .Where(s => s.ID == gameID).First().Events.Count;
                                int firstGameEventInDB = context.Events.Where(s => s.GameID == gameID).First().EventID;

                                for (int week2Loop = -value2Week; week2Loop <= value2Week; week2Loop++)
                                {
                                    if ((((eventID + week2Loop) > 0) && ((eventID + week2Loop) < (firstGameEventInDB + totalGameEventCount))) && (context.Events.Skip(eventID + week2Loop - 1).First().Machine[(int)value2Pos - 1] == value2 || context.Events.Skip(eventID + week2Loop - 1).First().Winning[(int)value2Pos - 1] == value2))
                                    {
                                        results.Add(
                                                new AdvancedSearchResult
                                                {
                                                    ID = asrID,
                                                    Events = (context.Events.Skip(GetFirstExistingPrevID(eventID, noOfWeeksToDisplay) - 1).Take(AmountOfEventsToTake(eventID, noOfWeeksToDisplay, (firstGameEventInDB + totalGameEventCount) - 1)).ToList()),
                                                    ReferenceEventID = eventID,
                                                    Value2EventID = context.Events.Skip(eventID + week2Loop - 1).First().EventID
                                                });
                                        asrID++;
                                    }
                                }
                            }
                            break;
                    }
                }

            }
            else
            {
                // when we are using specified week for value 2
                if (value2Pos == 0 || !(value2Pos.HasValue))
                {
                    switch (val2Location)
                    {
                        case 1:
                            foreach (int eventID in idToSelectFrom)
                            {
                                int gameID = context.Events.Where(s => s.EventID == eventID).FirstOrDefault().GameID;
                                int totalGameEventCount = context.Games
                                    .Include(s => s.Events)
                                    .Where(s => s.ID == gameID).First().Events.Count;
                                int firstGameEventInDB = context.Events.Where(s => s.GameID == gameID).First().EventID;

                                if ((((eventID + value2Week) > 0) && ((eventID + value2Week) < (firstGameEventInDB + totalGameEventCount))) && context.Events.Skip(eventID + value2Week - 1).First().Winning.Contains(value2))
                                {

                                    results.Add(
                                            new AdvancedSearchResult
                                            {
                                                ID = asrID,
                                                Events = (context.Events.Skip(GetFirstExistingPrevID(eventID, noOfWeeksToDisplay) - 1).Take(AmountOfEventsToTake(eventID, noOfWeeksToDisplay, (firstGameEventInDB + totalGameEventCount) - 1)).ToList()),
                                                ReferenceEventID = eventID,
                                                Value2EventID = context.Events.Skip(eventID + value2Week - 1).First().EventID
                                            });
                                    asrID++;
                                }
                            }
                            break;
                        case 2:
                            foreach (int eventID in idToSelectFrom)
                            {
                                int gameID = context.Events.Where(s => s.EventID == eventID).FirstOrDefault().GameID;
                                int totalGameEventCount = context.Games
                                    .Include(s => s.Events)
                                    .Where(s => s.ID == gameID).First().Events.Count;
                                int firstGameEventInDB = context.Events.Where(s => s.GameID == gameID).First().EventID;

                                if ((((eventID + value2Week) > 0) && ((eventID + value2Week) < (firstGameEventInDB + totalGameEventCount))) && context.Events.Skip(eventID + value2Week - 1).First().Machine.Contains(value2))
                                {
                                    results.Add(
                                            new AdvancedSearchResult
                                            {
                                                ID = asrID,
                                                Events = (context.Events.Skip(GetFirstExistingPrevID(eventID, noOfWeeksToDisplay) - 1).Take(AmountOfEventsToTake(eventID, noOfWeeksToDisplay, (firstGameEventInDB + totalGameEventCount) - 1)).ToList()),
                                                ReferenceEventID = eventID,
                                                Value2EventID = context.Events.Skip(eventID + value2Week - 1).First().EventID
                                            });
                                    asrID++;
                                }
                            }
                            break;
                        default:
                            foreach (int eventID in idToSelectFrom)
                            {
                                int gameID = context.Events.Where(s => s.EventID == eventID).FirstOrDefault().GameID;
                                int totalGameEventCount = context.Games
                                    .Include(s => s.Events)
                                    .Where(s => s.ID == gameID).First().Events.Count;
                                int firstGameEventInDB = context.Events.Where(s => s.GameID == gameID).First().EventID;

                                if ((((eventID + value2Week) > 0) && ((eventID + value2Week) < (firstGameEventInDB + totalGameEventCount))) && (context.Events.Skip(eventID + value2Week - 1).First().Machine.Contains(value2) || context.Events.Skip(eventID + value2Week - 1).First().Winning.Contains(value2)))
                                {
                                    results.Add(
                                            new AdvancedSearchResult
                                            {
                                                ID = asrID,
                                                Events = (context.Events.Skip(GetFirstExistingPrevID(eventID, noOfWeeksToDisplay) - 1).Take(AmountOfEventsToTake(eventID, noOfWeeksToDisplay, (firstGameEventInDB + totalGameEventCount) - 1)).ToList()),
                                                ReferenceEventID = eventID,
                                                Value2EventID = context.Events.Skip(eventID + value2Week - 1).First().EventID
                                            });
                                    asrID++;
                                }
                            }
                            break;
                    }
                }
                else
                {
                    switch (val2Location)
                    {
                        case 1:
                            foreach (int eventID in idToSelectFrom)
                            {
                                int gameID = context.Events.Where(s => s.EventID == eventID).FirstOrDefault().GameID;
                                int totalGameEventCount = context.Games
                                    .Include(s => s.Events)
                                    .Where(s => s.ID == gameID).First().Events.Count;
                                int firstGameEventInDB = context.Events.Where(s => s.GameID == gameID).First().EventID;

                                if ((((eventID + value2Week) > 0) && ((eventID + value2Week) < (firstGameEventInDB + totalGameEventCount))) && context.Events.Skip(eventID + value2Week - 1).First().Winning[(int)value2Pos - 1] == value2)
                                {
                                    results.Add(
                                            new AdvancedSearchResult
                                            {
                                                ID = asrID,
                                                Events = (context.Events.Skip(GetFirstExistingPrevID(eventID, noOfWeeksToDisplay) - 1).Take(AmountOfEventsToTake(eventID, noOfWeeksToDisplay, (firstGameEventInDB + totalGameEventCount) - 1)).ToList()),
                                                ReferenceEventID = eventID,
                                                Value2EventID = context.Events.Skip(eventID + value2Week - 1).First().EventID
                                            });
                                    asrID++;
                                }
                            }
                            break;
                        case 2:
                            foreach (int eventID in idToSelectFrom)
                            {
                                int gameID = context.Events.Where(s => s.EventID == eventID).FirstOrDefault().GameID;
                                int totalGameEventCount = context.Games
                                    .Include(s => s.Events)
                                    .Where(s => s.ID == gameID).First().Events.Count;
                                int firstGameEventInDB = context.Events.Where(s => s.GameID == gameID).First().EventID;

                                if ((((eventID + value2Week) > 0) && ((eventID + value2Week) < (firstGameEventInDB + totalGameEventCount))) && context.Events.Skip(eventID + value2Week - 1).First().Machine[(int)value2Pos - 1] == value2)
                                {
                                    results.Add(
                                            new AdvancedSearchResult
                                            {
                                                ID = asrID,
                                                Events = (context.Events.Skip(GetFirstExistingPrevID(eventID, noOfWeeksToDisplay) - 1).Take(AmountOfEventsToTake(eventID, noOfWeeksToDisplay, (firstGameEventInDB + totalGameEventCount) - 1)).ToList()),
                                                ReferenceEventID = eventID,
                                                Value2EventID = context.Events.Skip(eventID + value2Week - 1).First().EventID
                                            });
                                    asrID++;
                                }
                            }
                            break;
                        default:
                            foreach (int eventID in idToSelectFrom)
                            {
                                int gameID = context.Events.Where(s => s.EventID == eventID).FirstOrDefault().GameID;
                                int totalGameEventCount = context.Games
                                    .Include(s => s.Events)
                                    .Where(s => s.ID == gameID).First().Events.Count;
                                int firstGameEventInDB = context.Events.Where(s => s.GameID == gameID).First().EventID;

                                if ((((eventID + value2Week) > 0) && ((eventID + value2Week) < (firstGameEventInDB + totalGameEventCount))) && (context.Events.Skip(eventID + value2Week - 1).First().Machine[(int)value2Pos - 1] == value2 || context.Events.Skip(eventID + value2Week - 1).First().Winning[(int)value2Pos - 1] == value2))
                                {
                                    results.Add(
                                            new AdvancedSearchResult
                                            {
                                                ID = asrID,
                                                Events = (context.Events.Skip(GetFirstExistingPrevID(eventID, noOfWeeksToDisplay) - 1).Take(AmountOfEventsToTake(eventID, noOfWeeksToDisplay, (firstGameEventInDB + totalGameEventCount) - 1)).ToList()),
                                                ReferenceEventID = eventID,
                                                Value2EventID = context.Events.Skip(eventID + value2Week - 1).First().EventID
                                            });
                                    asrID++;
                                }
                            }
                            break;
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Method to find search results when three reference values are provided
        /// </summary>
        /// <param name="context"></param>
        /// <param name="noOfWeeksToDisplay"></param>
        /// <param name="referenceValue"></param>
        /// <param name="referenceLocation"></param>
        /// <param name="referencePos"></param>
        /// <param name="gameSelection"></param>
        /// <param name="groupGamesToSearchFrom"></param>
        /// <param name="value2"></param>
        /// <param name="val2WeekSelect"></param>
        /// <param name="value2Week"></param>
        /// <param name="val2Location"></param>
        /// <param name="value2Pos"></param>
        /// <param name="value3"></param>
        /// <param name="val3WeekSelect"></param>
        /// <param name="value3Week"></param>
        /// <param name="val3Location"></param>
        /// <param name="value3Pos"></param>
        /// <returns></returns>
        internal static List<AdvancedSearchResult> FindResults(GameContext context, int noOfWeeksToDisplay, int referenceValue, int referenceLocation, int? referencePos, int gameSelection, int[] groupGamesToSearchFrom, int value2, int val2WeekSelect, int value2Week, int val2Location, int? value2Pos, int value3, int val3WeekSelect, int value3Week, int val3Location, int? value3Pos)
        {
            int noOfGamesInDB = (from games in context.Games select games.ID).ToList().Count;
            int[] idToSelectFrom = RefIdToSearchFrom(context, referenceValue, referenceLocation, referencePos, gameSelection, groupGamesToSearchFrom, noOfGamesInDB);
            List<AdvancedSearchResult> results = new List<AdvancedSearchResult>();

            int asrID = 0;

            List<AdvancedSearchResult> resultsFor2Values = FindResults(context, noOfWeeksToDisplay, referenceValue, referenceLocation, referencePos, gameSelection, groupGamesToSearchFrom, (int)value2, val2WeekSelect, value2Week, val2Location, value2Pos).ToList();

            if(val3WeekSelect == 2)
            {
                if (value3Pos == 0 || !(value3Pos.HasValue))
                {
                    switch (val3Location)
                    {
                        case 1:
                            foreach (AdvancedSearchResult resultFor2Values in resultsFor2Values)
                            {
                                for (int week3Loop = -value3Week; week3Loop <= value3Week; week3Loop++)
                                {
                                    int referenceEventID = resultFor2Values.ReferenceEventID;
                                    List<Event> events = resultFor2Values.Events;
                                    if (((referenceEventID + week3Loop) > 0) && ((referenceEventID + week3Loop) >= (events.Where(s => s.EventID == referenceEventID).First().EventID) - events.First().EventID) )
                                    {
                                        asrID++;
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {

                }
            }
            else
            {
                if (value3Pos == 0 || !(value2Pos.HasValue))
                {

                }
                else
                {

                }
            }

            throw new NotImplementedException();
        }


        internal static int GetFirstExistingPrevID(int EventID, int noOfWeeksToDisplay)
        {
            return ((EventID - noOfWeeksToDisplay) < 1) ? GetFirstExistingPrevID(EventID + 1, noOfWeeksToDisplay) : (EventID - noOfWeeksToDisplay);
        }

        internal static int GetLastExistingNextID(int EventID, int noOfWeeksToDisplay, int totalEventsCount)
        {
            return ((EventID + noOfWeeksToDisplay) > totalEventsCount) ? GetLastExistingNextID(EventID - 1, noOfWeeksToDisplay, totalEventsCount) : (EventID + noOfWeeksToDisplay);
        }


        internal static int AmountOfEventsToTake(int EventID, int noOfWeeksToDisplay, int totalEventsCount)
        {
            int firstEventID = GetFirstExistingPrevID(EventID, noOfWeeksToDisplay);
            int lastEventID = GetLastExistingNextID(EventID, noOfWeeksToDisplay, totalEventsCount);
            return ((EventID - firstEventID) + (lastEventID - EventID) + 1);
        }


        #region RefIdToSearchFrom: Get IDs of results containing the reference search value
        /// <summary>
        /// Get IDs of results containing the reference search value
        /// </summary>
        /// <param name="context"></param>
        /// <param name="referenceValue"></param>
        /// <param name="referenceLocation"></param>
        /// <param name="referencePos"></param>
        /// <param name="gameSelection"></param>
        /// <param name="groupGamesToSearchFrom"></param>
        /// <param name="noOfGamesInDB"></param>
        /// <returns></returns>
        internal static int[] RefIdToSearchFrom(GameContext context, int referenceValue, int referenceLocation, int? referencePos, int gameSelection, int[] groupGamesToSearchFrom, int noOfGamesInDB)
        {
            if (!(referencePos.HasValue) || referencePos == 0)
            {

                switch (referenceLocation)
                {
                    case 1:
                        return
                           (gameSelection > 0 && gameSelection <= noOfGamesInDB) ?
                           (from s in context.Events.Where(s => s.GameID == gameSelection && (s.Winning.Contains(referenceValue))) select s.EventID).ToArray()
                           : ((groupGamesToSearchFrom.Count() > 0) ? (from s in context.Events.Where(s => groupGamesToSearchFrom.Contains(s.GameID) && (s.Winning.Contains(referenceValue))) select s.EventID).ToArray() : (from s in context.Events.Where(s => s.Winning.Contains(referenceValue)) select s.EventID).ToArray());
                    case 2:
                        return
                           (gameSelection > 0 && gameSelection <= noOfGamesInDB) ?
                           (from s in context.Events.Where(s => s.GameID == gameSelection && (s.Machine.Contains(referenceValue))) select s.EventID).ToArray()
                           : ((groupGamesToSearchFrom.Count() > 0) ? (from s in context.Events.Where(s => groupGamesToSearchFrom.Contains(s.GameID) && (s.Machine.Contains(referenceValue))) select s.EventID).ToArray() : (from s in context.Events.Where(s => s.Machine.Contains(referenceValue)) select s.EventID).ToArray());
                    default:
                        return
                            (gameSelection > 0 && gameSelection <= noOfGamesInDB) ?
                            (from s in context.Events.Where(s => s.GameID == gameSelection && (s.Winning.Contains(referenceValue) || s.Machine.Contains(referenceValue))) select s.EventID).ToArray()
                            : ((groupGamesToSearchFrom.Count() > 0) ? (from s in context.Events.Where(s => groupGamesToSearchFrom.Contains(s.GameID) && (s.Winning.Contains(referenceValue) || s.Machine.Contains(referenceValue))) select s.EventID).ToArray() : (from s in context.Events.Where(s => s.Winning.Contains(referenceValue) || s.Machine.Contains(referenceValue)) select s.EventID).ToArray());
                }

            }
            else
            {
                switch (referenceLocation)
                {
                    case 1:
                        return
                           (gameSelection > 0 && gameSelection <= noOfGamesInDB) ?
                           (from s in context.Events.Where(s => s.GameID == gameSelection && (s.Winning[(int)referencePos - 1] == referenceValue)) select s.EventID).ToArray()
                           : ((groupGamesToSearchFrom.Count() > 0) ? (from s in context.Events.Where(s => groupGamesToSearchFrom.Contains(s.GameID) && (s.Winning[(int)referencePos - 1] == referenceValue)) select s.EventID).ToArray() : (from s in context.Events.Where(s => s.Winning[(int)referencePos - 1] == referenceValue) select s.EventID).ToArray());
                    case 2:
                        return
                           (gameSelection > 0 && gameSelection <= noOfGamesInDB) ?
                           (from s in context.Events.Where(s => s.GameID == gameSelection && (s.Machine[(int)referencePos - 1] == referenceValue)) select s.EventID).ToArray()
                           : ((groupGamesToSearchFrom.Count() > 0) ? (from s in context.Events.Where(s => groupGamesToSearchFrom.Contains(s.GameID) && (s.Machine[(int)referencePos - 1] == referenceValue)) select s.EventID).ToArray() : (from s in context.Events.Where(s => s.Machine[(int)referencePos - 1] == referenceValue) select s.EventID).ToArray());
                    default:
                        return
                            (gameSelection > 0 && gameSelection <= noOfGamesInDB) ?
                            (from s in context.Events.Where(s => s.GameID == gameSelection && (s.Winning[(int)referencePos - 1] == referenceValue || s.Machine[(int)referencePos - 1] == referenceValue)) select s.EventID).ToArray()
                            : ((groupGamesToSearchFrom.Count() > 0) ? (from s in context.Events.Where(s => groupGamesToSearchFrom.Contains(s.GameID) && (s.Winning[(int)referencePos - 1] == referenceValue || s.Machine[(int)referencePos - 1] == referenceValue)) select s.EventID).ToArray() : (from s in context.Events.Where(s => s.Winning[(int)referencePos - 1] == referenceValue || s.Machine[(int)referencePos - 1] == referenceValue) select s.EventID).ToArray());
                }
            }
        }
        #endregion
    }
}
