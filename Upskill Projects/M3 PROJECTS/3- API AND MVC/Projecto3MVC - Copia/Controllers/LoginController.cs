using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projecto3MVC.Models;

namespace Projecto3MVC.Controllers
{
    public class LoginController : Controller
    {
      

        

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([Bind] Ad_login ad)
        {
            db logindb = new db();
            if (ad.Admin_id != null && ad.Ad_Password != null)
            { 
            
                int res = logindb.LoginCheck(ad);

                if (res == 1)
                {
                  
                    return View("/Views/Home/Index.cshtml");
                }
                return View();
            }
            return View();
            

        }

        //[HttpPost]
        //public  ActionResult Autherize(Projecto3MVC.Models.Ad_login userModel)
        //{
        //    using (NorthwindContext db = new NorthwindContext())
        //    {
        //        var userDetails = db.Ad_Login.Where(x => x.Admin_id == userModel.Admin_id && x.Ad_Password == userModel.Ad_Password).FirstOrDefault();
        //        if (userDetails == null)
        //        {
        //            userModel.LoginErrorMessage = "Wrong username or password.";
        //            return View("Index", userModel);
        //        }
        //    }
        //    return View();
        //}

    }
}
