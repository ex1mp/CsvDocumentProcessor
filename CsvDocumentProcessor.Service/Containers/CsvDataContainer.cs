using System;

namespace CsvDocumentProcessor.Service.Containers
{
    public struct CsvDataContainer
    {
        public DateTime SaleDate { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public decimal SaleSum { get; set; }
    }
}
