using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.Models
{
    public class SalesView
    {
        public int SalesId { get; set; }
        public int ManagerId { get; set; }
        public virtual ManagerView Manager { get; set; }
        public int ClientId { get; set; }
        public virtual ClientView Client { get; set; }
        public int ProductId { get; set; }
        public virtual ProductView Product { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SaleCost { get; set; }
    }
}
