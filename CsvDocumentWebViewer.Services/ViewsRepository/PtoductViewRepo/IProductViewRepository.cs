using CsvDocumentWebViewer.Services.ModelsView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Services.ViewsRepository.PtoductViewRepo
{
    public interface IProductViewRepository
    {
        ICollection<ProductView> GetAll();
        virtual Task<ICollection<ProductView>> GetAllAsync()
        {
            return null;
        }
        ProductView Get(int? id);
        void Add(ProductView productView);
        void Update(ProductView productView);
        void Delete(int id);
        bool Exists(int id);
    }
}
