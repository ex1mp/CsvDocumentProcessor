using AutoMapper;
using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository.Repositories.ImplementingClasses;
using CsvDocumentWebViewer.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.ViewsRepository.ClientViewRepo
{
    public class ClientViewRepository
    {
        private Mapper _mappClient;
        private Mapper _mappClientView;
        public ClientViewRepository()
        {
            var configCl = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientView>());
            _mappClient = new Mapper(configCl);
            var configClV = new MapperConfiguration(cfg => cfg.CreateMap<ClientView, Client>());
            _mappClientView = new Mapper(configClV);
        }
        //tested ok
        public async Task<ICollection<ClientView>> GetAllAsync()
        {
            using ClientRepository clientRepository = new ClientRepository();
            var clients = await clientRepository.GetAllAsync();
            return _mappClient.Map<ICollection<ClientView>>(clients);
        }
        //tested ok
        public ClientView Get(int id)
        {
            using ClientRepository clientRepository = new ClientRepository();
            var client = clientRepository.Get(id);
            return _mappClient.Map<ClientView>(client);
        }
        //tested ok
        public void Add(ClientView clientView)
        {
            using ClientRepository clientRepository = new ClientRepository();
            var client = _mappClientView.Map<Client>(clientView);
            clientRepository.Add(ref client);

        }
        //tested ok
        public void Update(ClientView clientView)
        {
            using ClientRepository clientRepository = new ClientRepository();
            var client = _mappClientView.Map<Client>(clientView);
            clientRepository.Update(client);
        }
        //tested ok
        public void Delete(int id)
        {
            using ClientRepository clientRepository = new ClientRepository();
            clientRepository.Remove(id);
        }
        //tested ok
        public bool Exists(int id)
        {
            using ClientRepository clientRepository = new ClientRepository();
            return clientRepository.Exists(id);
        }
    }
}
