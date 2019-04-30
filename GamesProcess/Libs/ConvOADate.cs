using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProcess.Libs
{
    public class ConvOADate
    {
        // METHOD TO CONVERT string DATE VALUES IN OLE FORMAT TO DATETIME FORMAT
        public static DateTime FromString(string num)
        {
            double daysToAdd = double.Parse(num);
            DateTime minValue = new DateTime(1899, 12, 30);
            return minValue.AddDays(daysToAdd);
        }

        // METHOD TO CONVERT double DATE VALUES IN OLE FORMAT TO DATETIME FORMAT
        public static DateTime FromDouble(double daysToAdd)
        {
            DateTime minValue = new DateTime(1899, 12, 30);
            return minValue.AddDays(daysToAdd);
        }
    }
}
