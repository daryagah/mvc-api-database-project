﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment4.Models;
using Assignment4.APIHandlerManager;
using Assignment4.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Assignment4.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext dbContext;
        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DataView()
        {
            APIHandler webHandler = new APIHandler();
            HospitalData result = webHandler.GetHospitals();
            
            foreach(var item in result.data)
            {
                dbContext.Hospitals.Add(item);
            }
            dbContext.SaveChanges();
            return View(result);
        }

        public ActionResult Table()
        {
            IQueryable<Hospital> Hosp = dbContext.Hospitals
                                                 .Where(h => h.provider_state == "FL");
            return View(Hosp);
        }
        //public async Task<IActionResult> Table()
        //{
        //    return View(await dbContext.Hospitals.ToListAsync());
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
