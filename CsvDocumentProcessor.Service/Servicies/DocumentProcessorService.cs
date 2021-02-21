using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Service.Servicies
{
    public class DocumentProcessorService
    {
        private static List<string> filesInProcessing;
        private readonly string processedFilePath;
        private readonly string filePath;
        //в этом классе логика
        public DocumentProcessorService()
        {
            processedFilePath = ConfigurationManager.AppSettings.Get("ProcessedFilePath");
            filePath = ConfigurationManager.AppSettings.Get("FilePath");
            filesInProcessing = new List<string>();
        }
    }
}
