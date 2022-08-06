using EFCore.Models;
using ExemploMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;


namespace ExemploMVC.Controllers
{
    public class UpskillController : Controller
    {
        public NorthwindContext Context { get; }

        public UpskillController(NorthwindContext db)
        { 
            Context = db;
        }
        
        public IActionResult Index()
        {
            DateModel dm = new DateModel();
            dm.DateNow = DateTime.Now;
            return View(dm);
        }
    }
}
