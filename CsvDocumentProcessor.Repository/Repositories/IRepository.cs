using System;

namespace CsvDocumentProcessor.Repository.Repositories
{
    public interface IRepository<TEntity>:IDisposable where TEntity: class
    {
        void Add(ref TEntity entity);
        void Remove(int id);
        TEntity Get(string param);
        TEntity Get(int id);
        void Update(int id, TEntity entity);
        bool Exists(int id);
    }
}
