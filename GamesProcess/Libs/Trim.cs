using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProcess.Libs
{
    public static class Trim
    {
        public static string trim (string text)
        {
            return text.Replace(" ", "");
        }
    }
}
