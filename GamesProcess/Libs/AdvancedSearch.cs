using GamesProcess.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static GamesProcess.Libs.SearchBoolCheck;
using System.Threading.Tasks;


namespace GamesProcess.Libs
{
    public class AdvancedSearch
    {
        // PUBLIC IQUERYABLE METHOD TO FIND EVENTS
        // FINDS EVENT WHEN ONLY ONE NUMBR IS PROVIDED
        public static IQueryable<Event> FindAsync(IQueryable<Event> events, int noOfWeeksToDisplay, int referenceValue, int referenceLocation, int? referencePosition)
        {
            IList<Event> selectedEvents = new List<Event>();

            if (referencePosition.HasValue)
            {
                for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                {
                    var currentEvent = events.Skip(currentWeek).First();

                    if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation, (int)referencePosition))
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

                    if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation))
                    {
                        selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                    }
                }
            }
            return selectedEvents.AsQueryable();
        }
        // OVERLOAD
        // FINDS EVENT WHEN TWO VALUES ARE PROVIDED
        //FINDS EVENT WHEN TWO VALUES ARE PROVIDED AND THE SECOND VALUE HAS A POSITION
        public static IQueryable<Event> FindAsync(IQueryable<Event> events, int noOfWeeksToDisplay, int referenceValue, int referenceLocation, int? referencePosition, int valueTwo, int valueTwoWeek, int valueTwolocation, int? valueTwoPosition)
        {
            IList<Event> selectedEvents = new List<Event>();

            if (valueTwoPosition.HasValue)
            {
                if (referencePosition.HasValue)
                {
                    for (int currentWeek = 0; currentWeek < events.Count(); currentWeek++)
                    {
                        var currentEvent = events.Skip(currentWeek).First();

                        if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation, (int)referencePosition))
                        {
                            if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwolocation, (int)valueTwoPosition - 1))
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

                        if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation))
                        {
                            if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwolocation, (int)valueTwoPosition - 1))
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

                        if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation, (int)referencePosition))
                        {
                            if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwolocation))
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

                        if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation))
                        {
                            if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwolocation))
                            {
                                selectedEvents.JoinSelectedEvents(events, currentWeek, noOfWeeksToDisplay);
                            }
                        }
                    }
                }
            }


            return selectedEvents.AsQueryable(); ;
        }

        // FINDS EVENT WHEN THREE VALUES ARE PROVIDED
        public static IQueryable<Event> FindAsync(IQueryable<Event> events, int noOfWeeksToDisplay, int referenceValue, int referenceLocation, int? referencePosition, int valueTwo, int valueTwoWeek, int valueTwoLocation, int? valueTwoPosition, int valueThree, int valueThreeWeek, int valueThreeLocation, int? valueThreePosition)
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
                            if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation, (int)referencePosition))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwoLocation, (int)valueTwoPosition - 1) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree, valueThreeLocation, (int)valueThreePosition - 1))
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
                            if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwoLocation, (int)valueTwoPosition - 1) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree, valueThreeLocation, (int)valueThreePosition - 1))
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
                            if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation, (int)referencePosition))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwoLocation) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree, valueThreeLocation, (int)valueThreePosition - 1))
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
                            if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwoLocation) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree, valueThreeLocation, (int)valueThreePosition - 1))
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
                            if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation, (int)referencePosition))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwoLocation, (int)valueTwoPosition - 1) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree, valueThreeLocation))
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
                            if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwoLocation, (int)valueTwoPosition - 1) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree, valueThreeLocation))
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
                            if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation, (int)referencePosition))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwoLocation) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree, valueThreeLocation))
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
                            if (currentEventHasValueCheck(currentEvent, referenceValue, referenceLocation))
                            {
                                if (refEventHasValueCheck(events, currentWeek, valueTwoWeek, valueTwo, valueTwoLocation) && refEventHasValueCheck(events, currentWeek, valueThreeWeek, valueThree, valueThreeLocation))
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
