using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApi.DLayer.Entity;
using WebApi.DLayer.Interfaces;

namespace WebApi.DLayer.Classes
{
    public class DataAccess<T> : IDataAccess<T> where T : class
    {
        private readonly IdentityDBContext _DbConetxt;

        public DataAccess(IdentityDBContext dbConetxt)
        {
            _DbConetxt = dbConetxt;
        }
        public void Add(T entity)
        {
            _DbConetxt.Set<T>().Add(entity);
        }

        public async Task<string> FindByEmailAsync(string email)
        {
            var user = await _DbConetxt.AspNetUsers.FirstOrDefaultAsync(e => e.Email == email);

            if (user != null)
            {
                return user.Email;
            }
            // Return null or an appropriate value if the user is not found.
            return null;
        }

        public async Task<bool> FindByPasswordAsync(string password,string email,string Id)
        {
            var user = await _DbConetxt.AspNetUsers.FirstOrDefaultAsync(x => x.Email == email && x.Id == Id);

            if(user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password,user.PasswordHash))
                {
                    return true;
                }
            }
            return false;
        }

      

        public async Task<List<T>> GetAll()
        {

            var data=await _DbConetxt.Set<T>().ToListAsync();
            return data;
        }

        public async Task<T> GetById(object id)
        {
            return await _DbConetxt.Set<T>().FindAsync(id);
        }

        public void Remove(T entity)
        {
            _DbConetxt.Set<T>().Remove(entity);
        }

        public async Task<bool> saveChnagesAsync()
        {
            return await _DbConetxt.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            _DbConetxt.Set<T>().Update(entity);
        }

        public async Task<AspNetUser> FindByPasswordEmailAndIdAsync(string password, string email, string id)
        {
            // You can implement your logic to retrieve the user from the database
            // based on the provided password, email, and ID.
            // You may use Entity Framework or any other data access method here.

            var user = await _DbConetxt.AspNetUsers.FirstOrDefaultAsync(x => x.Email == email && x.Id == id);

            if (user != null)
            {
                return user;
            }

            return null; // Return null if the user is not found or credentials do not match.
        }

    }
}
