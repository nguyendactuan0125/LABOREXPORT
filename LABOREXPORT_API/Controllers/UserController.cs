<<<<<<< HEAD
﻿using LABOREXPORT_API.Models.Respond;
using Service.DAL;
using Service.Model;
using System;
=======
﻿using System;
>>>>>>> 28cb8d5c92f7ab534bc5629962f23dc457a59134
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
<<<<<<< HEAD
//using System.Web.Mvc;
=======
using System.Web.Mvc;
>>>>>>> 28cb8d5c92f7ab534bc5629962f23dc457a59134

namespace LABOREXPORT_API.Controllers
{
    public class UserController : ApiController
    {
<<<<<<< HEAD
        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("api/User/Register")]
        public IRespond Register([FromBody] User users)
        {
            IRespond _res = new IRespond();
            var _userDAL = new UserDAL();
            long _ID = _userDAL.Insert(users);
            if (_ID > 0)
            {
                _res.p_msg = "Đăng ký thành công";
                _res.p_result = "Suscess";
                _res.Data = null;
            }
            else
            {
                _res.p_msg = "Đăng ký thất bại";
                _res.p_result = "False";
                _res.Data = null;
            }
            return _res;
        }



        //[AllowAnonymous]
        //[HttpGet]
        //[Route("api/data/forall")]
        //public IHttpActionResult Get()
        //{
        //    return Ok("Now server time is: " + DateTime.Now.ToString());
        //}
        //[System.Web.Http.Authorize]
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/data/authenticate")]
        //public IHttpActionResult GetForAuthenticate()
        //{
        //    var identity = (ClaimsIdentity)User.Identity;
        //    return Ok("Hello " + identity.Name);
        //}
        //[System.Web.Http.Authorize(Roles = "admin")]
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/data/authorize")]
        //public IHttpActionResult GetForAdmin()
        //{
        //    var identity = (ClaimsIdentity)User.Identity;
        //    var roles = identity.Claims
        //                .Where(c => c.Type == ClaimTypes.Role)
        //                .Select(c => c.Value);
        //    return Ok("Hello " + identity.Name + " Role: " + string.Join(",", roles.ToList()));
        //}
=======
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/data/forall")]
        public IHttpActionResult Get()
        {
            return Ok("Now server time is: " + DateTime.Now.ToString());
        }
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/data/authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello " + identity.Name);
        }
        [System.Web.Http.Authorize(Roles = "admin")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/data/authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("Hello " + identity.Name + " Role: " + string.Join(",", roles.ToList()));
        }
>>>>>>> 28cb8d5c92f7ab534bc5629962f23dc457a59134
    }
}