using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using log4net;
using PMSApplication.Models;
using System.Collections;

namespace PMSApplication.Controllers
{
    public class ProductController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(ProductController));

        HttpClient client;
        Uri baseAddress = new Uri("http://localhost:22128/api");

        public ProductController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: Product
        public ActionResult Index(String colName = "Category", String sortOrder = "asc", int noOfRecords = 5, int pageNo = 1, String search = "", bool aSearch = false)
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
                if (search != "")
                {
                    response = client.GetAsync(client.BaseAddress + "/ProductAPI/GetAdvanceList?colName=" + colName + "&sortOrder=" + sortOrder +
                        "&noOfRecords=" + noOfRecords + "&pageNo=" + pageNo + "&search=" + search + "&aSearch=" + aSearch).Result;
                }
                else
                {
                    response = client.GetAsync(client.BaseAddress + "/ProductAPI/GetAdvanceList?colName=" + colName + "&sortOrder=" + sortOrder +
                        "&noOfRecords=" + noOfRecords + "&pageNo=" + pageNo).Result;
                }
                if (response.IsSuccessStatusCode)
                {
                    string res = response.Content.ReadAsStringAsync().Result;
                    if (response.Headers.Contains("pageData"))
                    {
                        ViewBag.noOfPages = Convert.ToInt32(response.Headers.GetValues("pageData").FirstOrDefault());
                    }
                    pList = JsonConvert.DeserializeObject<List<Product>>(res);
                }
                return View("Index", pList);
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View();
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
                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/ProductAPI/DeleteMultiple", content).Result;
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

        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "Add Product";
            return View("Create", new ProductModel());
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ActiveMenu = "Add Product";
            try
            {
                Product product = new Product();
                ProductModel productModel = new ProductModel();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/ProductAPI/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<Product>(data);
                    productModel.ProductID = product.ProductID;
                    productModel.ProductName = product.ProductName;
                    productModel.Category = product.Category;
                    productModel.Price = product.Price;
                    productModel.Quantity = product.Quantity;
                    productModel.ShortDescription = product.ShortDescription;
                    productModel.LongDescription = product.LongDescription;
                    TempData["SmallImage"] = product.SmallImage;
                    TempData["LargeImage"] = product.LargeImage;
                }
                return View("Create", productModel);
            }
            catch (Exception ex)
            {
                log.Error("Exception : " + ex);
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult CreateOrEdit(ProductModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product product = new Product();
                    product.ProductID = productModel.ProductID;
                    product.ProductName = productModel.ProductName;
                    product.Category = productModel.Category;
                    product.Price = productModel.Price;
                    product.Quantity = productModel.Quantity;
                    product.ShortDescription = productModel.ShortDescription;
                    product.LongDescription = productModel.LongDescription;

                    if (productModel.SmallImage != null)
                    {
                        string smImg = Path.GetFileName(productModel.SmallImage.FileName);
                        productModel.SmallImage.SaveAs(Server.MapPath("~/Images/SmallImages/" + smImg));
                        product.SmallImage = smImg;
                    }
                    else
                    {
                        product.SmallImage = TempData["SmallImage"].ToString();
                    }

                    if (productModel.LargeImage != null)
                    {
                        string lgImg = Path.GetFileName(productModel.LargeImage.FileName);
                        productModel.LargeImage.SaveAs(Server.MapPath("~/Images/LargeImages/" + lgImg));
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
                        HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/ProductAPI/Post", content).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/ProductAPI/" + product.ProductID, content).Result;
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
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/ProductAPI/" + id).Result;
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
    }
}