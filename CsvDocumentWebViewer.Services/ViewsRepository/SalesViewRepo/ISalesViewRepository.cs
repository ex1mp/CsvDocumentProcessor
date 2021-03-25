using CsvDocumentWebViewer.Services.ModelsView;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.ViewsRepository.SalesViewRepo
{
    public interface ISalesViewRepository
    {
        virtual Task<ICollection<SalesView>> GetAllAsync()
        {
            throw new Exception("not implemented");
        }
        SalesView Get(int? id);
        void Add(SalesView salesView);
        void Update(SalesView salesView);
        void Delete(int id);
        bool Exists(int id);
    }
}
