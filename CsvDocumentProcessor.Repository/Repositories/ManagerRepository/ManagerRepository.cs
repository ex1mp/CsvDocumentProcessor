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
        public Manager FindManager(string managerSurname)
        {
            using var dbContext = new AppDbContext();
            return dbContext.Managers.FirstOrDefault(x => x.Surname == managerSurname);
        }

        public void AddManager(Manager manager)
        {
            using var dbContext = new AppDbContext();
            dbContext.Managers.Add(manager);
            dbContext.SaveChanges();
        }

        public void RemoveManager(Manager manager)
        {
            using var dbContext = new AppDbContext();
            dbContext.Managers.Remove(manager);
            dbContext.SaveChanges();
        }
    }
}
