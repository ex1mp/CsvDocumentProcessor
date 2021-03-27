using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CsvDocumentWebViewer.Services.ModelsView
{
    public class ClientView
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(15, ErrorMessage = "Maximum 15 characters only")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(15, ErrorMessage = "Maximum 15 characters only")]
        public string Surname { get; set; }
    }
}
