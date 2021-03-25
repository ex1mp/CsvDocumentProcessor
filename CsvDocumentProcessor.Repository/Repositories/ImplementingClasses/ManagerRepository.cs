using CsvDocumentProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Repository.Repositories.ImplementingClasses
{
    public class ManagerRepository : IRepository<Manager>
    {
        private readonly AppDbContext _dbContext;
        private readonly ReaderWriterLockSlim _managersLockSlim;
        public ManagerRepository(AppDbContext dbContext, ReaderWriterLockSlim managersLockSlim)
        {
            this._dbContext = dbContext;
            this._managersLockSlim = managersLockSlim;
        }
        public ManagerRepository()
        {
            _dbContext = new AppDbContext();
            _managersLockSlim = new ReaderWriterLockSlim();

        }
        public async Task<ICollection<Manager>> GetAllAsync()
        {
            return await _dbContext.Managers.ToListAsync();
        }
        public ICollection<Manager> GetAll()
        {
            return _dbContext.Managers.ToList();
        }
        public void Add(ref Manager manager)
        {
            var temp = Get(manager.Surname);
            if (temp == null)
            {
                _managersLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(manager.Surname)) == null)
                    {
                        _dbContext.Managers.Add(manager);
                        _dbContext.SaveChanges();
                    }
                }
                finally
                {
                    _managersLockSlim.ExitWriteLock();
                }
            }
            else
            {
                manager = temp;
            }
        }

        public void Remove(int id)
        {
            var temp = Get(id);
            if (temp != null)
            {
                _managersLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(id)) != null)
                    {
                        _dbContext.Managers.Remove(temp);
                        _dbContext.SaveChanges();
                    }
                }
                finally
                {
                    _managersLockSlim.ExitWriteLock();
                }
            }
        }
        public void Update(Manager manager)
        {
            var temp = Get(manager.ManagerId);
            if (temp != null)
            {
                _managersLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(manager.ManagerId)) != null)
                    {
                        _dbContext.Entry(temp).CurrentValues.SetValues(manager);
                        _dbContext.SaveChanges();
                    }
                }
                finally
                {
                    _managersLockSlim.ExitWriteLock();
                }
            }
        }
        public Manager Get(string managerSurname)
        {
            return _dbContext.Managers.FirstOrDefault(x => x.Surname == managerSurname);
        }
        public Manager Get(int? id)
        {
            return _dbContext.Managers.Find(id);
        }
        public Manager Get(int id)
        {
            return _dbContext.Managers.Find(id);
        }
        public bool Exists(int id)
        {
            return _dbContext.Managers.Any(e => e.ManagerId == id);
        }
        public void Dispose()
        {
            _managersLockSlim.Dispose();
        }
    }
}
