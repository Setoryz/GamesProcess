using GamesProcess.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProcess.Libs
{
    public class AdvancedSearch
    {
        // PRIVATE BOOL METHOD TO CHECK IF CURRENT EVENT CONTAINS VALUE CHECKED
        private static bool currentEventHasValueCheck(Event currentEvent, int referenceValue)
        {
            return currentEvent.Winning.Contains(referenceValue) || currentEvent.Machine.Contains(referenceValue);
        }
        // OVERLOAD
        // PRIVATE BOOK TO CHECK IF CURRENT EVENT CONTAINS VLAUE CHECKED IN GIVEN POSITION
        private static bool currentEventHasValueCheck(Event currentEvent, int referenceValue, int referencePosition)
        {
            return currentEvent.Winning[referencePosition - 1] == referenceValue || currentEvent.Machine[referencePosition - 1] == referenceValue;
        }

        // PRIVATE BOOL METHOD TO CHECK IF REFERENCE WEEEK HAS PROVIDED VALUE
        private static bool refEventHasValueCheck(IQueryable<Event> events, int currentWeek, int secondWeek, int secondWeekValue)
        {
            return events.Skip(currentWeek + secondWeek).First().Winning.Contains(secondWeekValue) || events.Skip(currentWeek + secondWeek).First().Machine.Contains(secondWeekValue);
        }
        // PUBLIC IQUERYABLE METHOD TO FIND EVENTS
        // FINDS EVENT WHEN ONLY ONE NUMBR IS PROVIDED
        public static IQueryable<Event> FindAsync(IQueryable<Event> events, int referenceValue, int referencePosition)
        {
            IList<Event> selectedEvents = new List<Event>();

            if (referencePosition != 0)
            {
                for (int i = 0; i < events.Count(); i++)
                {
                    var currentEvent = events.Skip(i).First();
                    
                    if (currentEventHasValueCheck(currentEvent, referenceValue, referencePosition))
                    {
                        for (int j = i - 2; j <= i + 2; j++)
                        {
                            if (j >= 0)
                            {
                                selectedEvents.Add(events.Skip(j).First());
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < events.Count(); i++)
                {
                    var currentEvent = events.Skip(i).First();
                    
                    if (currentEventHasValueCheck(currentEvent, referenceValue))
                    {
                        for (int j = i - 2; j <= i + 2; j++)
                        {
                            selectedEvents.Add(events.Skip(j).First());
                        }
                    }
                }
            }
            return selectedEvents.AsQueryable();
        }
        // OVERLOAD
        // FINDS EVENT WHEN TWO VALUES ARE PROVIDED
        public static IQueryable<Event> FindAsync(IQueryable<Event> events, int referenceValue, int referencePosition, int valueTwo, int valueTwoWeek)
        {
            IList<Event> selectedEvents = new List<Event>();

            if (referencePosition != 0)
            {
                for (int i = 0; i < events.Count(); i++)
                {
                    var currentEvent = events.Skip(i).First();
                    
                    if (currentEventHasValueCheck(currentEvent, referenceValue, referencePosition))
                    {
                        if (events.Skip(i + valueTwoWeek).First().Winning.Contains(valueTwo) || events.Skip(i +valueTwoWeek).First().Machine.Contains(valueTwo))
                        {
                            for (int j = i - 2; j <= i + 2; j++)
                            {
                                if (j >= 0)
                                {
                                    selectedEvents.Add(events.Skip(j).First());
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < events.Count(); i++)
                {
                    var currentEvent = events.Skip(i).First();
                    //bool winningCheck = currentEvent.Winning.Contains(referenceValue);
                    //bool machineCheck = currentEvent.Machine.Contains(referenceValue);
                    if (currentEventHasValueCheck(currentEvent, referenceValue))
                    {
                        if (events.Skip(i + valueTwoWeek).First().Winning.Contains(valueTwo) || events.Skip(i + valueTwoWeek).First().Machine.Contains(valueTwo))
                        {

                        }
                        for (int j = i - 2; j <= i + 2; j++)
                        {
                            selectedEvents.Add(events.Skip(j).First());
                        }
                    }
                }
            }

            return selectedEvents.AsQueryable(); ;
        }

        //FINDS EVENT WHEN TWO VALUES ARE PROVIDED AND THE SECOND VALUE HAS A POSITION
        public static IQueryable<Event> FindAsync(IQueryable<Event> events, int referenceValue, int referencePosition, int valueTwo, int valueTwoWeek, int valueTwoPosition)
        {
            IList<Event> selectedEvents = new List<Event>();

            for (int i = 0; i < events.Count(); i++)
            {
                var currentEvent = events.Skip(i).First();
                //bool winningCheck = currentEvent.Winning[referencePosition - 1] == referenceValue;
                //bool machineCheck = currentEvent.Machine[referencePosition - 1] == referenceValue;
                if (currentEventHasValueCheck(currentEvent, referenceValue, referencePosition))
                {
                    if (events.Skip(i + valueTwoWeek).First().Winning[valueTwoPosition - 1] == valueTwo)
                    {
                        for (int j = i - 2; j <= i + 2; j++)
                        {
                            if (j >= 0)
                            {
                                selectedEvents.Add(events.Skip(j).First());
                            }
                        }
                    }
                }
            }
            
            return selectedEvents.AsQueryable(); ;
        }
    }
}
