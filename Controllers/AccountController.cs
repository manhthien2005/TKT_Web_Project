using Microsoft.AspNetCore.Mvc;
using Tiki_Web_Project.Data;
using Tiki_Web_Project.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Tiki_Web_Project.Services;

namespace Tiki_Web_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SendOTPtoEmail _emailService;

        private bool SendConfirmation = false;


        public AccountController(ApplicationDbContext context, SendOTPtoEmail emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // Registration GET
        public IActionResult Register()
        {
            return View();
        }

        // Registration POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                model.Created_At = DateTime.Now;
                model.Updated_At = DateTime.Now;


                _context.Users.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(model);
        }


        // Login GET
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u =>
                    u.Username == model.Username &&
                    u.Password == model.Password);

                if (user != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("UserID", user.UserID.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim("Email", user.Email ?? ""),
                    new Claim("Name", user.Name ?? ""),
                    new Claim("Phone", user.Phone ?? "")
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddDays(7)
                    };

                    // Đăng nhập bằng scheme mặc định (Cookies)
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

                    if (user.Role == 0)  // Admin
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }

                    return RedirectToAction("HomePage", "Home");  // Người dùng thông thường
                }

                ModelState.AddModelError("", "Invalid username or password");
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public IActionResult Index()
        {
            // Lấy thông tin từ Claims
            var username = User.Identity.Name; // Lấy tên người dùng
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

            // Lấy thông tin người dùng từ database dựa trên UserID hoặc Username
            var user = _context.Users.FirstOrDefault(u => u.UserID.ToString() == userId);

            var mainViewModels = new HomeViewModel
            {
                Users = new List<User> { user }
            };
            return View(mainViewModels);
        }

        [HttpPost]
        [Authorize]
        public IActionResult UpdateAccountInfo(User updatedUser)
        {
            // Lấy thông tin UserID từ Claims
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

            // Tìm người dùng trong cơ sở dữ liệu
            var user = _context.Users.FirstOrDefault(u => u.UserID.ToString() == userId);
            if (user == null)
            {
                // Nếu không tìm thấy người dùng, trả về lỗi
                return NotFound();
            }

            // Cập nhật thông tin người dùng
            user.Name = updatedUser.Name;          // Họ & Tên
            user.Username = updatedUser.Username;  // Nickname
            user.Birthday = updatedUser.Birthday;  // Ngày sinh
            user.Gender = updatedUser.Gender;      // Giới tính
            user.Nationality = updatedUser.Nationality; // Quốc tịch

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            // Chuyển hướng về trang thông tin cá nhân
            return RedirectToAction("Index");
        }


        public IActionResult UpdatePhone()
        {
            return View();
        }

        public IActionResult UpdateEmail()
        {
            return View();
        }

        // ForgotPassword GET
        public IActionResult ForgotPass()
        {
            return View();
        }

        // ForgotPassword POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPass(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "No user found with this email.");
                return View(model);
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            ViewBag.Email = model.Email;


            TempData["SuccessMessage"] = "Password recovery email sent. Please check your inbox.";
            return View();
        }



        public IActionResult OTP()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePass(string email)
        {
            // Lấy email từ TempData
            if (TempData["Email"] == null)
            {
                return RedirectToAction("ForgotPass"); // Quay lại ForgotPass nếu email không tồn tại
            }

            ViewBag.Email = TempData["Email"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePass(string email, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Không tìm thấy email.");
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu và xác nhận mật khẩu không khớp.");
                return View();
            }

            // Tìm user theo email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email không tồn tại trong hệ thống.");
                return View();
            }

            // Cập nhật mật khẩu
            user.Password = newPassword; // Lưu trực tiếp, không mã hóa (không khuyến khích)
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Mật khẩu đã được thay đổi thành công.";
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult SendOTP(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Json(new { success = false, message = "Email không hợp lệ." });
            }

            // Tạo mã OTP
            var otpCode = new Random().Next(1000, 9999).ToString();

            // Lưu OTP vào TempData để xác minh
            TempData["OTP"] = otpCode;

            // Gửi OTP qua email
            var subject = "Your OTP Code";
            var message = $"Your OTP code is: <strong>{otpCode}</strong>";
            _emailService.SendEmailAsync(email, subject, message);

            // Lưu email trong Claim
            var claims = new List<Claim>
            {
                new Claim("OTP_Email", email)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Json(new { success = true, message = "OTP đã được gửi thành công." });
        }




        [HttpPost]
        public IActionResult OTP(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email không được để trống.");
                return View("ForgotPass");
            }

            // Kiểm tra email có tồn tại trong database không
            var userExists = _context.Users.Any(u => u.Email == email);
            if (!userExists)
            {
                ModelState.AddModelError("", "Email không tồn tại. Vui lòng thử lại.");
                return View("ForgotPass");
            }

            // Gửi OTP và lưu email vào claim
            SendOTP(email);
            ViewBag.Email = email;

            return View("OTP");
        }



        [HttpPost]
        public IActionResult VerifyOTP(string[] otp)
        {
            // Lấy OTP người dùng nhập
            var otpCode = string.Join("", otp);

            // Lấy OTP từ TempData
            if (TempData["OTP"] != null && TempData["OTP"].ToString() == otpCode)
            {
                TempData["OTP"] = null; // Xóa OTP khỏi TempData sau khi xác minh

                // Lấy email từ claims
                var emailClaim = User.Claims.FirstOrDefault(c => c.Type == "OTP_Email");
                if (emailClaim == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy email trong phiên." });
                }

                TempData["Email"] = emailClaim.Value;

                // Redirect đến ChangePass và truyền email
                return RedirectToAction("ChangePass");
            }

            // Nếu OTP không khớp, trả về lỗi
            return Json(new { success = false, message = "OTP không chính xác. Vui lòng thử lại." });
        }
    }
}
