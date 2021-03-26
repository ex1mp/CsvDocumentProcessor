using AutoMapper;
using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository.Repositories.ImplementingClasses;
using CsvDocumentWebViewer.Services.ModelsView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.ViewsRepository.ManagerViewRepo
{
    public class ManagerViewRepository : IManagerViewRepository
    {
        private readonly IMapper _mapper;
        public ManagerViewRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ICollection<ManagerView>> GetAllAsync()
        {
            using var managerRepository = new ManagerRepository();
            var products = await managerRepository.GetAllAsync();
            return _mapper.Map<ICollection<ManagerView>>(products);
        }
        public ICollection<ManagerView> GetAll()
        {
            using var managerRepository = new ManagerRepository();
            var products = managerRepository.GetAll();
            return _mapper.Map<ICollection<ManagerView>>(products);
        }

        public ManagerView Get(int? id)
        {
            using var managerRepository = new ManagerRepository();
            var manager = managerRepository.Get(id);
            return _mapper.Map<ManagerView>(manager);
        }

        public void Add(ManagerView managerView)
        {
            using var managerRepository = new ManagerRepository();
            var manager = _mapper.Map<Manager>(managerView);
            managerRepository.Add(ref manager);

        }

        public void Update(ManagerView managerView)
        {
            using var managerRepository = new ManagerRepository();
            var manager = _mapper.Map<Manager>(managerView);
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
