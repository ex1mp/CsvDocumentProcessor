using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.Models
{
    public class ManagerView
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ManagerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Post { get; set; }
    }
}

