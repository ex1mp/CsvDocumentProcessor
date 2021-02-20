using CsvDocumentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Repository.Repositories.ManagerRepository
{
    public interface IManagerRepository
    {
        Manager FindManager(string managerSurname);

        void AddManager(Manager manager);

        void RemoveManager(Manager manager);

    }
}
