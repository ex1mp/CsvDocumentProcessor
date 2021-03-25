using CsvDocumentWebViewer.Services.ModelsView;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.ViewsRepository.ManagerViewRepo
{
    public interface IManagerViewRepository
    {
        ICollection<ManagerView> GetAll();
        virtual Task<ICollection<ManagerView>> GetAllAsync()
        {
            throw new Exception("not implemented");
        }
        ManagerView Get(int? id);
        void Add(ManagerView managerView);
        void Update(ManagerView managerView);
        void Delete(int id);
        bool Exists(int id);
    }
}
