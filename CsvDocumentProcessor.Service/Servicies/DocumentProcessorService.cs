using CsvDocumentProcessor.Repository.Repositories.SalesRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        private SalesCreatorService salesCreatorService;

        public DocumentProcessorService()
        {
            processedFilePath = ConfigurationManager.AppSettings.Get("ProcessedFilePath");
            filePath = ConfigurationManager.AppSettings.Get("FilePath");
            filesInProcessing = new List<string>();
            salesCreatorService = new SalesCreatorService();
        }

        public void DocumentProcessor()
        {
            try
            {
                var csvFiles = Directory.EnumerateFiles(filePath, "*.csv");

                foreach (var currentFile in csvFiles)
                {
                    if (!filesInProcessing.Contains(currentFile))
                    {
                        filesInProcessing.Add(currentFile);
                        DataFromFileToDbAsync(currentFile);
                    }
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
            salesCreatorService.DataFromFileToDb(currentFile);
            MoveDocument(currentFile);
        }

        private void MoveDocument(string filePath)
        {
            var targetPath = processedFilePath + "/" + Path.GetFileName(filePath);
            if (!Directory.Exists(processedFilePath))
            {
                Directory.CreateDirectory(processedFilePath);
            }
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }
            File.Move(filePath, targetPath);
            filesInProcessing.RemoveAll(x => x == filePath);
        }
    }
}
