using CsvDocumentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Repository.Repositories.ManagerRepository
{
    public class ManagerRepository: IManagerRepository
    {
        private AppDbContext dbContext;
        public ManagerRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public ManagerRepository()
        {
            this.dbContext = new AppDbContext();
        }
        public Manager FindManager(string managerSurname)
        {
            return dbContext.Managers.FirstOrDefault(x => x.Surname == managerSurname);
        }

        public void AddManager(Manager manager)
        {
            dbContext.Managers.Add(manager);
            dbContext.SaveChanges();
        }

        public void RemoveManager(Manager manager)
        {
            dbContext.Managers.Remove(manager);
            dbContext.SaveChanges();
        }
    }
}
