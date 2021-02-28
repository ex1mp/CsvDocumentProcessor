using CsvDocumentProcessor.Domain.Entities;
using System.Linq;
using System.Threading;

namespace CsvDocumentProcessor.Repository.Repositories.ImplementingClasses
{
    public class ClientRepository : IRepository<Client>
    {
        private AppDbContext dbContext;
        private ReaderWriterLockSlim clientsLockSlim;
        public ClientRepository(AppDbContext dbContext, ReaderWriterLockSlim clientsLockSlim)
        {
            this.dbContext = dbContext;
            this.clientsLockSlim = clientsLockSlim;
        }
        public void Add(ref Client client)
        {
            var temp = Get(client.Surname);
            if (temp == null)
            {
                clientsLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(client.Surname)) == null)
                    {
                        dbContext.Clients.Add(client);
                        dbContext.SaveChanges();
                    }
                }
                finally
                {
                    clientsLockSlim.ExitWriteLock();
                }  
            }
            else
            {
                client = temp;
            }
        }
        public void Remove(Client client)
        {
            var temp = Get(client.Surname);
            if (temp != null)
            {
                clientsLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(client.Surname)) != null)
                    {
                        dbContext.Clients.Remove(client);
                        dbContext.SaveChanges();
                    }
                }
                finally
                {
                    clientsLockSlim.ExitWriteLock();
                }  
            }
        }
        public Client Get(string clientSurname)
        {
            return dbContext.Clients.FirstOrDefault(x => x.Surname == clientSurname);
        }
        public void Dispose()
        {
            clientsLockSlim.Dispose();
        }
    }
}
