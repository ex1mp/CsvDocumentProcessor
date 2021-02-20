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
        public Client FindClient(string clientSurname)
        {
            using var dbContext = new AppDbContext();
            return dbContext.Clients.FirstOrDefault(x => x.Surname == clientSurname);
        }

        public void AddClient(Client client)
        {
            using var dbContext = new AppDbContext();
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();
        }

        public void RemoveClient(Client client)
        {
            using var dbContext = new AppDbContext();
            dbContext.Clients.Remove(client);
            dbContext.SaveChanges();
        }
    }
}
