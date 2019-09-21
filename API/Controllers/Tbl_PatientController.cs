using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    public class Tbl_PatientController : ApiController
    {
        private Clc_AppEntities db = new Clc_AppEntities();

        // GET: api/Tbl_Patient
        public IQueryable<Tbl_Patient> GetTbl_Patient()
        {
            return db.Tbl_Patient;
        }

        // GET: api/Tbl_Patient/5
        [ResponseType(typeof(Tbl_Patient))]
        public IHttpActionResult GetTbl_Patient(int id)
        {
            Tbl_Patient tbl_Patient = db.Tbl_Patient.Find(id);
            if (tbl_Patient == null)
            {
                return NotFound();
            }

            return Ok(tbl_Patient);
        }

        [ResponseType(typeof(Tbl_Patient))]
        [Route("api/Tbl_Patient/GetTbl_Patient/{Phone}/{Pswd}")]
        public IHttpActionResult GetTbl_Patient(string Phone, String Pswd)
        {
            var phone = Phone.ToString();
            var pswd = Pswd.ToString();
            Tbl_Patient ChkCredential = db.Tbl_Patient.Where(a => a.phone_No == phone && a.password == pswd).FirstOrDefault();
            if (ChkCredential != null)
            {
                return Ok(ChkCredential);
            }
            return NotFound();

        }

        // PUT: api/Tbl_Patient/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_Patient(int id, Tbl_Patient tbl_Patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Patient.patient_Id)
            {
                return BadRequest();
            }

            db.Entry(tbl_Patient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tbl_Patient
        [ResponseType(typeof(Tbl_Patient))]
        public IHttpActionResult PostTbl_Patient(Tbl_Patient tbl_Patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbl_Patient.Add(tbl_Patient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Patient.patient_Id }, tbl_Patient);
        }

        // DELETE: api/Tbl_Patient/5
        [ResponseType(typeof(Tbl_Patient))]
        public IHttpActionResult DeleteTbl_Patient(int id)
        {
            Tbl_Patient tbl_Patient = db.Tbl_Patient.Find(id);
            if (tbl_Patient == null)
            {
                return NotFound();
            }

            db.Tbl_Patient.Remove(tbl_Patient);
            db.SaveChanges();

            return Ok(tbl_Patient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_PatientExists(int id)
        {
            return db.Tbl_Patient.Count(e => e.patient_Id == id) > 0;
        }
    }
}