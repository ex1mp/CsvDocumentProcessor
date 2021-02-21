using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Service.Parcers.CsvParcer
{
    public class CsvParcer
    {
        public string GetManagerSurname(string filePath)
        {
            return filePath.Substring(0, Path.GetFileName(filePath).Length - 13);
        }
        public List<CsvDataContainer> GetDataFromCsv(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<CsvDataContainer>();
            return records.ToList();
        }
    }
}
