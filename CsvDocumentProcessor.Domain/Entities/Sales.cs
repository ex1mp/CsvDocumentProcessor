using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsvDocumentProcessor.Domain.Entities
{
    public class Sales
    {
        [Key]
        public int SaleReportId { get; set; }
        public int ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public virtual Manager Manager { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public virtual Client Customer { get; set; }
        [ForeignKey("ClientId")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SaleCost { get; set; }
    }
}
