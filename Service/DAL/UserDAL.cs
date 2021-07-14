using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DAL
{
<<<<<<< HEAD
    public class UserDAL
=======
    public class UserDAL: IDisposable
>>>>>>> 28cb8d5c92f7ab534bc5629962f23dc457a59134
    {
        LABOREXPORT_DbContext db = null;
        public UserDAL()
        {
            db = new LABOREXPORT_DbContext();
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
<<<<<<< HEAD
       
=======
        public User Login(string username, string password)
        {
            return db.Users.FirstOrDefault(user =>
            user.Login.Equals(username, StringComparison.OrdinalIgnoreCase)
            && user.Password == password);
        }
        public void Dispose()
        {
            db.Dispose();
        }
>>>>>>> 28cb8d5c92f7ab534bc5629962f23dc457a59134
    }
}
