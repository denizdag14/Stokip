using Microsoft.AspNetCore.Identity;
using POStock.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace POStock.Controllers
{
    public class UserController : Controller
    {

        HttpCookie userNameCookie = new HttpCookie("UserCookie");
        DbStockEntities db = new DbStockEntities();

        public ActionResult UserIndex()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetActiveUser()
        {
            string username = Request.Cookies["UserCookie"]?.Value;

            return Json(new { username = username }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            try
            {

                var existingUser = db.USERS.FirstOrDefault(u => u.Username == username);
                if (existingUser != null)
                {
                    return Json(new { success = false, message = "Bu kullanıcı adı zaten kullanılıyor." });
                }

                string salt = PasswordHasher.GenerateSalt(16);
                string hashedPassword = PasswordHasher.HashPassword(password, salt);

                USERS newUser = new USERS();
                newUser.Username = username;
                newUser.PasswordHash = hashedPassword;
                newUser.Salt = salt;
                db.USERS.Add(newUser);
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Kayıt sırasında bir hata oluştu: " + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            try
            {
                var user = db.USERS.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    string hashedPassword = PasswordHasher.HashPassword(password, user.Salt);

                    if (user.PasswordHash == hashedPassword)
                    {
                        userNameCookie.Value = user.Username;
                        Response.Cookies.Add(userNameCookie);
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Hatalı kullanıcı adı veya şifre." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Kullanıcı bulunamadı." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Giriş sırasında bir hata oluştu: " + ex.Message });
            }

        }

        public ActionResult Logout()
        {
            if (Request.Cookies["UserCookie"] != null)
            {
                userNameCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(userNameCookie);
            }
            return RedirectToAction("UserIndex", "User");
        }

        [HttpPost]
        public ActionResult UpdateCredentials(string username, string newUsername, string oldPassword, string newPassword)
        {
            try
            {
                var user = db.USERS.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    if (newUsername != "")
                    {
                        user.Username = newUsername;
                        userNameCookie.Value = user.Username;
                        Response.Cookies.Add(userNameCookie);
                    }
                    if (newPassword != "")
                    {
                        string hashedOldPassword = PasswordHasher.HashPassword(oldPassword, user.Salt);
                        if (user.PasswordHash == hashedOldPassword)
                        {
                            
                            string newSalt = PasswordHasher.GenerateSalt(16);
                            string newHashedPassword = PasswordHasher.HashPassword(newPassword, newSalt);


                            user.PasswordHash = newHashedPassword;
                            user.Salt = newSalt;
                            db.SaveChanges();

                            if(newUsername != "")
                            {
                                return Json(new { success = true, message = "Kullanıcı adı ve Şifre başarıyla güncellendi." });
                            }
                            else
                            {
                                return Json(new { success = true, message = "Şifre başarıyla güncellendi." });
                            }                          
                        }
                        else
                        {
                            return Json(new { success = false, message = "Mevcut şifreniz hatalı." });
                        }
                    }
                    else
                    {
                        db.SaveChanges();
                        return Json(new { success = true, message = "Kullanıcı adı başarıyla güncellendi." });
                    }
                   
                }
                else
                {
                    return Json(new { success = false, message = "Kullanıcı bulunamadı." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Şifre değiştirme sırasında bir hata oluştu: " + ex.Message });
            }
        }

    }
}

public class PasswordHasher
{
    public static string HashPassword(string password, string salt)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Şifreyi ve tuzu birleştir
            string combinedString = password + salt;

            // Birleşik stringi byte dizisine dönüştür
            byte[] bytes = Encoding.UTF8.GetBytes(combinedString);

            // Hash değerini hesapla
            byte[] hashBytes = sha256Hash.ComputeHash(bytes);

            // Hash değerini string olarak dönüştür ve geri döndür
            return Convert.ToBase64String(hashBytes);
        }
    }

    public static string GenerateSalt(int length)
    {
        // Rastgele bir salt oluştur
        byte[] randomBytes = new byte[length];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomBytes);
        }
        // Rastgele byte dizisini string olarak dönüştür
        return Convert.ToBase64String(randomBytes);
    }
}