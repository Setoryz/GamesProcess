using GamesProcess.Data;
using GamesProcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProcess.Libs
{
    public class AdvSearch
    {
        private readonly GameContext _context;
        public AdvSearch(GameContext context)
        {
            _context = context;
        }


        #region Find Search Results when only one number is provided
        public /*internal*/ static IQueryable<AdvancedSearchResult> FindResults(int noOfWeeksToDisplay, int referenceValue, int referenceLocation, int? referencePos, int gameSelection, int[] groupGamesToSearchFrom)
        {
            if (referencePos.HasValue)
            {

            }

            throw new NotImplementedException();
        } 
        #endregion
    }
}
