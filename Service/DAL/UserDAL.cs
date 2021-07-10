using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DAL
{
    public class UserDAL
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
       
    }
}
