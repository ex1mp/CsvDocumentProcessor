using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Service.Servicies
{
    public class DocumentProcessorService
    {
        private static List<string> _filesInProcessing;
        private readonly string _processedFilePath;
        private readonly string _filePath;
        private readonly SalesCreatorService _salesCreatorService;

        public DocumentProcessorService()
        {
            _processedFilePath = ConfigurationManager.AppSettings.Get("ProcessedFilePath");
            _filePath = ConfigurationManager.AppSettings.Get("FilePath");
            _filesInProcessing = new List<string>();
            _salesCreatorService = new SalesCreatorService();
        }

        public void DocumentProcessor()
        {
            try
            {
                var csvFiles = Directory.EnumerateFiles(_filePath, "*.csv");

                foreach (var currentFile in csvFiles)
                {
                    if (_filesInProcessing.Contains(currentFile))
                    {
                        continue;
                    }
                    _filesInProcessing.Add(currentFile);
                    DataFromFileToDbAsync(currentFile);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private async void DataFromFileToDbAsync(string currentFile)
        {
            await Task.Run(() => DataFromFileToDb(currentFile));
        }
        private void DataFromFileToDb(string currentFile)
        {
            _salesCreatorService.DataFromFileToDb(currentFile);
            MoveDocument(currentFile);
        }

        private void MoveDocument(string filePath)
        {
            var targetPath = _processedFilePath + "/" + Path.GetFileName(filePath);
            if (!Directory.Exists(_processedFilePath))
            {
                Directory.CreateDirectory(_processedFilePath);
            }
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }
            File.Move(filePath, targetPath);
            _filesInProcessing.RemoveAll(x => x == filePath);
        }
    }
}
