using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProcess.Libs
{
    public static class SearchExtension
    {

        // EXTENSION METHOD TO ADD SELECTED EVENT WITH SPECIFIED AMOUNT OF WEEKS BEFORE AND AFTER TO LIST
        public static void JoinSelectedEvents<Event>(this ICollection<Event> collection, IQueryable<Event> events, int currentWeek, int noOfWeeksToDisplay)
        {
            for (int j = currentWeek - noOfWeeksToDisplay; j <= currentWeek + noOfWeeksToDisplay; j++)
            {
                if (j >= 0 && j < events.Count())
                {
                    collection.Add(events.Skip(j).First());
                }
            }
        }
    }
}
