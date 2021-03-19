using CsvDocumentProcessor.Domain.Entities;
using CsvDocumentProcessor.Repository.Repositories;
using CsvDocumentProcessor.Repository.Repositories.SalesRepository;

namespace CsvDocumentProcessor.Repository
{
    public class CsvDocumentRepository
    {
        private LockSlimContainer syncObjContainer;
        public IRepository<Client> clientRepository { get; set; }
        public IRepository<Manager> managerRepository { get; set; }
        public IRepository<Product> productRepository { get; set; }
        public SalesRepository salesRepository { get; set; }
        public CsvDocumentRepository()
        {


        }
    }
}
