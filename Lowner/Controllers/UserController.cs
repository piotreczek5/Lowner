using Lowner.Models;
using Lowner.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowner.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("AddUser")]
        public ActionResult<User> AddUser(User user)
        {
            string errorString;

            if (UserIsValid(user, out errorString))
            {
                return BadRequest(errorString);
            }
            
            _userRepository.AddUser(user);

            return Ok(user);
        }

        private bool UserIsValid(User user, out string errorString)
        {
            errorString = String.Empty;

            if (user.Name == null)
            {
                errorString += "Could not add user. You must provide username.";
            }

            if (_userRepository.GetUsers().Contains(user))
            {
                errorString+= "Could not add user. User already exists.";
            }

            return errorString == String.Empty ? false : true;
        }


        [HttpPost]
        [Route("GetUsers")]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(_userRepository.GetUsers());
        }
    }
}
