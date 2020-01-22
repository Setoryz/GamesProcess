using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProcess.Models
{
    public class GamesClass
    {
        public int id { get; set; }

        public string Name { get; set; } //this is the class to group the games in as in Premier games and Ghana games
        public ICollection<Game> Games { get; set; }
    }
}
