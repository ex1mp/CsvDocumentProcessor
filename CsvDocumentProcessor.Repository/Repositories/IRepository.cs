using CsvDocumentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvDocumentProcessor.Repository.Repositories
{
    public interface IRepository<TEntity>:IDisposable where TEntity: class
    {
        void Add(ref TEntity entity);
        void Remove(int id);
        TEntity Get(string param);
        TEntity Get(int id);
        void Update(TEntity entity);
        bool Exists(int id);
    }
}
