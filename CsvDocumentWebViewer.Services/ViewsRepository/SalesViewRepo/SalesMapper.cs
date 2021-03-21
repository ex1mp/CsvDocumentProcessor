using AutoMapper;
using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentWebViewer.Services.Models;
using System.Collections.Generic;

namespace CsvDocumentWebViewer.Services.ViewsRepository.SalesViewRepo
{
    public class SalesMapper
    {
        private Mapper _mappClient;
        private Mapper _mappManager;
        private Mapper _mappProduct;
        public SalesMapper()
        {
            var configCl = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientView>());
            _mappClient = new Mapper(configCl);
            var configM = new MapperConfiguration(cfg => cfg.CreateMap<Manager, ManagerView>());
            _mappManager = new Mapper(configM);
            var configP = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductView>());
            _mappProduct = new Mapper(configP);

        }
        public Sales MapSalesView(SalesView salesView)
        {
            var sales = new Sales();
            sales.SalesId = salesView.SalesId;
            sales.ClientId = salesView.ClientId;
            sales.ManagerId = salesView.ManagerId;
            sales.ProductId = salesView.ProductId;
            sales.SaleCost = salesView.SaleCost;
            sales.SaleDate = salesView.SaleDate;
            return sales;
        }
        public ICollection<Sales> MapSalesView(ICollection<SalesView> salesView)
        {
            var sales = new List<Sales>();
            foreach (var item in salesView)
            {
                sales.Add(MapSalesView(item));
            }
            return sales;
        }
        public SalesView MapSales(Sales sales)
        {
            SalesView salesView = new SalesView();
            salesView.Client = _mappClient.Map<ClientView>(sales.Client);
            salesView.ClientId = salesView.Client.ClientId;
            salesView.Manager = _mappManager.Map<ManagerView>(sales.Manager);
            salesView.ManagerId = salesView.Manager.ManagerId;
            salesView.Product = _mappProduct.Map<ProductView>(sales.Product);
            salesView.ProductId = salesView.Product.ProductId;
            salesView.SaleCost = sales.SaleCost;
            salesView.SaleDate = sales.SaleDate;
            salesView.SalesId = sales.SalesId;
            return salesView;
        }
        public ICollection<SalesView> MapSales(ICollection<Sales> sales)
        {
            var salesViews = new List<SalesView>();
            foreach (var item in sales)
            {
                salesViews.Add(MapSales(item));
            }
            return salesViews;
        }
    }
}
