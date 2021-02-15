using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PMS.BAL.Interface;
using PMS.Models;
using log4net;

namespace PMS.WebAPI.Controllers
{
    public class ProductWebAPIController : ApiController
    {
        ILog log = log4net.LogManager.GetLogger(typeof(ProductWebAPIController));
        private readonly IProductManager _productManager;

        public ProductWebAPIController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        // GET: api/ProductWebAPI
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_productManager.GetProducts());
        }

        // GET: api/ProductwebAPI/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(_productManager.GetProductById(id));
        }

        // POST: api/ProductWebAPI
        [Route("api/ProductWebAPI/Post")]
        [HttpPost]
        public IHttpActionResult Post(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string result = _productManager.Create(product);
                    if (result == "Success")
                        return Ok(result);
                    else
                        return NotFound();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return NotFound();
            }
        }

        // PUT: api/ProductWebAPI/5        
        [HttpPut]
        public IHttpActionResult Put(int id, Product product)
        {
            try
            {
                string result = _productManager.Update(id, product);
                if (result == "Success")
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return NotFound();
            }
        }

        // DELETE: api/ProductWebAPI/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                string result = _productManager.Delete(id);
                if (result == "Success")
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return NotFound();
            }
        }

        [Route("api/ProductWebAPI/DeleteMultiple")]
        [HttpPost]
        public IHttpActionResult DeleteMultiple(string[] ids)
        {
            try
            {
                string result = _productManager.DeleteMultiple(ids);
                if (result == "Success")
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return NotFound();
            }
        }
    }
}
