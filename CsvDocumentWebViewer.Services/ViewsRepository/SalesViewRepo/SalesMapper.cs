using AutoMapper;
using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentWebViewer.Services.ModelsView;
using System.Collections.Generic;
using System.Linq;

namespace CsvDocumentWebViewer.Services.ViewsRepository.SalesViewRepo
{
    public class SalesMapper
    {
        private readonly Mapper _mappClient;
        private readonly Mapper _mappManager;
        private readonly Mapper _mappProduct;
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
            var sales = new Sales
            {
                SalesId = salesView.SalesId,
                ClientId = salesView.ClientId,
                ManagerId = salesView.ManagerId,
                ProductId = salesView.ProductId,
                SaleCost = salesView.SaleCost,
                SaleDate = salesView.SaleDate
            };
            return sales;
        }
        public ICollection<Sales> MapSalesView(ICollection<SalesView> salesView)
        {
            return salesView.Select(item => MapSalesView(item)).ToList();
        }
        public SalesView MapSales(Sales sales)
        {
            var salesView = new SalesView { Client = _mappClient.Map<ClientView>(sales.Client) };
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
            return sales.Select(item => MapSales(item)).ToList();
        }
    }
}
