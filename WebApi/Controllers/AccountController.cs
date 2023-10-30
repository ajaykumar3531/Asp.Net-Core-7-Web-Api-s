using Microsoft.AspNetCore.Mvc;
using WebApi.BLayer.Interfaces;
using WebApi.Models;
using WebApi.Models.Dtos;

namespace WebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IBusinessAccess _businessAccess;

        public AccountController(IBusinessAccess businessAccess)
        {
            _businessAccess = businessAccess;
        }

        [Route("User Registration")]
        [HttpPost]

        public async Task<IActionResult> Register(UserRegistrationDto dto)
        {
            var res = await _businessAccess.UserRegistration(dto);
            return Ok(res);
        }

        [Route("Login")]
        [HttpPost]

        public async Task<IActionResult> Login(LoginDto dto)
        {
            var res = await _businessAccess.UserLogin(dto);
            return Ok(res);
        }
    }
}
