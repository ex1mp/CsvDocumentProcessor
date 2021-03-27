using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsvDocumentWebViewer.Services.ModelsView
{
    public class ManagerView
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ManagerId { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(15, ErrorMessage = "Maximum 15 characters only")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(15, ErrorMessage = "Maximum 15 characters only")]
        public string Surname { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(12, ErrorMessage = "Maximum 12 characters only")]
        public string Post { get; set; }
    }
}

