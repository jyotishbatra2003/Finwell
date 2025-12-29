using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinwellLibrary.Models
{
    public class userModel
    {
       
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; } // in this application, store hashed passwords only
        public userModel(int userId, string userName, string password)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
        }
    }
}
