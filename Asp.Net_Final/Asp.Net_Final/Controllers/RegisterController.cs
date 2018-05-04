using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.Net_Final.Models;
using Asp.Net_Final.ViewModels;

namespace Asp.Net_Final.Controllers
{
    public class RegisterController : Controller
    {
        Entities db = new Entities();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Reader curReader , ForForm imgpass)
        {
            var conPass = imgpass.ConfirmPassword;
            var Image = imgpass.image;

            if (curReader.Name == null || curReader.Lastname == null || curReader.Email == null || curReader.Password == null || conPass == null)
            {
                ViewBag.Error = "Sorry , you must be fill all fields";
                return View();
            }
            else if (db.Readers.Any(rd => rd.Email == curReader.Email))
            {
                ViewBag.Error = "Sorry , this email alredy exists";
                return View();
            }
            else if (curReader.Password != conPass)
            {
                ViewBag.Error = "Sorry , Password and Confirm Password is NOT the same";
                return View();
            }
            else
            {
                //Image begin
                if (Image != null)
                {
                    if (Image.ContentLength <= 2 * 1024 * 1024)
                    {
                        if (Image.ContentType.ToLower() == "image/jpg" ||
                            Image.ContentType.ToLower() == "image/png" ||
                            Image.ContentType.ToLower() == "image/jpeg" ||
                            Image.ContentType.ToLower() == "image/gif")
                        {
                            var originalname = Path.GetFileName(Image.FileName);
                            string fieldID = Guid.NewGuid().ToString().Replace("-", "");
                            string filename = fieldID + originalname;
                            var path = Path.Combine(Server.MapPath("~/Public/images/"), filename);
                            Image.SaveAs(path);
                            Reader newReader = new Reader();
                            newReader.Name = curReader.Name;
                            newReader.Lastname = curReader.Lastname;
                            newReader.Email = curReader.Email;
                            newReader.Password = curReader.Password;
                            newReader.Status = 1;
                            newReader.Image = filename;
                            db.Readers.Add(newReader);
                            db.SaveChanges();
                            return RedirectToAction("Index", "Login");

                        }
                        else
                        {
                            ViewBag.Error = "Please upload image format";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Size can't be more than 2 mb";
                        return View();

                    }
                }
                //Image end 
                else
                {
                    db.Readers.Add(new Reader
                    {
                        Name = curReader.Name,
                        Lastname = curReader.Lastname,
                        Image = null,
                        Email = curReader.Email,
                        Password = curReader.Password,
                        Status = 1
                    });
                    db.SaveChanges();
                    return RedirectToAction("Index", "Login");
                }
            }



        }
    }
}