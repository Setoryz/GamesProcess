﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProcess.Models
{
    public class GamesClass
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string Name { get; set; } //this is the class to group the games in as in Premier games and Ghana games
        public ICollection<Game> Games { get; set; }
    }
}
