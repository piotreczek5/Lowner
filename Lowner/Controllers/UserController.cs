using Lowner.Models;
using Lowner.Repositories;
using Lowner.Validators;
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
        private readonly UserValidator _userValidator;


        public UserController(IUserRepository userRepository, UserValidator userValidator)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
        }

        [HttpPost]
        [Route("AddUser")]
        public ActionResult<User> AddUser(User user)
        {
            string errorString;

            if (_userValidator.AddUserIsValid(user, out errorString))
            {
                return BadRequest(new JsonResult(errorString));
            }
            
            _userRepository.AddUser(user);

            return Ok(user);
        }

       

    }
}
