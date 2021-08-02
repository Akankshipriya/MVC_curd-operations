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
    public class DepartmentController : Controller
    {
        database_testEntities Obj = new database_testEntities();
        // GET: Department
        public ActionResult DepartmentCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DepartmentCreate(Department model)
        {

            Obj.Departments.Add(model);
            Obj.SaveChanges();
            ModelState.Clear();
            return View();
            
        }
        public ActionResult DepartmentList()
        {
            var res = Obj.Departments.ToList();
            return View(res);
        }

        public ActionResult DepartmentDelete(int id)
        {
            var res = Obj.Departments.Where(x => x.DeptID == id).FirstOrDefault();
            Obj.Departments.Remove(res);
            Obj.SaveChanges();

            var list = Obj.Departments.ToList();
            return View("DepartmentList", list);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = Obj.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                Obj.Entry(department).State = EntityState.Modified;
                Obj.SaveChanges();
                return RedirectToAction("DepartmentList");
            }
            return View(department);
        }

    }
}