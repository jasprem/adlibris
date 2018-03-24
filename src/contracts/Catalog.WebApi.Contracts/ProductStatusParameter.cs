using System;

namespace Catalog.WebApi.Contracts
{
    public class ProductStatusParameter
    {
        public int TotalAvailable { get; set; }
        public LocationDetails LocationDetails { get; set; }
    }

    public class LocationDetails
    {
        public int Shelf { get; set; }
        public int TotalAvailableInShelf { get; set; }  
    }
}
