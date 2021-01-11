using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PMSApplication.Models
{
    public class ProductModel
    {
        [Display(Name = "Product ID")]
        [ScaffoldColumn(true)]
        public long ProductID { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Please Enter Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please Select Category")]
        public string Category { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please Enter Price")]
        [RegularExpression(@"^[1-9]\d{0,7}(?:\.\d{1,4})?$", ErrorMessage = "Please Enter Price(Integer/Decimal)")]
        public decimal Price { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Please Select Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please Enter Short Description")]
        [MinLength(8, ErrorMessage = "Short Description must contain atleast 10 characters"), MaxLength(200, ErrorMessage = "Short Description can not contain more than 200 characters")]
        public string ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        [Required(ErrorMessage = "Please Enter Long Description")]
        [MinLength(201, ErrorMessage = "Long Description must contain atleast 201 characters")]
        public string LongDescription { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please Upload Image")]
        public HttpPostedFileBase SmallImage { get; set; }

        [FileExtensions(Extensions = "jpg,jpeg,png,gif", ErrorMessage = "Only image file can be uploaded")]
        public string smImgPath
        {
            get
            {
                if (SmallImage != null)
                    return SmallImage.FileName;
                else
                    return "";
            }
        }

        [Display(Name = "Large Image")]
        [Required(ErrorMessage = "Please Upload Large Image")]
        public HttpPostedFileBase LargeImage { get; set; }

        [FileExtensions(Extensions = "jpg,jpeg,png,gif", ErrorMessage = "Only image file can be uploaded")]
        public string lgImgPath
        {
            get
            {
                if (LargeImage != null)
                    return LargeImage.FileName;
                else
                    return "";
            }
        }
    }
}