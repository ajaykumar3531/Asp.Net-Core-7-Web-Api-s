using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DLayer.Entity;

namespace WebApi.DLayer.Interfaces
{
    public interface ITokenManager
    {
        Task<string> GenerateTokenAsync(AspNetUser user);
    }
}
