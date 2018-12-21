using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Lowner.Context;
using Lowner.Models;

namespace Lowner.Repositories
{
    public class DbUserRepository : IUserRepository
    {
        private readonly LoanContext _dbContext;

        public DbUserRepository(LoanContext loanContext)
        {
            _dbContext = loanContext;
        }

        public void AddUser(User user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }

        public User GetUserById(int id)
        {
            return _dbContext.Users.Find(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users.AsEnumerable();
        }

        public void UpdateUser(User user)
        {
            _dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
