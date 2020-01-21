using GamesProcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProcess.Libs
{
    public class SearchBoolCheck
    {
        // PRIVATE BOOL METHOD TO CHECK IF CURRENT EVENT CONTAINS VALUE CHECKED
        public static bool currentEventHasValueCheck(Event currentEvent, int referenceValue, int referenceLocation)
        {
            switch (referenceLocation)
            {
                case 1:
                    return currentEvent.Winning.Contains(referenceValue);
                case 2:
                    return currentEvent.Machine.Contains(referenceValue);
                default:
                    return currentEvent.Winning.Contains(referenceValue) || currentEvent.Machine.Contains(referenceValue);
            }
        }
        // OVERLOAD
        // PRIVATE BOOK TO CHECK IF CURRENT EVENT CONTAINS VLAUE CHECKED IN GIVEN POSITION
        public static bool currentEventHasValueCheck(Event currentEvent, int referenceValue, int referenceLocation, int referencePosition)
        {
            switch (referenceLocation)
            {
                case 1:
                    return currentEvent.Winning[referencePosition - 1] == referenceValue;
                case 2:
                    return currentEvent.Machine[referencePosition - 1] == referenceValue;
                default:
                    return currentEvent.Winning[referencePosition - 1] == referenceValue || currentEvent.Machine[referencePosition - 1] == referenceValue;
            }
        }

        // PRIVATE BOOL METHOD TO CHECK IF REFERENCE WEEEK HAS PROVIDED VALUE
        public static bool refEventHasValueCheck(IQueryable<Event> events, int currentWeek, int secondWeek, int secondWeekValue, int secondWeekValueLocation)
        {
            if ((currentWeek + secondWeek) < events.Count() && (currentWeek + secondWeek >= 0))
            {
                switch (secondWeekValueLocation)
                {
                    case 1:
                        return events.Skip(currentWeek + secondWeek).First().Winning.Contains(secondWeekValue);
                    case 2:
                        return events.Skip(currentWeek + secondWeek).First().Machine.Contains(secondWeekValue);
                    default:
                        return events.Skip(currentWeek + secondWeek).First().Winning.Contains(secondWeekValue) || events.Skip(currentWeek + secondWeek).First().Machine.Contains(secondWeekValue);
                }
            }
            else
            {
                return false;
            }
        }
        // OVERLOAD
        public static bool refEventHasValueCheck(IQueryable<Event> events, int currentWeek, int secondWeek, int secondWeekValue, int secondWeekValueLocation, int secondWeekValuePosition)
        {
            if ((currentWeek + secondWeek) < events.Count() && (currentWeek + secondWeek >= 0))
            {
                switch (secondWeekValueLocation)
                {
                    case 1:
                        return events.Skip(currentWeek + secondWeek).First().Winning[secondWeekValuePosition] == secondWeekValue;
                    case 2:
                        return events.Skip(currentWeek + secondWeek).First().Machine[secondWeekValuePosition] == secondWeekValue;
                    default:
                        return events.Skip(currentWeek + secondWeek).First().Winning[secondWeekValuePosition] == secondWeekValue || events.Skip(currentWeek + secondWeek).First().Machine[secondWeekValuePosition] == secondWeekValue;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
