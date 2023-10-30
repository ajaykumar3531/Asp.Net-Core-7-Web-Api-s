using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApi.DLayer.Entity;

namespace WebApi.DLayer.Interfaces
{
    public interface IDataAccess<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(object id);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        Task<bool> saveChnagesAsync();
        Task<string> FindByEmailAsync(string email);
        Task<bool> FindByPasswordAsync(string password,string email,string id);
        Task<AspNetUser> FindByPasswordEmailAndIdAsync(string password, string email, string id);
    }
}
