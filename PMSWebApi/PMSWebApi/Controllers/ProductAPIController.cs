using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using log4net;
using PMSWebApi.Models;
using System.Collections;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System.Web;

namespace PMSWebApi.Controllers
{
    public class ProductAPIController : ApiController
    {
        PMSystemDBEntities dbContext = new PMSystemDBEntities();
        ILog log = log4net.LogManager.GetLogger(typeof(ProductAPIController));

        // GET: api/ProductAPI
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(dbContext.Products.ToList());
        }

        // GET: api/ProductAPI/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(dbContext.Products.Find(id));
        }

        [Route("api/ProductAPI/GetAdvanceList")]
        [HttpGet]
        public IHttpActionResult GetAdvanceList(String colName = "Category", String sortOrder = "asc", int noOfRecords = 5, int pageNo = 1, String search = "", bool aSearch = false)        
        {
            try
            {
                List<Product> pList = dbContext.Products.ToList();
                if (colName == "Category")
                {
                    if (sortOrder == "asc")
                        pList = pList.OrderBy(x => x.Category).ToList();
                    else
                        pList = pList.OrderByDescending(x => x.Category).ToList();
                }
                else if (colName == "ProductName")
                {
                    if (sortOrder == "asc")
                        pList = pList.OrderBy(x => x.ProductName).ToList();
                    else
                        pList = pList.OrderByDescending(x => x.ProductName).ToList();
                }

                if (search != "")
                {
                    if (aSearch == true)
                        pList = pList.Where(x => x.ProductName.Equals(search) || x.Category.Equals(search) || x.Price.Equals(Convert.ToDecimal(search)) || x.ShortDescription.Equals(search)).ToList();
                    else
                        pList = pList.Where(x => x.ProductName.Equals(search) || x.Category.Equals(search)).ToList();
                }

                int NoOfRecordsPerPage = noOfRecords;
                int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(pList.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
                int NoOfRecordsToSkip = (pageNo - 1) * NoOfRecordsPerPage;
                pList = pList.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();
                
                HttpContext.Current.Response.Headers.Add("pageData", NoOfPages.ToString());

                //return Json(new { data = pList, pagination = new { NoOfRecordsPerPage, NoOfPages, NoOfRecordsToSkip } });
                return Ok(pList);
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return NotFound();
            }
        }

        // POST: api/ProductAPI
        [Route("api/ProductAPI/Post")]
        [HttpPost]
        public HttpResponseMessage Post(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbContext.Products.Add(product);
                    dbContext.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        // PUT: api/ProductAPI/5        
        [HttpPut]
        public HttpResponseMessage Put(int id, Product product)
        {
            try
            {
                Product pd = dbContext.Products.Find(id);
                if (pd != null)
                {
                    pd.ProductName = product.ProductName;
                    pd.Category = product.Category;
                    pd.Price = product.Price;
                    pd.Quantity = product.Quantity;
                    pd.ShortDescription = product.ShortDescription;
                    pd.LongDescription = product.ShortDescription;
                    pd.SmallImage = product.SmallImage;
                    pd.LargeImage = product.LargeImage;
                    dbContext.Entry(pd).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        // DELETE: api/ProductAPI/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Product pd = dbContext.Products.Find(id);
                if (pd != null)
                {
                    dbContext.Products.Remove(pd);
                    dbContext.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [Route("api/ProductAPI/DeleteMultiple")]
        [HttpPost]
        public HttpResponseMessage DeleteMultiple(string[] ids)
        {
            try
            {
                if (ids.Count() > 0)
                {
                    foreach (string pid in ids)
                    {
                        Product pd = dbContext.Products.Find(Convert.ToInt32(pid));
                        dbContext.Products.Remove(pd);
                        dbContext.SaveChanges();
                    }
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }
    }
}
