using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lowner.Models;

namespace Lowner.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        void UpdateUser(User user);
        
    }
}
