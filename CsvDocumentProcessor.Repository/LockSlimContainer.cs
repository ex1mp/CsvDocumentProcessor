using System.Threading;

namespace CsvDocumentProcessor.Repository
{
    public class LockSlimContainer
    {
        public ReaderWriterLockSlim ClientsLocker { get; set; }
        public ReaderWriterLockSlim ManagersLocker { get; set; }
        public ReaderWriterLockSlim ProductsLocker { get; set; }
        public ReaderWriterLockSlim SalesLocker { get; set; }
        public LockSlimContainer()
        {
            ClientsLocker = new ReaderWriterLockSlim();
            ManagersLocker = new ReaderWriterLockSlim();
            ProductsLocker = new ReaderWriterLockSlim();
            SalesLocker = new ReaderWriterLockSlim();
        }
    }
}
