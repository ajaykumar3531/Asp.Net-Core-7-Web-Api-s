using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BLayer.Interfaces;
using WebApi.DLayer.Entity;
using WebApi.DLayer.Interfaces;
using WebApi.Models;
using WebApi.Models.Dtos;
using System.Linq;
using System.Net;

namespace WebApi.BLayer.Classes
{
    public class BusinessAccess : IBusinessAccess
    {
        private readonly IDataAccess<AspNetUser> _userRepo;
        private readonly IDataAccess<AspNetRole> _roleRepo;
        private readonly ITokenManager _token;

        public BusinessAccess(IDataAccess<AspNetUser> userRepo, IDataAccess<AspNetRole> roleRepo,ITokenManager token)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _token=token;
        }

        public async Task<Response> UserLogin(LoginDto loginDto)
        {
            bool res = await _userRepo.FindByPasswordAsync(loginDto.Password, loginDto.Email, loginDto.Id);

            AspNetUser user = await _userRepo.FindByPasswordEmailAndIdAsync(loginDto.Password, loginDto.Email,loginDto.Id);

            if (res)
            {
                var token = await _token.GenerateTokenAsync(user);
                return new Response(HttpStatusCode.OK.ToString(), "User Logged Successfully", loginDto,token);
            }
            else
            {
                return new Response(HttpStatusCode.BadRequest.ToString(), "Inavlid Login Deatils");
            }
        }

        public async Task<Response> UserRegistration(UserRegistrationDto dto)
        {
            var email = await _userRepo.FindByEmailAsync(dto.Email);

            if(email != null)
            {
                return new Response(HttpStatusCode.OK.ToString(),"User already Registered");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            AspNetUser user = new AspNetUser()
            {
                UserName = dto.Name,
                Email = dto.Email,
                Id = dto.Id,
                PasswordHash = passwordHash,
                PhoneNumber = dto.PhoneNumber,
                NormalizedUserName = dto.Name.Substring(0, 3).ToLower()+"353"+ dto.Email.Substring(0, 3).ToLower(),
                NormalizedEmail = dto.Email.ToLower(),
                PhoneNumberConfirmed = true,
            };
            _userRepo.Add(user);

            if (await _userRepo.saveChnagesAsync())
            {
                AspNetRole role = new AspNetRole()
                {
                    Id = user.Id,
                    Name = dto.Rolename,
                    NormalizedName = dto.Name,
                    ConcurrencyStamp = "₹",
                };

                _roleRepo.Add(role);

                if(await _roleRepo.saveChnagesAsync())
                {
                    return new Response(HttpStatusCode.OK.ToString(),"User Register Successfully",dto);
                }
                else
                {
                    return new Response(HttpStatusCode.BadRequest.ToString(), "User Registration failed");
                }
            }
            else
            {
                return new Response(HttpStatusCode.BadRequest.ToString(), "User Registration failed");
            }
        }
    }
}
