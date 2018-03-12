using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
using System.Web.UI.WebControls;
using TestWebApiMvc.Models;

namespace TestWebApiMvc.Controllers
{
    [Authorize]
    public class ProductsController : ApiController
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/
        //public ProductsController(UserManager<ApplicationUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        public IQueryable<ProductDTO> GetProducts()
        {
            //var user = _userManager.FindByName(User.Identity.Name);
            //var claims = user.Claims;
            //var claims = User.Claims;
            var name = User.Identity.Name;
            var isAutorize = User.Identity.IsAuthenticated;
            var TypeAutorization = User.Identity.AuthenticationType;

            var productDTO = from p in db.Products
                             select new ProductDTO
                             {
                                 Address = p.Address,
                                 CategoryName = p.Category.Name,
                                 Name = p.Name
                             };
            return productDTO;
           
                             
            //return db.Products;
        }

        // GET: api/Products/5
        [ResponseType(typeof(Products))]
        public IHttpActionResult GetProducts(Guid id)
        {
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return NotFound();
            }
          
            return Ok(products);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProducts(Guid id, Products products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != products.ID)
            {
                return BadRequest();
            }

            db.Entry(products).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(Products))]
        public IHttpActionResult PostProducts(Products products)
        {
            Categorys cat1;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (products!=null)
            {
                if (products.IDCategory != null/* .ToString() != "00000000-0000-0000-0000-000000000000"*/)
                {
                     cat1 = db.Categorys.Find(products.IDCategory);
                    products.Category = cat1;
                    db.Products.Add(products);
                    if (cat1.Listproducts==null)
                    {
                        cat1.Listproducts = new Collection<Products>();
                    }
                    
                    cat1.Listproducts.Add(products);
                    db.Categorys.Add(cat1);
                    //db.SaveChanges();
                    db.Entry(cat1).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        if (ProductsExists(products.ID))
                        {
                            return Conflict();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                
            }
            return CreatedAtRoute("DefaultApi", new { id = products.ID }, products);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Products))]
        public IHttpActionResult DeleteProducts(Guid id)
        {
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return NotFound();
            }

            db.Products.Remove(products);
            db.SaveChanges();

            return Ok(products);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductsExists(Guid id)
        {
            return db.Products.Count(e => e.ID == id) > 0;
        }
    }
}