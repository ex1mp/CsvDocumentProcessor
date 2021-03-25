using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsvDocumentWebViewer.Services.ModelsView
{
    public class SalesView
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
