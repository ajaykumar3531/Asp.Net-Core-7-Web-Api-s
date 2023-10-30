using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.Dtos;

namespace WebApi.BLayer.Interfaces
{
    public interface IBusinessAccess
    {
        Task<Response> UserRegistration(UserRegistrationDto dto);

        Task<Response> UserLogin(LoginDto loginDto);


    }
}
