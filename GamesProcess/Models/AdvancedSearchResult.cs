using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProcess.Models
{
    public class AdvancedSearchResult
    {
        public int ID { get; set; }
        public List<Event> Events { get; set; }
        public int NoOfWeeksAvailableBeforeRef { get; set; }
        public int NoOfWeeksAvailableAfterRef { get; set; }
    }
}
