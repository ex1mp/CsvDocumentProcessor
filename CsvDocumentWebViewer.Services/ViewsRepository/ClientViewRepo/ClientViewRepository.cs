using AutoMapper;
using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository.Repositories.ImplementingClasses;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvDocumentWebViewer.Services.ModelsView;

namespace CsvDocumentWebViewer.Services.ViewsRepository.ClientViewRepo
{
    public class ClientViewRepository: IClientViewRepository
    {
        private readonly Mapper _mappClient;
        private readonly Mapper _mappClientView;
        public ClientViewRepository()
        {
            var configCl = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientView>());
            _mappClient = new Mapper(configCl);
            var configClV = new MapperConfiguration(cfg => cfg.CreateMap<ClientView, Client>());
            _mappClientView = new Mapper(configClV);
        }

        public async Task<ICollection<ClientView>> GetAllAsync()
        {
            using var clientRepository = new ClientRepository();
            var clients = await clientRepository.GetAllAsync();
            return _mappClient.Map<ICollection<ClientView>>(clients);
        }
        public ICollection<ClientView> GetAll()
        {
            using var clientRepository = new ClientRepository();
            var clients = clientRepository.GetAll();
            return _mappClient.Map<ICollection<ClientView>>(clients);
        }

        public ClientView Get(int? id)
        {
            using var clientRepository = new ClientRepository();
            var client = clientRepository.Get(id);
            return _mappClient.Map<ClientView>(client);
        }

        public void Add(ClientView clientView)
        {
            using var clientRepository = new ClientRepository();
            var client = _mappClientView.Map<Client>(clientView);
            clientRepository.Add(ref client);

        }

        public void Update(ClientView clientView)
        {
            using var clientRepository = new ClientRepository();
            var client = _mappClientView.Map<Client>(clientView);
            clientRepository.Update(client);
        }
 
        public void Delete(int id)
        {
            using var clientRepository = new ClientRepository();
            clientRepository.Remove(id);
        }

        public bool Exists(int id)
        {
            using var clientRepository = new ClientRepository();
            return clientRepository.Exists(id);
        }
    }
}
