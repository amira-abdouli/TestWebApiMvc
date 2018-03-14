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
using BLL1.Models;

namespace TestWebApiMvc.Controllers
{
    public class UserRoleGruopsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/UserRoleGruops
        public IQueryable<UserRoleGruop> GetUserRoleGruops()
        {
            return db.UserRoleGruops;
        }

        [Route("api/RoleGruopJoinUsers/{id}")]
        // GET: api/RoleGruopJoinUsers
        public IQueryable<RoleGruopJoinUsers> GetRoleGruopJoinUsers(Guid id)
        {
            return db.RoleGruopJoinUsers.Where(c=>c.UserRoleGruopID==id);
        }

        // GET: api/UserRoleGruops/5
        [ResponseType(typeof(UserRoleGruop))]
        public IHttpActionResult GetUserRoleGruop(Guid id)
        {
            UserRoleGruop userRoleGruop = db.UserRoleGruops.Find(id);
            if (userRoleGruop == null)
            {
                return NotFound();
            }

            return Ok(userRoleGruop);
        }
        // PUT: api/UserRoleGruops/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserRoleGruop(Guid id, UserRoleGruop userRoleGruop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userRoleGruop.ID)
            {
                return BadRequest();
            }

            db.Entry(userRoleGruop).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRoleGruopExists(id))
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
        // POST: api/UserRoleGruops
        [ResponseType(typeof(UserRoleGruop))]
        public IHttpActionResult PostUserRoleGruop(UserRoleGruop userRoleGruop)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            userRoleGruop.RoleJoinRoleGruops = new List<RoleJoinRoleGruop>();
            db.UserRoleGruops.Add(userRoleGruop);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserRoleGruopExists(userRoleGruop.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userRoleGruop.ID }, userRoleGruop);
        }
        // POST: api/AddUserToGruop
        [Route("api/AddUserToGruop")]
        [ResponseType(typeof(RoleGruopJoinUsers))]
        public IHttpActionResult PostAddUserToGruop(RoleGruopJoinUsers roleGruopJoinUsers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RoleGruopJoinUsers.Add(roleGruopJoinUsers);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RoleGruopJoinUsersExists(roleGruopJoinUsers.UserRoleGruopID))
                    return Conflict();
                else
                    throw;
                
                //throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = roleGruopJoinUsers.UserRoleGruopID }, roleGruopJoinUsers);
        }
        // DELETE: api/UserRoleGruops/5
        [ResponseType(typeof(UserRoleGruop))]
        public IHttpActionResult DeleteUserRoleGruop(Guid id)
        {
            UserRoleGruop userRoleGruop = db.UserRoleGruops.Find(id);
            if (userRoleGruop == null)
            {
                return NotFound();
            }

            db.UserRoleGruops.Remove(userRoleGruop);
            db.SaveChanges();

            return Ok(userRoleGruop);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool UserRoleGruopExists(Guid id)
        {
            return db.UserRoleGruops.Count(e => e.ID == id) > 0;
        }
        private bool RoleGruopJoinUsersExists(Guid id)
        {
            return db.RoleGruopJoinUsers.Count(e => e.UserRoleGruopID == id) > 0;
        }
    }
}