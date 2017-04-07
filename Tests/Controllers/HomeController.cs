﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Tests.Models;
using Tests.Models.BLL;

namespace Tests.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            IEnumerable<IncomeInfo> incomeTypes = XDocument.Load(Server.MapPath("~/Resources/IncomeOptions.xml")).Element("IncomeTypes")
                       .Descendants("IncomeType")
                       .Select(x => new IncomeInfo
                       {
                           IncomeTitle = x.Element("Name").Value,
                           IncomeType = x.Element("Type").Value

                       });
            //ViewBag.LoanTypes = loanTypes;
           
            //Note : you can bind same list from database  
            

            
            return Json(incomeTypes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowStudents()
        {
            var lstStudents = DAL.GetAllStudents();
            return View(lstStudents);
        }

        public ActionResult NewStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewStudent(Student s)
        {
            if (ModelState.IsValid)
            {
                DAL.SaveStudent(s);
                return RedirectToAction("ShowStudents");
            }
            return View(s);
        }


    }
}