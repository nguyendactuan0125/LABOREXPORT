using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DAL
{
    public class Login :IDisposable
    {
        LABOREXPORT_DbContext db = null;
        public Login()
        {
            db = new LABOREXPORT_DbContext();
        }
        
        public User LoginSystem(string username, string password)
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
