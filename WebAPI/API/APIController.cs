using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace WebAPI.API
{
    public class APIController : ApiController
    {
        WebAPIEntities objdb = new WebAPIEntities();
        // GET: api/API
        [HttpGet]
        [Route("api/API/getAllDetails")]
        public IHttpActionResult Get()
        {
            return Ok(objdb.student_table.ToList());
        }

        // GET: api/API/5
        [HttpGet]
        [Route("api/API/getid_studenttable/{id}")]
        public IHttpActionResult Get(int id)
        {
            student_table student = objdb.student_table.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        // POST: api/API
        [HttpPost]
        [Route("api/API/post_studenttable")]
        public IHttpActionResult Post([FromBody] student_table objcls)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            objdb.student_table.Add(objcls);
            objdb.SaveChanges();
            return Ok(200);
        }

        // PUT: api/API/5
        [HttpPut]
        [Route("api/API/put_studenttable/{id}")]
        public IHttpActionResult Put(int id, [FromBody]student_table objCls)
        {
            objdb.Entry(objCls).State = EntityState.Modified;
            objdb.SaveChanges();
            return Ok(200);
        }

        // DELETE: api/API/5
        [HttpDelete]
        [Route("api/API/delete_studenttable/{id}")]
        public IHttpActionResult Delete(int id)
        {
            student_table student = objdb.student_table.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            objdb.student_table.Remove(student);
            objdb.SaveChanges();
            return Ok(student);
        }
    }
}
