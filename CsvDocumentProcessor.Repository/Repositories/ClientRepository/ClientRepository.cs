using CsvDocumentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Repository.Repositories.ClientRepository
{
    public class ClientRepository: IClientRepository
    {
        private AppDbContext dbContext;
        public ClientRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public ClientRepository()
        {
            dbContext = new AppDbContext();
        }
        public Client FindClient(string clientSurname)
        {
            return dbContext.Clients.FirstOrDefault(x => x.Surname == clientSurname);
        }

        public void AddClient(Client client)
        {
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();
        }

        public void RemoveClient(Client client)
        {
            dbContext.Clients.Remove(client);
            dbContext.SaveChanges();
        }
    }
}
