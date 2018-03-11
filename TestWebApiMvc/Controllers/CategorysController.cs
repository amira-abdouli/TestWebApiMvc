using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TestWebApiMvc.Models;

namespace TestWebApiMvc.Controllers
{
    public class CategorysController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        [ResponseType(typeof(Products))]
        [Route("api/AllCategoryProducts/{id}")]
        public ICollection<Products> GetAllProducts(Guid id)
        {
            var cat = db.Categorys.Where(c => c.ID==id).Include(c =>c.Listproducts).SingleOrDefault();
            //cat = db.Categorys.Find(id);
            return cat.Listproducts;
            //  return db.Categorys.Find(id).products;
        }



        // GET: api/Categorys
        public IQueryable<Categorys> GetCategorys()
        {
            return db.Categorys.Include(c => c.Listproducts);
        }

        // GET: api/Categorys/5
        [ResponseType(typeof(Categorys))]
        public IHttpActionResult GetCategorys(Guid id)
        {
            Categorys categorys = db.Categorys.Find(id);
            if (categorys == null)
            {
                return NotFound();
            }

            return Ok(categorys);
        }

        // PUT: api/Categorys/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategorys(Guid id, Categorys categorys)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categorys.ID)
            {
                return BadRequest();
            }

            db.Entry(categorys).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategorysExists(id))
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

        // POST: api/Categorys
        [ResponseType(typeof(Categorys))]
        public IHttpActionResult PostCategorys(Categorys categorys)
        {
            if (categorys!=null)
            {
                if (categorys.Listproducts==null)
                categorys.Listproducts = new Collection<Products>();

            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categorys.Add(categorys);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CategorysExists(categorys.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = categorys.ID }, categorys);
        }



        // POST: api/Categorys
        [Route("api/AddProduct/{id}")]
        [ResponseType(typeof(Products))]
        public IHttpActionResult PostProduct(Products product , Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Categorys cat = db.Categorys.Find(id);
            cat.Listproducts.Add(product);
            //db.Categorys.Add(categorys);
            db.SaveChanges();
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateException)
            //{
            //    if (CategorysExists(categorys.ID))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return CreatedAtRoute("DefaultApi", new { id = product.ID }, product);
        }



        // DELETE: api/Categorys/5
        [ResponseType(typeof(Categorys))]
        public IHttpActionResult DeleteCategorys(Guid id)
        {
            Categorys categorys = db.Categorys.Find(id);
            if (categorys == null)
            {
                return NotFound();
            }

            db.Categorys.Remove(categorys);
            db.SaveChanges();

            return Ok(categorys);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategorysExists(Guid id)
        {
            return db.Categorys.Count(e => e.ID == id) > 0;
        }


        



    }
}