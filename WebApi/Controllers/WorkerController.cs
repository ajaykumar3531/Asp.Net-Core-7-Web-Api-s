using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.BLayer.WorkerDB.Interfaces;
using WebApi.Models;
using WebApi.Models.WorkerDB.Dtos;

namespace WebApi.Controllers
{
    //[Authorize]
    public class WorkerController : Controller
    {

        private readonly IWorkerDB_BusinessAccess _businessRepo;

        public WorkerController(IWorkerDB_BusinessAccess businessAccess)
        {
            _businessRepo = businessAccess;
        }


        [Route("Worker Registartion")]
        [HttpPost]
        //[AllowAnonymous]
        public async Task<IActionResult> WorkerRegistation(WorkerDto dto)
        {
            var res = await _businessRepo.WorkerRegistration(dto);
            return Ok(res);
        }
        [Route("GetAllWorkers")]
        [HttpGet]
        public async Task<IActionResult> GetAllWorkers()
        {
            var res = await _businessRepo.GetAllWorkers();
            return Ok(res);
        }

        [Route("GetById")]
        [HttpGet]
        public async Task<IActionResult> GetById(GetByIdDto dto)
        {
            var res = await _businessRepo.GetById(dto);
            return Ok(new Response(HttpStatusCode.OK.ToString(),"Fetched Successfully",res) );
        }

     
      
        [HttpGet("GetHighestSalary")]

        public async Task<IActionResult> GetHighestSalary()
        {
            var res = await _businessRepo.GetHighstSalary();
            return Ok(res);
        }

        [HttpGet("Join-Tables")]
        public async Task<IActionResult> joinTable()
        {
            return  Ok(await _businessRepo.JoinTables());
        }

        [HttpPost("Upload-file")]
        public async Task<IActionResult> UploadExcelFile(IFormFile file)
        {
            var res = await _businessRepo.Import(file);
            return Ok(res);
        }

    }
}
