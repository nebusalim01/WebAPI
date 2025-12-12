using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAPI;
using System.Net.Http;

namespace WebAPI.Controllers
{
    public class MVCController : Controller
    {
        private WebAPIEntities db = new WebAPIEntities();

        // GET: MVC
        public ActionResult Index()
        {
            //return View(db.student_table.ToList());
            IEnumerable<student_table> students = null;
            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63809/api/API/");
                var responseTask = client.GetAsync("getAllDetails");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<student_table>>();
                    readTask.Wait();
                    students = readTask.Result;
                }
                else
                {
                    students = Enumerable.Empty<student_table>();
                }
            }
            return View(students);
        }

        // GET: MVC/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //student_table student_table = db.student_table.Find(id);
            //if (student_table == null)
            //{
            //    return HttpNotFound();
            //}
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                student_table student = null;
                using(var client =new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:63809/api/API/");
                    var responseTask = client.GetAsync($"getid_studenttable/{id}");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<student_table>();
                        readTask.Wait();
                        student = readTask.Result;
                    }
                    else
                    {
                        student = new student_table();
                    }                    
                }
                return View(student);
            }            
        }

        // GET: MVC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MVC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Age,Mark")] student_table student_table)
        {
            if (ModelState.IsValid)
            {
                //db.student_table.Add(student_table);
                //db.SaveChanges();
                using(var client=new HttpClient())//'using' is used to create the instance of the class HttpClient for the particular operation.after the execution of the code the memory optained for the object will deallocated. 
                {
                    client.BaseAddress = new Uri("http://localhost:63809/api/API/");//API LOCALHOST URL
                    var postTast = client.PostAsJsonAsync<student_table>("post_studenttable", student_table);
                    postTast.Wait();
                    var result = postTast.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(student_table);
            }
            return View(student_table);
        }

        // GET: MVC/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //student_table student_table = db.student_table.Find(id);
            //if (student_table == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(student_table);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                student_table student = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:63809/api/API/");
                    var responseTask = client.GetAsync($"getid_studenttable/{id}");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<student_table>();
                        readTask.Wait();
                        student = readTask.Result;
                    }
                    else
                    {
                        student = new student_table();
                    }
                }
                return View(student);


            }
        }

        // POST: MVC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,Mark")] student_table student_table)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:63809/api/API/");
                    var postTask = client.PutAsJsonAsync<student_table>($"put_studenttable/{student_table.Id}", student_table);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }

        // GET: MVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //student_table student_table = db.student_table.Find(id);
            //if (student_table == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(student_table);

            student_table student = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63809/api/API/");
                var responseTask = client.GetAsync($"getid_studenttable/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<student_table>();
                    readTask.Wait();
                    student = readTask.Result;
                }
                else
                {
                    student = new student_table();
                }
            }
            return View(student);
        }

        // POST: MVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //student_table student_table = db.student_table.Find(id);
            //db.student_table.Remove(student_table);
            //db.SaveChanges();

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63809/api/API/");
                var postTask = client.DeleteAsync($"delete_studenttable/{id}");
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Delete");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
