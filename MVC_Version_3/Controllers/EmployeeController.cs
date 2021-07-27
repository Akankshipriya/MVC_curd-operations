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
    public class EmployeeController : Controller
    {
        database_testEntities Obj = new database_testEntities();
        // GET: Employee
        public ActionResult EmployeeCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeCreate(Employee model)
        {
            Obj.Employees.Add(model);
            Obj.SaveChanges();
            return View();
            
        }
        public ActionResult EmployeeList()
        {
            var res = Obj.Employees.ToList();
            return View(res);
        }

        public ActionResult EmployeeDelete(int id)
        {
            var res = Obj.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            Obj.Employees.Remove(res);
            Obj.SaveChanges();

            var list = Obj.Employees.ToList();
            return View("EmployeeList", list);
        }

        public ActionResult EmployeeEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = Obj.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeEdit([Bind(Include = "EmployeeId,DeptID,Name,DOJ,Mobile,Email,Address,Department,salary.SalaryAmount")] Employee employee)
        {

            if (ModelState.IsValid)
            {
              
                Obj.Entry(employee).State = EntityState.Modified;
                Obj.SaveChanges();
                return RedirectToAction("EmployeeList");
            }
            return View(employee);
        }
    }
}