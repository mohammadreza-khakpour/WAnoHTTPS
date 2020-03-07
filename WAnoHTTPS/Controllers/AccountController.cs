using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WAnoHTTPS.Areas.Identity.Data;
using WAnoHTTPS.ViewModels;

namespace WAnoHTTPS.Controllers
{
    public class AccountController : Controller
    {
        UserManager<WAnoHTTPSUser> EalfaTestUserManager { get; set; }
        SignInManager<WAnoHTTPSUser> EalfaTestSignInManager { get; set; }
        public AccountController(UserManager<WAnoHTTPSUser> _usermanager, SignInManager<WAnoHTTPSUser> _signinmanager)
        {
            EalfaTestUserManager = _usermanager;
            EalfaTestSignInManager = _signinmanager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Succeeded()
        {
            return View();
        }
        public IActionResult Failed()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> RegisterConfirm(RegisterViewModel model)
        {
            WAnoHTTPSUser newUser = new WAnoHTTPSUser()
            {
                UserName = model.emailaddress_viewmodel,
                Email = model.emailaddress_viewmodel,
                FirstName = model.firstname_viewmodel,
                LastName = model.lastname_viewmodel
            };
            IdentityResult registeringStatus = await EalfaTestUserManager.CreateAsync(newUser, model.password_viewmodel);
            if (registeringStatus.Succeeded)
            {
                string newUser_token = await EalfaTestUserManager.GenerateEmailConfirmationTokenAsync(newUser);
                string href = Url.Action("EmailConfirm", "Account", new { newuserid = newUser.Id, newusertoken = newUser_token });
                string body =
                    $"Hello {newUser.UserName}<br/>" +
                    $"to confirm your account click on: <a href={href}>here</a> .<br/>" +
                    $"you would redirect to the website";
                MailMessage mailMessage = new MailMessage("info@ealfatest.ir", newUser.Email);
                mailMessage.Subject = "Confirm your EalfaTest account";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                SmtpClient smtp = new SmtpClient("mail.ealfatest.ir", 25);
                smtp.Credentials = new System.Net.NetworkCredential("info@ealfatest.ir", "uzIa4dtpf?cxljiwseqk");
                //smtp.EnableSsl = true;
                smtp.Send(mailMessage);
                TempData["msg"] = "برای شما ایمیل تایید فرستاده شد";
                ViewData["message"] = "برای شما ایمیل تایید فرستاده شد";
                return RedirectToAction("Succeeded");
            }
            else
            {
                TempData["msg"] = "مشکلی برای فرستادن ایمیل تایید بوجود آمده";
                return RedirectToAction("Failed");
            }
        }
        public async Task<IActionResult> EmailConfirm(string newuserid, string newusertoken)
        {
            WAnoHTTPSUser returnedUser = await EalfaTestUserManager.FindByIdAsync(newuserid);
            var status = await EalfaTestUserManager.ConfirmEmailAsync(returnedUser, newusertoken);
            if (status.Succeeded)
            {
                TempData["msg"] = "اکانت و ایمیل شما تایید شد";
                ViewData["message"] = " اکانت و ایمیل شما تایید شد. میتوانید وارد شوید";
                return RedirectToAction("Succeeded");
            }
            else
            {
                TempData["msg"] = "مشکلی در جریان تایید اکانت و ایمیل شما بوجود آمده";
                ViewData["message"] = "مشکلی در جریان تایید اکانت و ایمیل شما بوجود آمده. لطفا یکبار دیگر امتحان کنید";
                return RedirectToAction("Failed");
            }
        }

        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> LoginConfirm(LoginViewModel model)
        {
            
                WAnoHTTPSUser theUser = await EalfaTestUserManager.FindByNameAsync(model.email_as_username_viewmodel);
                if (theUser != null)
                {
                    var status = await EalfaTestSignInManager.PasswordSignInAsync(theUser.UserName, model.password_viewmodel, model.rememberme_viewmodel, true);
                    if (status.Succeeded)
                    {
                        TempData["msg"] = "به اکانت خود وارد شدید";
                        ViewData["message"] = "به اکانت خود وارد شدید";
                        return RedirectToAction("Succeeded");
                    }
                    else
                    {
                        TempData["msg"] = "مشکلی در جریان ورود به اکانت بوجود آمد";
                        ViewData["message"] = "مشکلی در جریان ورود به اکانت بوجود آمد. لطفا یکبار دیگر امتحان کنید";
                        return RedirectToAction("Failed");
                    }
                }
                else
                {
                    TempData["msg"] = "اکانتی با اطلاعات وارد شده وجود ندارد. دوباره تلاش کنید";
                    ViewData["message"] = "اکانتی با اطلاعات وارد شده وجود ندارد. دوباره تلاش کنید";
                    return RedirectToAction("Failed");
                }
        }
        public async Task<IActionResult> Logout()
        {
            await EalfaTestSignInManager.SignOutAsync();
            TempData["msg"] = "از اکانت خود خارج شدید";
            return RedirectToAction("Index", "Home");
        }
    }
}