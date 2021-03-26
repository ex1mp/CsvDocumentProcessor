using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentWebViewer.Services.ModelsView;

namespace CsvDocumentWebViewer.Services
{
    public class AutoMappProfile : Profile
    {
        public AutoMappProfile()
        {
            CreateMap<Product, ProductView>().ReverseMap();
            CreateMap<Manager, ManagerView>().ReverseMap();
            CreateMap<Client, ClientView>().ReverseMap();
            CreateMap<Sales, SalesView>().ForMember(sal => sal.Client,x=>x.MapFrom(src => src.Client)).
                ForMember(sal=> sal.ClientId, x => x.MapFrom(src => src.ClientId)).
                ForMember(sal => sal.Manager, x => x.MapFrom(src => src.Manager)).
                ForMember(sal => sal.ManagerId, x => x.MapFrom(src => src.ManagerId)).
                ForMember(sal => sal.Product, x => x.MapFrom(src => src.Product)).
                ForMember(sal => sal.ProductId, x => x.MapFrom(src => src.ProductId)).
                ForMember(sal => sal.SaleCost, x => x.MapFrom(src => src.SaleCost)).
                ForMember(sal => sal.SaleDate, x => x.MapFrom(src => src.SaleDate)).
                ForMember(sal => sal.SalesId, x => x.MapFrom(src => src.SalesId)).ReverseMap();
        }
    }
}
