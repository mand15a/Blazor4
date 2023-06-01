using Blazor4.Models;
using System.Collections.Generic;

namespace Blazor4.Service
{
    public class SearchService
    {
        private AdventureWorks2019Context _db;

        public string SearchInput { get; set; }
        public Person SelectedPerson { get; set; }
        public List<Person> SearchResults { get; set; }

        public SearchService(AdventureWorks2019Context dbContext)
        {
            _db = dbContext;
            SearchResults = new List<Person>();
        }

        public void PerformSearch()
        {
            if (!string.IsNullOrEmpty(SearchInput))
            {
                SearchResults = _db.People
                    .AsEnumerable() // Perform client-side evaluation
                    .Where(p => p.FirstName.Contains(SearchInput, StringComparison.OrdinalIgnoreCase) ||
                                p.LastName.Contains(SearchInput, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                SearchResults.Clear();
            }
        }

    }
}
