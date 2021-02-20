using CsvDocumentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Repository.Repositories.ClientRepository
{
    public interface IClientRepository
    {
        Client FindClient(string clientSurname);

        void AddClient(Client client);

        void RemoveClient(Client client);
    }
}
