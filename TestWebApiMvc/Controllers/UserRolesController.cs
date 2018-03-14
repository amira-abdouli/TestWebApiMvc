using BLL1.Models;
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

namespace TestWebApiMvc.Controllers
{
    public class UserRolesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/UserRoles
        public IQueryable<UserRole> GetUserRoles()
        {
            return db.UserRoles;
        }

        // GET: api/UserRoles/5
        [ResponseType(typeof(UserRole))]
        public IHttpActionResult GetUserRole(Guid id)
        {
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return NotFound();
            }

            return Ok(userRole);
        }

        // PUT: api/UserRoles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserRole(Guid id, UserRole userRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userRole.ID)
            {
                return BadRequest();
            }

            db.Entry(userRole).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRoleExists(id))
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

        // POST: api/UserRoles
        [ResponseType(typeof(UserRole))]
        public IHttpActionResult PostUserRole(UserRole userRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserRoles.Add(userRole);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserRoleExists(userRole.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userRole.ID }, userRole);
        }

        // DELETE: api/UserRoles/5
        [ResponseType(typeof(UserRole))]
        public IHttpActionResult DeleteUserRole(Guid id)
        {
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return NotFound();
            }

            db.UserRoles.Remove(userRole);
            db.SaveChanges();

            return Ok(userRole);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserRoleExists(Guid id)
        {
            return db.UserRoles.Count(e => e.ID == id) > 0;
        }
    }
}