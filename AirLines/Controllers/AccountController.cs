using AirLines.Data;
using AirLines.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace AirLines.Controllers
{
    public class AccountController : Controller
    {
        private readonly AirLineDbContext _context;
        public AccountController(AirLineDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Name")))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> IsEmailExsist(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                    var finduser = await _context.User!.FirstOrDefaultAsync(x => x.Email == email);
                    if (finduser != null)
                    {
                        return Ok("Exist");
                    }
            }
            return Ok("NotExist");
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            var Isemail= _context.User!.FirstOrDefault(u=>u.Email==user.Email);
            if(Isemail != null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _context.User!.Add(user);
                _context.SaveChanges();
                var role = _context.Role!.FirstOrDefault(r => r.Id == user.RoleId);
                if (role != null)
                {
                    bool send=EmailSend(user.Email!);
                    if (role.Name == "Admin")
                    {
                        return RedirectToAction("Login");
                    }
                    else if (role.Name == "Operator")
                    {
                        return RedirectToAction("Login");
                    }
                }
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public async Task<IActionResult> Login(string Email,string password)
        {
            var Isuser=await _context.User!.FirstOrDefaultAsync(u=>u.Email==Email && u.Password==password);
            if (Isuser != null && Isuser.Status!="Pending" && Isuser.Status!="Reject")
            {
                HttpContext.Session.SetString("Name", Isuser.Email!);
                var rol=_context.Role!.FirstAsync(r => r.Id==Isuser.RoleId).Result;
                HttpContext.Session.SetInt32("RoleId", Isuser.RoleId);
                HttpContext.Session.SetString("role", rol.Name!);
                var role =await _context.Role!.FirstOrDefaultAsync(r => r.Id == Isuser.RoleId);
                if (role!.Name == "Admin")
                {
                    return RedirectToAction("Index", "Home", new { area = "Home" });
                }
                else if(role!.Name == "Operator")
                {
                    return RedirectToAction("Index", "Home", new { area = "Home" });
                }
            }
            else
            {
                ViewData["loginInvalid"] = "Invalid Credentials";
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Name")))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        private static bool EmailSend(string username)
        {
            string from = "ytak989@gmail.com";
            string password = "cvgcycykgkauawnk";
            string subject = "Welcome  Dear Customer";
            string body = $"{username}\n<h1>Thank you for the registration. Please login using the url"+ "'<a href=\"https://localhost:7421/Account/Login\" </h1>\n'";
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(from);
                message.To.Add(new MailAddress(username));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(from, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
