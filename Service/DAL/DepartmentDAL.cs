using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DAL
{
    public class DepartmentDAL
    {
        LABOREXPORT_DbContext db = null;
        public DepartmentDAL()
        {
            db = new LABOREXPORT_DbContext();
        }
        public long Insert(Department entity)
        {
            db.Departments.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Department entity)
        {
            try
            {
                //var department = db.Departments.Find(entity.ID);
                //department.Name = entity.Name;
                //if (!string.IsNullOrEmpty(entity.Password))
                //{
                //    user.Password = entity.Password;
                //}
                //user.Address = entity.Address;
                //user.Email = entity.Email;
                //user.ModifiedBy = entity.ModifiedBy;
                //user.ModifiedDate = DateTime.Now;
                //db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
    }
}
