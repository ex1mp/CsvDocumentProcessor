using CsvDocumentProcessor.Domain.Entities;

namespace CsvDocumentProcessor.Repository.Repositories.ClientRepository
{
    public interface IClientRepository
    {
        Client FindClient(string clientSurname);

        void AddClient(Client client);

        void RemoveClient(Client client);
    }
}
