using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IUser
    {
        public List<Users> GetUserData(int userId);
        public bool AddUser(Users user);
        public bool UpdateUser(int userId, Users user);
        public bool DeleteUser(int userId);
    }
}
