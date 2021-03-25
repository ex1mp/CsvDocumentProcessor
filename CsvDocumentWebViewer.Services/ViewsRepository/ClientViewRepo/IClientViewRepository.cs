using CsvDocumentWebViewer.Services.ModelsView;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.ViewsRepository.ClientViewRepo
{
    public interface IClientViewRepository
    {
        ICollection<ClientView> GetAll();
        virtual Task<ICollection<ClientView>> GetAllAsync()
        {
            throw new Exception("not implemented");
        }
        ClientView Get(int? id);
        void Add(ClientView clientView);
        void Update(ClientView clientView);
        void Delete(int id);
        bool Exists(int id);
    }
}
