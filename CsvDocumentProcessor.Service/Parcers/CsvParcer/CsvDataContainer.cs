﻿using System;

namespace CsvDocumentProcessor.Service.Parcers.CsvParcer
{
    public struct CsvDataContainer
    {
        public DateTime SaleDate { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public decimal SaleSum { get; set; }
    }
}
