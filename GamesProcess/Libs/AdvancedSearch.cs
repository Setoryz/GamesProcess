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
            if ((currentWeek + secondWeek) < events.Count() && (currentWeek + secondWeek >= 0))
            {
                return events.Skip(currentWeek + secondWeek).First().Winning.Contains(secondWeekValue) || events.Skip(currentWeek + secondWeek).First().Machine.Contains(secondWeekValue);
            }
            else
            {
                return false;
            }
        }
        // OVERLOAD
        private static bool refEventHasValueCheck(IQueryable<Event> events, int currentWeek, int secondWeek, int secondWeekValue, int secondWeekValuePosition)
        {
            if ((currentWeek + secondWeek) < events.Count() && (currentWeek + secondWeek >= 0))
            {
                return events.Skip(currentWeek + secondWeek).First().Winning[secondWeekValuePosition] == secondWeekValue || events.Skip(currentWeek + secondWeek).First().Machine[secondWeekValuePosition] == secondWeekValue;
            }
            else
            {
                return false;
            }
        }

        // PUBLIC IQUERYABLE METHOD TO FIND EVENTS
        // FINDS EVENT WHEN ONLY ONE NUMBR IS PROVIDED
        public static IQueryable<Event> FindAsync(IQueryable<Event> events, int noOfWeeksToDisplay, int referenceValue, int? referencePosition)
        {
            IList<Event> selectedEvents = new List<Event>();

            if (referencePosition.HasValue)
            {
                for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                {
                    var currentEvent = events.Skip(currentWeek).First();

                    if (currentEventHasValueCheck(currentEvent, referenceValue, (int)referencePosition))
                    {
                        selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                    }
                }
            }
            else
            {
                for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                {
                    var currentEvent = events.Skip(currentWeek).First();

                    if (currentEventHasValueCheck(currentEvent, referenceValue))
                    {
                        selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                    }
                }
            }
            return selectedEvents.AsQueryable();
        }
        // OVERLOAD
        // FINDS EVENT WHEN TWO VALUES ARE PROVIDED
        public static IQueryable<Event> FindAsync(IQueryable<Event> events, int noOfWeeksToDisplay, int referenceValue, int? referencePosition, int valueTwo, int valueTwoWeek)
        {
            IList<Event> selectedEvents = new List<Event>();

            if (referencePosition.HasValue)
            {
                for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                {
                    var currentEvent = events.Skip(currentWeek).First();

                    if (currentEventHasValueCheck(currentEvent, referenceValue, (int)referencePosition))
                    {
                        if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo))
                        {
                            selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                        }
                    }
                }
            }
            else
            {
                for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                {
                    var currentEvent = events.Skip(currentWeek).First();

                    if (currentEventHasValueCheck(currentEvent, referenceValue))
                    {
                        if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo))
                        {
                            selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                        }
                    }
                }
            }

            return selectedEvents.AsQueryable();
        }

        //FINDS EVENT WHEN TWO VALUES ARE PROVIDED AND THE SECOND VALUE HAS A POSITION
        public static IQueryable<Event> FindAsync(IQueryable<Event> events, int noOfWeeksToDisplay, int referenceValue, int? referencePosition, int valueTwo, int valueTwoWeek, int valueTwoPosition)
        {
            IList<Event> selectedEvents = new List<Event>();

            if (referencePosition.HasValue)
            {
                for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                {
                    var currentEvent = events.Skip(currentWeek).First();

                    if (currentEventHasValueCheck(currentEvent, referenceValue, (int)referencePosition))
                    {
                        if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwoPosition - 1))
                        {
                            selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                        }
                    }
                }
            }
            else
            {
                for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                {
                    var currentEvent = events.Skip(currentWeek).First();

                    if (currentEventHasValueCheck(currentEvent, referenceValue))
                    {
                        if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwoPosition - 1))
                        {
                            selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                        }
                    }
                }
            }


            return selectedEvents.AsQueryable(); ;
        }

        // FINDS EVENT WHEN THREE VALUES ARE PROVIDED
        public static IQueryable<Event> FindAsync(IQueryable<Event> events, int noOfWeeksToDisplay, int referenceValue, int? referencePosition, int valueTwo, int valueTwoWeek, int? valueTwoPosition, int valueThree, int valueThreeWeek, int? valueThreePosition)
        {
            IList<Event> selectedEvents = new List<Event>();

            if (valueThreePosition.HasValue)
            {
                if (valueTwoPosition.HasValue)
                {
                    if (referencePosition.HasValue)
                    {
                        for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                        {
                            var currentEvent = events.Skip(currentWeek).First();
                            if (currentEventHasValueCheck(currentEvent, referenceValue, (int)referencePosition))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, (int)valueTwoPosition - 1) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree, (int)valueThreePosition - 1))
                                {
                                    selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                        {
                            var currentEvent = events.Skip(currentWeek).First();
                            if (currentEventHasValueCheck(currentEvent, referenceValue))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, (int)valueTwoPosition - 1) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree, (int)valueThreePosition - 1))
                                {
                                    selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (referencePosition.HasValue)
                    {
                        for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                        {
                            var currentEvent = events.Skip(currentWeek).First();
                            if (currentEventHasValueCheck(currentEvent, referenceValue, (int)referencePosition))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree, (int)valueThreePosition - 1))
                                {
                                    selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                        {
                            var currentEvent = events.Skip(currentWeek).First();
                            if (currentEventHasValueCheck(currentEvent, referenceValue))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree, (int)valueThreePosition - 1))
                                {
                                    selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (valueTwoPosition.HasValue)
                {
                    if (referencePosition.HasValue)
                    {
                        for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                        {
                            var currentEvent = events.Skip(currentWeek).First();
                            if (currentEventHasValueCheck(currentEvent, referenceValue, (int)referencePosition))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, (int)valueTwoPosition - 1) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree))
                                {
                                    selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                        {
                            var currentEvent = events.Skip(currentWeek).First();
                            if (currentEventHasValueCheck(currentEvent, referenceValue))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, (int)valueTwoPosition - 1) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree))
                                {
                                    selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (referencePosition.HasValue)
                    {
                        for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                        {
                            var currentEvent = events.Skip(currentWeek).First();
                            if (currentEventHasValueCheck(currentEvent, referenceValue, (int)referencePosition))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree))
                                {
                                    selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                        {
                            var currentEvent = events.Skip(currentWeek).First();
                            if (currentEventHasValueCheck(currentEvent, referenceValue))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree))
                                {
                                    selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                                }
                            }
                        }
                    }
                }
            }
            return selectedEvents.AsQueryable();
        }
    }
}
