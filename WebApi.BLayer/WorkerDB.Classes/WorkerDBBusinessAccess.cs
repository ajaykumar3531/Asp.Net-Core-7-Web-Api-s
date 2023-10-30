using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.BLayer.WorkerDB.Interfaces;
using WebApi.DLayer.WorkerDB;
using WebApi.DLayer.WorkerDB.Interfaces;
using WebApi.Models;
using WebApi.Models.WorkerDB.Dtos;

namespace WebApi.BLayer.WorkerDB.Classes
{
    public class WorkerDBBusinessAccess : IWorkerDB_BusinessAccess
    {
        private readonly IWorkerDBDataAccess<Worker> _workerRepo;
        public WorkerDBBusinessAccess(IWorkerDBDataAccess<Worker> workerRepo)
        {
            _workerRepo = workerRepo;
        }

        public async Task<List<Worker>> GetAllWorkers()
        {
            var dat = await _workerRepo.GetAllWorkers();
            return dat;
        }

        public async Task<Worker> GetById(GetByIdDto dto)
        {
            var worker = await _workerRepo.GetById(dto.id);
            return worker;
        }

        
      

        public async Task<Response> WorkerRegistration(WorkerDto dto)
        {
            DateTime current = DateTime.Now;
            string now = current.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime joiningDate = DateTime.ParseExact(now, "yyyy-MM-dd HH:mm:ss", null);

            Worker worker = new Worker()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Salary = dto.Salary,
                Department =  dto.Department,
                JoiningDate = joiningDate
            };

            _workerRepo.Add(worker);

            if(await _workerRepo.SaveChangesAsync())
            {
                return new Response(HttpStatusCode.OK.ToString(), "Worker Registered Successfully", dto);
            }
            else
            {
                return new Response(HttpStatusCode.BadRequest.ToString(), "Worker Registration failed");
            }
        }

        public async Task<Response> Import(IFormFile file)
        {
            var workerList = new List<Worker>();
            DateTime current = DateTime.Now;
            string now = current.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime joiningDate = DateTime.ParseExact(now, "yyyy-MM-dd HH:mm:ss", null);
        
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var package = new ExcelPackage(file.OpenReadStream());
                var worksheet = package.Workbook.Worksheets[0];
                for (int row = worksheet.Dimension.Start.Row+1; row<=worksheet.Dimension.End.Row; row++)
                {
                    Worker worker = new Worker()
                    {
                        FirstName = worksheet.Cells[row, 1].GetValue<string>(),
                       LastName = worksheet.Cells[row, 2].GetValue<string>(),
                        Salary = worksheet.Cells[row, 3].GetValue<int>(),
                        JoiningDate = joiningDate,
                        Department = worksheet.Cells[row, 4].GetValue<string>(),
                    };
                _workerRepo.Add(worker);
            }
            if(await _workerRepo.SaveChangesAsync())
            {
                return new Response(HttpStatusCode.OK.ToString(), "Data stored Succesfully into database");
            }
            else{
                return new Response(HttpStatusCode.BadRequest.ToString(), "Data not stored Succesfully into database");
            }

        }

        public async Task<List<string>> GetHighstSalary()
        {
            return await _workerRepo.GetHighestSalry();
        }

        public async Task<List<(Worker, Bonu)>> JoinTables()
        {
            return await _workerRepo.JoinTables();
        }
    }
}
