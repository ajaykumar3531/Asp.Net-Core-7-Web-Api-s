using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DLayer.WorkerDB;
using WebApi.Models;
using WebApi.Models.WorkerDB.Dtos;

namespace WebApi.BLayer.WorkerDB.Interfaces
{
    public interface IWorkerDB_BusinessAccess
    {
        Task<Response> WorkerRegistration(WorkerDto dto);
        Task<List<Worker>> GetAllWorkers();
        Task<Worker> GetById(GetByIdDto dto);

        

        Task<List<string>> GetHighstSalary();

        Task<List<(Worker, Bonu)>> JoinTables();

        Task<Response> Import(IFormFile file);
    }
}
