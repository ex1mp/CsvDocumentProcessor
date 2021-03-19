using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CsvDocumentWebViewer.Services.Models;

    public class CsvDocumentWebViewerContext : DbContext
    {
        public CsvDocumentWebViewerContext (DbContextOptions<CsvDocumentWebViewerContext> options)
            : base(options)
        {
        }

        public DbSet<CsvDocumentWebViewer.Services.Models.SalesView> SalesView { get; set; }

        public DbSet<CsvDocumentWebViewer.Services.Models.ManagerView> ManagerView { get; set; }

        public DbSet<CsvDocumentWebViewer.Services.Models.ClientView> ClientView { get; set; }

        public DbSet<CsvDocumentWebViewer.Services.Models.ProductView> ProductView { get; set; }
    }
