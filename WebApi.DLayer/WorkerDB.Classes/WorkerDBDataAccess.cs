using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DLayer.WorkerDB.Interfaces;

namespace WebApi.DLayer.WorkerDB.Classes
{
    public class WorkerDBDataAccess<T> : IWorkerDBDataAccess<T> where  T:class
    {
        private readonly WorkerDBContext _workerDB;
        public WorkerDBDataAccess(WorkerDBContext workerDB)
        {
            _workerDB=workerDB;
        }

        public void Add(T entity)
        {
            _workerDB.Set<T>().Add(entity);
        }

      

        public void AddList(List<Worker> workers)
        {
            _workerDB.Set<Worker>().AddRange(workers);
        }

        public async Task<List<T>> GetAllWorkers()
        {
            var data = await _workerDB.Set<T>().ToListAsync();
            return data;
        }

        public async Task<T> GetById(int id)
        {
            var user = await _workerDB.Set<T>().FindAsync(id);
            if(user != null)
            {
                return user;
            }

            return null;
        }
        public void Remove(T entity)
        {
            _workerDB.Set<T>().Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _workerDB.SaveChangesAsync() > 0;
        }
        public void Update(T entity)
        {
            _workerDB.Set<T>().Update(entity);
        }

        public async Task<List<string>> GetHighestSalry()
        {
            return await _workerDB.Set<Worker>().Select(worker => worker.FirstName.ToUpper()).ToListAsync();
        }

        public async Task<List<(Worker, Bonu)>> JoinTables()
        {
            var result = await (from worker in _workerDB.Set<Worker>()
                                join title in _workerDB.Set<Bonu>()
                                on worker.WorkerId equals title.WorkerRefId into joinedData
                                from title in joinedData.DefaultIfEmpty()
                                select new { Worker = worker, Bonu = title }).ToListAsync();

            return result.Select(x => (x.Worker, x.Bonu)).ToList();
        }
    }
}
