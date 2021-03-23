using AutoMapper;
using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository.Repositories.ImplementingClasses;
using CsvDocumentWebViewer.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.ViewsRepository.ManagerViewRepo
{
    public class ManagerViewRepository: IManagerViewRepository
    {
        private Mapper _mappManager;
        private Mapper _mappManagerView;
        public ManagerViewRepository()
        {
            var configM = new MapperConfiguration(cfg => cfg.CreateMap<Manager, ManagerView>());
            _mappManager = new Mapper(configM);
            var configMV = new MapperConfiguration(cfg => cfg.CreateMap<ManagerView, Manager>());
            _mappManagerView = new Mapper(configMV);
        }
        //tested ok
        public async Task<ICollection<ManagerView>> GetAllAsync()
        {
            using ManagerRepository managerRepository = new ManagerRepository();
            var products = await managerRepository.GetAllAsync();
            return _mappManager.Map<ICollection<ManagerView>>(products);
        }
        public ICollection<ManagerView> GetAll()
        {
            using ManagerRepository managerRepository = new ManagerRepository();
            var products = managerRepository.GetAll();
            return _mappManager.Map<ICollection<ManagerView>>(products);
        }
        //tested ok
        public ManagerView Get(int? id)
        {
            using ManagerRepository managerRepository = new ManagerRepository();
            var manager = managerRepository.Get(id);
            return _mappManager.Map<ManagerView>(manager);
        }
        //tested ok
        public void Add(ManagerView managerView)
        {
            using ManagerRepository managerRepository = new ManagerRepository();
            var manager = _mappManagerView.Map<Manager>(managerView);
            managerRepository.Add(ref manager);

        }
        //tested ok
        public void Update(ManagerView managerView)
        {
            using ManagerRepository managerRepository = new ManagerRepository();
            var manager = _mappManagerView.Map<Manager>(managerView);
            managerRepository.Update(manager);
        }
        //tested ok
        public void Delete(int id)
        {
            using ManagerRepository managerRepository = new ManagerRepository();
            managerRepository.Remove(id);
        }
        //tested ok
        public bool Exists(int id)
        {
            using ManagerRepository managerRepository = new ManagerRepository();
            return managerRepository.Exists(id);
        }
    }
}
