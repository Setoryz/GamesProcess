using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProcess.Models
{
    public class GamesClass
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
