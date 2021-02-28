using CsvDocumentProcessor.Domain.Entities;
using System.Linq;
using System.Threading;

namespace CsvDocumentProcessor.Repository.Repositories.ImplementingClasses
{
    public class ManagerRepository : IRepository<Manager>
    {
        private AppDbContext dbContext;
        private ReaderWriterLockSlim managersLockSlim;
        public ManagerRepository(AppDbContext dbContext, ReaderWriterLockSlim managersLockSlim)
        {
            this.dbContext = dbContext;
            this.managersLockSlim = managersLockSlim;
        }
        public void Add(ref Manager manager)
        {
            var temp = Get(manager.Surname);
            if (temp == null)
            {
                managersLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(manager.Surname)) == null)
                    {
                        dbContext.Managers.Add(manager);
                        dbContext.SaveChanges();
                    }
                }
                finally
                {
                    managersLockSlim.ExitWriteLock();
                }
            }
            else
            {
                manager = temp;
            }
        }
        public void Remove(Manager manager)
        {
            var temp = Get(manager.Surname);
            if (temp != null)
            {
                managersLockSlim.EnterWriteLock();
                try
                {
                    if ((temp = Get(manager.Surname)) != null)
                    {
                        dbContext.Managers.Remove(manager);
                        dbContext.SaveChanges();
                    }
                }
                finally
                {
                    managersLockSlim.ExitWriteLock();
                }
            }
        }
        public Manager Get(string managerSurname)
        {
            return dbContext.Managers.FirstOrDefault(x => x.Surname == managerSurname);
        }
        public void Dispose()
        {
            managersLockSlim.Dispose();
        }
    }
}
