using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Helpers
{
    public  class Helpers
    {
        //Validate errors
        public  void ValidateErrors(CreateUserDTO user, ref string errors)
        {
            if (string.IsNullOrEmpty(user.name))
                //Validate if Name is null
                errors = "The name is required";
            if (string.IsNullOrEmpty(user.email))
                //Validate if Email is null
                errors = errors + " The email is required";
            if (string.IsNullOrEmpty(user.address))
                //Validate if Address is null
                errors = errors + " The address is required";
            if (string.IsNullOrEmpty(user.phone))
                //Validate if Phone is null
                errors = errors + " The phone is required";
            if (string.IsNullOrEmpty(user.userType) ||
                !userValid(user.userType))
                //Validate if Phone is null
                errors = errors + " The user type is invalid";
        }
        public static bool userValid(string userType)
        {
            var uType = new List<string> { "Normal", "SuperUser", "Premium" };

            return uType.Contains(userType);

        }

        public  bool userDuplicate(User newUser,List<User>_users, ref string errors)
        {
            foreach (var user in _users)
            {
                if (user.Email == newUser.Email
                    ||
                    user.Phone == newUser.Phone)
                {
                    return true;
                }
                else if (user.Name == newUser.Name && user.Address == newUser.Address)
                {
                    errors = "The User is duplicated";
                    return true;

                }
            }
            return false;
        }

        public  decimal MoneyGif(string UserType, decimal money)
        {

            if (UserType == "Normal" && money > 100)
            {
                return money + (money * Convert.ToDecimal(0.12));
            }
            else if (UserType == "Normal" && money < 100 && money > 10)
            {
                return money + (money * Convert.ToDecimal(0.12));
            }
            if (UserType == "SuperUser" && money > 100)
            {
                return money + (money * Convert.ToDecimal(0.20));
            }
            if (UserType == "Premium" && money > 100)
            {
                return money + (money * 2);
            }

            return money;
        }

        public  string normalizeEmail(string email)
        {
            try
            {

                var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

                var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

                aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

                email = string.Join("@", new string[] { aux[0], aux[1] });

                return email;
            }
            catch
            {
                return "";
            }
        }

        public User nUser(CreateUserDTO cUser)
        {
            var newUser = new User
            {
                Name = cUser.name,
                Email = cUser.email,
                Address = cUser.address,
                Phone = cUser.phone,
                UserType = cUser.userType,
                Money = decimal.Parse(cUser.money)
            };

            return newUser;
        }

    }
}
