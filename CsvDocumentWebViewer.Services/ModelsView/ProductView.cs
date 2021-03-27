using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsvDocumentWebViewer.Services.ModelsView
{
    public class ProductView
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(15, ErrorMessage = "Maximum 15 characters only")]
        public string ProductName { get; set; }
    }
}
