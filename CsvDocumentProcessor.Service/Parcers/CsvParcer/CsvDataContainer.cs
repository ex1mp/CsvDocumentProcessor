using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Service.Parcers.CsvParcer
{
    public struct CsvDataContainer
    {
        public DateTime SaleDate { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public decimal SaleSum { get; set; }
    }
}
