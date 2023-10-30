using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DLayer.WorkerDB.Interfaces
{
    public interface IWorkerDBDataAccess<T> where T : class
    {
        Task<T> GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> SaveChangesAsync();
        Task<List<T>> GetAllWorkers();
        void AddList(List<Worker> workers);
        Task<List<string>> GetHighestSalry();
        Task<List<(Worker, Bonu)>> JoinTables();
    }
}
