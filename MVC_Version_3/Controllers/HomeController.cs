using MVC_Version_3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC_Version_3.Controllers
{
    public class HomeController : Controller
    {
        database_testEntities obj = new database_testEntities();
        public ActionResult Index()
        {
            

            return View(obj.displaySortedData());
        }
        
        public ActionResult Delete(int id)
        {
            var res = obj.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            obj.Employees.Remove(res);
            obj.SaveChanges();

            var list = obj.displaySortedData().ToList();
            return View("Index", list);
        }





    }
}