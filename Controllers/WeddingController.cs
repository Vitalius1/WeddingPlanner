using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WeddingPlanner.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingContext _context;

        public WeddingController(WeddingContext context)
        {
            _context = context;
        }
        

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            // Checking if user is logged in to access the page.
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["NiceTry"] = "You need to be logged in to view account info!";
                return RedirectToAction("LoginPage", "Home");
            }
            List<Wedding> Weddings = _context.Weddings.Include(w => w.Guests).ToList();
            ViewBag.Weddings = Weddings;
            ViewBag.UserId = (int)HttpContext.Session.GetInt32("UserId");
            return View("Dashboard");
        }

        [HttpGet]
        [Route("planner")]
        public IActionResult Planner()
        {
            // Checking if user is logged in to access the page.
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["NiceTry"] = "You need to be logged in to view account info!";
                return RedirectToAction("LoginPage", "Home");
            }
            return View("Planner");
        }

// ============================================================================================================
        [HttpPost]
        [Route("plan")]
        public IActionResult CreatePlan(WeddingViewModel NewPlan)
        {
            if(ModelState.IsValid)
            {
                Wedding wedding = new Wedding
                {
                    BrideName = NewPlan.BrideName,
                    GroomName = NewPlan.GroomName,
                    Date = Convert.ToDateTime(NewPlan.Date),
                    Address = NewPlan.Address,
                    Creator = (int)HttpContext.Session.GetInt32("UserId"),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Weddings.Add(wedding);
                _context.SaveChanges();
                Attendance attendance = new Attendance
                {
                    UserId = (int)HttpContext.Session.GetInt32("UserId"),
                    WeddingId = wedding.WeddingId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Attendances.Add(attendance);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Wedding");
            }
            return View("Planner");
        }

// ==================================Actions we can do=========================================================
        [HttpGet]
        [Route("delete/{wedID}")]
        public IActionResult Delete(int wedID)
        {
            Wedding wedToDelete = _context.Weddings.SingleOrDefault(w => w.WeddingId == wedID);
            _context.Weddings.Remove(wedToDelete);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "Wedding");
        }
//-------------------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("rsvp/{wedID}")]
        public IActionResult RSVP(int wedID)
        {
            Attendance attendance = new Attendance
                {
                    UserId = (int)HttpContext.Session.GetInt32("UserId"),
                    WeddingId = wedID,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "Wedding");
        }
//-------------------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("unrsvp/{wedID}")]
        public IActionResult UnRSVP(int wedID)
        {
            Attendance AttndToDelete = _context.Attendances.SingleOrDefault(a => a.WeddingId == wedID && a.UserId == (int)HttpContext.Session.GetInt32("UserId"));
            _context.Attendances.Remove(AttndToDelete);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "Wedding");
        }
//-------------------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("wedInfo/{wedID}")]
        public IActionResult WeddingInfo(int wedID)
        {
            Wedding TheWedding = _context.Weddings.Include(w => w.Guests).ThenInclude(u => u.User).SingleOrDefault(w => w.WeddingId == wedID);
            ViewBag.Wedding = TheWedding;
            ViewBag.UserId = (int)HttpContext.Session.GetInt32("UserId");
            return View("Wedding");
        }
    }
}
