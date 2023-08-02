using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.DTO;
using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();         
        private  UserServices _userServices = new UserServices() ;
        private Helpers.Helpers helpers = new Helpers.Helpers();
        public UsersController()
        {
            _users = _userServices.GetUsers();
        }       
      
        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(CreateUserDTO cUser)
        {
            
            var errors = "";
            
            helpers.ValidateErrors(cUser, ref errors);

            if (!string.IsNullOrEmpty(errors))
                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };

            var newUser = helpers.nUser(cUser); 

            // total money
            newUser.Money = helpers.MoneyGif(newUser.UserType,newUser.Money);                      

            //Normalize email          

            newUser.Email = helpers.normalizeEmail(newUser.Email);

            if (string.IsNullOrEmpty(newUser.Email))
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = "Invalid email format"
                };
            }
          
            try
            {
                var errorDuplicated = "";
                var isDuplicated = helpers.userDuplicate(newUser,_users,ref errorDuplicated);
               

                if (!isDuplicated)
                {
                    Debug.WriteLine("User Created");

                    return new Result()
                    {
                        IsSuccess = true,
                        Errors = "User Created"
                    };
                }
                else
                {
                    Debug.WriteLine(errors);

                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = errors
                    };
                }
            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return new Result()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                };
            }

            return new Result()
            {
                IsSuccess = true,
                Errors = "User Created"
            };
        }    

    }
    
    
}
