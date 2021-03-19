using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.Models
{
    public class SalesView
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SalesId { get; set; }
        public int ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public virtual ManagerView Manager { get; set; }
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual ClientView Client { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual ProductView Product { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SaleCost { get; set; }
    }
}
