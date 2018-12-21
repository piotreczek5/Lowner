using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lowner.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            User user = obj as User;

            if (user == null)
            {
                return false;
            }
            
            return (user.Name == Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
