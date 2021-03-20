using CsvDocumentProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Repository.Repositories.ImplementingClasses
{
    public class ClientRepository : IRepository<Client>
    {
        private AppDbContext _dbContext;
        private ReaderWriterLockSlim _clientsLockSlim;
        public ClientRepository(AppDbContext dbContext, ReaderWriterLockSlim clientsLockSlim)
        {
            this._dbContext = dbContext;
            this._clientsLockSlim = clientsLockSlim;
        }
        public ClientRepository()
        {
            _clientsLockSlim = new ReaderWriterLockSlim();
            _dbContext = new AppDbContext();
        }
        public async Task<ICollection<Client>> GetAllAsync()
        {
            return await _dbContext.Clients.ToListAsync();
        }
        public async Task<Client> GetAsync(int id)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(x => x.ClientId == id);
        }
      
        public void Add(ref Client client)
        {
            var temp = Get(client.Surname);
            if (temp == null)
            {

                _clientsLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(client.Surname)) == null)
                    {
                        _dbContext.Clients.Add(client);
                        _dbContext.SaveChanges();
                    }
                }
                finally
                {
                    _clientsLockSlim.ExitWriteLock();
                }
            }
            else
            {
                client = temp;
            }
        }
       
        public void Remove(int id)
        {
            var temp = Get(id);
            if (temp != null)
            {
                _clientsLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(id)) != null)
                    {
                        _dbContext.Clients.Remove(temp);
                        _dbContext.SaveChanges();
                    }
                }
                finally
                {
                    _clientsLockSlim.ExitWriteLock();
                }
            }
        }
        public void Update(Client client)
        {
            var temp = Get(client.ClientId);
            if (temp != null)
            {
                _clientsLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(client.ClientId)) != null)
                    {
                        _dbContext.Entry(temp).CurrentValues.SetValues(client);
                        _dbContext.SaveChanges();
                    }
                }
                finally
                {
                    _clientsLockSlim.ExitWriteLock();
                }
            }

        }
        public Client Get(string clientSurname)
        {
            return _dbContext.Clients.FirstOrDefault(x => x.Surname == clientSurname);
        }
        public Client Get(int id)
        {
            return _dbContext.Clients.Find(id);
        }
        public bool Exists(int id)
        {
            return _dbContext.Clients.Any(e => e.ClientId == id);
        }
        public void Dispose()
        {
            _clientsLockSlim.Dispose();
        }
    }
}
