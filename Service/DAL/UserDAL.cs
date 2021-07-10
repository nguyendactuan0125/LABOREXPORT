using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DAL
{
    public class UserDAL: IDisposable
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
    }
}
