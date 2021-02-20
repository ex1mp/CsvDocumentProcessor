using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Domain.Entities
{
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Post { get; set; }
    }
}
