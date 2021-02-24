using CsvDocumentProcessor.Domain.Entities;

namespace CsvDocumentProcessor.Repository.Repositories.ManagerRepository
{
    public interface IManagerRepository
    {
        Manager FindManager(string managerSurname);

        void AddManager(Manager manager);

        void RemoveManager(Manager manager);

    }
}
