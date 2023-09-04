using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer
{
    public class UsersModel
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string securityQuestion { get; set; }
        public string securityAnswer { get; set; }
        public DateTime dateOfRegister { get; set; }
    }
}
