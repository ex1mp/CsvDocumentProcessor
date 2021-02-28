using System;

namespace CsvDocumentProcessor.Repository.Repositories
{
    public interface IRepository<TEntity>:IDisposable where TEntity: class
    {
        void Add(ref TEntity entity);
        void Remove(TEntity entity);
        TEntity Get(string param);
    }
}
