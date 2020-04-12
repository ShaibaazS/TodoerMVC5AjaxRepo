using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoerMVC5Ajax.Models;

namespace TodoerMVC5Ajax.Controllers
{
    public class TodoController : Controller
    {
        // GET: Todo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TodoHome()
        {
            return View("TodoHome");
        }

        public ActionResult TodoInsert()
        {
            Todo obj = new Todo();
            obj.Title = Request.Form["Title"];
            obj.Description = Request.Form["Description"];
            obj.Status = Request.Form["Status"];

            if (ModelState.IsValid)
            {
                // insert the customer object to database
                DataLayer.DataLayer dal = new DataLayer.DataLayer();
                dal.Todos.Add(obj); // in mmemory
                dal.SaveChanges(); // physical commit
            }
            // Access Data Layer
            DataLayer.DataLayer dal1 = new DataLayer.DataLayer();
            List<Todo> todos = dal1.Todos.ToList<Todo>();
            return Json(todos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TodoUpdate(int Id)
        {
            //return View("TodoHome");
            DataLayer.DataLayer dal = new DataLayer.DataLayer();

            Todo todo = (from t in dal.Todos
                         where t.Id == Id
                         select t).FirstOrDefault();
            todo.Title = Request.Form["txtTitle" + Id];
            todo.Description = Request.Form["txtDescription" + Id];
            todo.Status = Request.Form["txtStatus" + Id];
            dal.SaveChanges();
            // Access Data Layer
            DataLayer.DataLayer dal1 = new DataLayer.DataLayer();
            List<Todo> todos = dal1.Todos.ToList<Todo>();
            return Json(todos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TodoDelete(int Id)
        {
            DataLayer.DataLayer dal = new DataLayer.DataLayer();

            Todo todo = (from t in dal.Todos
                         where t.Id == Id
                         select t).FirstOrDefault();
            dal.Todos.Remove(todo);
            dal.SaveChanges();
            List<Todo> todos = dal.Todos.ToList<Todo>();
            return Json(todos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TodoSelect()
        {
            DataLayer.DataLayer dal = new DataLayer.DataLayer();
            List<Todo> todos = dal.Todos.ToList<Todo>();
            return Json(todos, JsonRequestBehavior.AllowGet);
        }
    }
}