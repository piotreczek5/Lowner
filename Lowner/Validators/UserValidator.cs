using Lowner.Models;
using Lowner.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowner.Validators
{
    public class UserValidator
    {
        private readonly IUserRepository _userRepository;

        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool AddUserIsValid(User user, out string errorString)
        {
            errorString = String.Empty;

            if (user.Name == null)
            {
                errorString += "Could not add user. You must provide username.";
            }

            if (_userRepository.GetUsers().Contains(user))
            {
                errorString += "Could not add user. User already exists.";
            }

            return errorString == String.Empty ? false : true;
        }
    }
}
