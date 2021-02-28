using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvDocumentProcessor.Service.Containers;
using CsvHelper;

namespace CsvDocumentProcessor.Service.Parcers
{
    public class CsvParcer
    {
        public string GetManagerSurname(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            return fileName.Substring(0, fileName.Length - 13);
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
