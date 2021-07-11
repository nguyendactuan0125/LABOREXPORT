using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LABOREXPORT.Controllers
{
    public class LEUR01Controller : Controller
    {
        public ActionResult LEUR0101()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LEUR0110()
        {
            //return Redirect("/LEUR01/LEUR0110");
            return View();
        }
    }
}