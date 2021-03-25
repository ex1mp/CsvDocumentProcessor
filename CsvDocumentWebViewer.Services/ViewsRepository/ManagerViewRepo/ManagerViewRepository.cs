using AutoMapper;
using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository.Repositories.ImplementingClasses;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvDocumentWebViewer.Services.ModelsView;

namespace CsvDocumentWebViewer.Services.ViewsRepository.ManagerViewRepo
{
    public class ManagerViewRepository: IManagerViewRepository
    {
        private readonly Mapper _mappManager;
        private readonly Mapper _mappManagerView;
        public ManagerViewRepository()
        {
            var configM = new MapperConfiguration(cfg => cfg.CreateMap<Manager, ManagerView>());
            _mappManager = new Mapper(configM);
            var configMv = new MapperConfiguration(cfg => cfg.CreateMap<ManagerView, Manager>());
            _mappManagerView = new Mapper(configMv);
        }

        public async Task<ICollection<ManagerView>> GetAllAsync()
        {
            using var managerRepository = new ManagerRepository();
            var products = await managerRepository.GetAllAsync();
            return _mappManager.Map<ICollection<ManagerView>>(products);
        }
        public ICollection<ManagerView> GetAll()
        {
            using var managerRepository = new ManagerRepository();
            var products = managerRepository.GetAll();
            return _mappManager.Map<ICollection<ManagerView>>(products);
        }
       
        public ManagerView Get(int? id)
        {
            using var managerRepository = new ManagerRepository();
            var manager = managerRepository.Get(id);
            return _mappManager.Map<ManagerView>(manager);
        }
    
        public void Add(ManagerView managerView)
        {
            using var managerRepository = new ManagerRepository();
            var manager = _mappManagerView.Map<Manager>(managerView);
            managerRepository.Add(ref manager);

        }
     
        public void Update(ManagerView managerView)
        {
            using var managerRepository = new ManagerRepository();
            var manager = _mappManagerView.Map<Manager>(managerView);
            managerRepository.Update(manager);
        }
       
        public void Delete(int id)
        {
            using var managerRepository = new ManagerRepository();
            managerRepository.Remove(id);
        }

        public bool Exists(int id)
        {
            using var managerRepository = new ManagerRepository();
            return managerRepository.Exists(id);
        }
    }
}
