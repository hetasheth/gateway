using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.Models;
using PMS.Web.Models;
using log4net;
using AutoMapper;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace PMS.Web.Controllers
{
    public class ProductController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(ProductController));

        HttpClient client;
        Uri baseAddress = new Uri("http://localhost:27170/api");

        public ProductController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: Product
        public ActionResult Index(string colName="Category",string sortOrder="asc", int noOfRecords = 5, int pageNo = 1, String search = "", bool aSearch = false)
        {
            try
            {
                ViewBag.ActiveMenu = "Product List";
                ViewBag.colName = colName;
                ViewBag.sortOrder = sortOrder;
                ViewBag.search = search;
                ViewBag.aSearch = aSearch;
                ViewBag.noOfRecords = noOfRecords;
                ViewBag.pageNo = pageNo;
                List<Product> pList = new List<Product>();
                HttpResponseMessage response;
                response = client.GetAsync(client.BaseAddress + "/ProductWebAPI").Result;
                if (response.IsSuccessStatusCode)
                {
                    string res = response.Content.ReadAsStringAsync().Result;
                    if (response.Headers.Contains("pageData"))
                    {
                        ViewBag.noOfPages = Convert.ToInt32(response.Headers.GetValues("pageData").FirstOrDefault());
                    }
                    pList = JsonConvert.DeserializeObject<List<Product>>(res);
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
                    ViewBag.noOfPages = NoOfPages;
                    pList = pList.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();

                    return View(pList);
                }
                return View(pList);
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View();
            }
            
        }

        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "Add Product";
            return View("Create", new ProductVM());
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ActiveMenu = "Add Product";
            try
            {
                Product product = new Product();
                ProductVM productVM = new ProductVM();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/ProductWebAPI/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<Product>(data);
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductVM>()
                        .ForMember(x=>x.SmallImage,y=>y.Ignore())
                        .ForMember(x=>x.LargeImage,y=>y.Ignore()));
                    var mapper = new Mapper(config);
                    productVM = mapper.Map<ProductVM>(product);
                    TempData["SmallImage"] = product.SmallImage;
                    TempData["LargeImage"] = product.LargeImage;
                }
                return View("Create", productVM);
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult CreateOrEdit(ProductVM productVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product product = new Product();
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductVM, Product>());
                    var mapper = new Mapper(config);
                    product = mapper.Map<Product>(productVM);

                    if (productVM.SmallImage != null)
                    {
                        string smImg = Path.GetFileName(productVM.SmallImage.FileName);
                        productVM.SmallImage.SaveAs(Server.MapPath("~/Images/SmallImages/" + smImg));
                        product.SmallImage = smImg;
                    }
                    else
                    {
                        product.SmallImage = TempData["SmallImage"].ToString();
                    }

                    if (productVM.LargeImage != null)
                    {
                        string lgImg = Path.GetFileName(productVM.LargeImage.FileName);
                        productVM.LargeImage.SaveAs(Server.MapPath("~/Images/LargeImages/" + lgImg));
                        product.LargeImage = lgImg;
                    }
                    else
                    {
                        product.LargeImage = TempData["LargeImage"].ToString();
                    }

                    string data = JsonConvert.SerializeObject(product);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    if (product.ProductID == 0)
                    {
                        HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/ProductWebAPI/Post", content).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/ProductWebAPI/" + product.ProductID, content).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }

                }
                return View("Create");
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View("Create");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/ProductWebAPI/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["toast"] = "Product Successfully Deleted...";
                    TempData["type"] = "success";
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            try
            {
                if (formCollection.Count > 0)
                {
                    string[] lst = formCollection["ID"].Split(new char[] { ',' });
                    string data = JsonConvert.SerializeObject(lst);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/ProductWebAPI/DeleteMultiple", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["toast"] = "Products Successfully Deleted...";
                        TempData["type"] = "success";
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["toast"] = "No Product is Selected";
                    TempData["type"] = "error";
                    return RedirectToAction("IndexSort");
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View();
            }
        }
    }
}