using Microsoft.AspNetCore.Mvc;
using Tiki_Web_Project.Data;
using Tiki_Web_Project.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Tiki_Web_Project.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Dashboard()
        {
            // Kiểm tra nếu người dùng chưa đăng nhập hoặc không phải admin
            if (!User.Identity.IsAuthenticated || User.FindFirst(ClaimTypes.Role)?.Value != "0")  // Role = 0 là Admin
            {
                return RedirectToAction("Login", "Account");  // Chuyển hướng đến trang login
            }

            // Nếu người dùng là admin, thực hiện hành động cho phép
            int shopCount = _context.Sellers.Count();     // Đếm số lượng shop trong database
            int userCount = _context.Users.Count();       // Đếm số lượng user trong database
            int productCount = _context.Products.Count(); // Đếm số lượng product trong database

            // Truyền dữ liệu vào View
            ViewBag.ShopCount = shopCount;
            ViewBag.UserCount = userCount;
            ViewBag.ProductCount = productCount;

            return View();
        }

        [HttpGet]
        public IActionResult GetCounts()
        {
            // Đếm số lượng shop, user và product
            int shopCount = _context.Sellers.Count();
            int userCount = _context.Users.Count();
            int productCount = _context.Products.Count();

            // Trả về kết quả dưới dạng JSON
            return Json(new
            {
                shopCount = shopCount,
                userCount = userCount,
                productCount = productCount
            });
        }

        public IActionResult UserManagement()
        {
            var users = _context.Users.ToList();
            var mainViewModels = new HomeViewModel
            {
                Users = users,
            };
            return View(mainViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user, IFormFile Avatar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid form data.");
            }


            user.Created_At = DateTime.Now;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Xử lý avatar (nếu có)
            string avatarPath = null;
            if (Avatar != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/AVT/" + user.UserID);
                Directory.CreateDirectory(uploadsFolder); // Đảm bảo thư mục tồn tại
                avatarPath = Path.Combine(uploadsFolder, Avatar.FileName);

                using (var fileStream = new FileStream(avatarPath, FileMode.Create))
                {
                    await Avatar.CopyToAsync(fileStream);
                }

                user.Avatar = $"{Avatar.FileName}"; 
            }

            // Lưu dữ liệu vào database
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("User added successfully.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public IActionResult GetUserDetails(int userID)
        {
            // Ghi log để kiểm tra xem phương thức được gọi với userID nào
            Console.WriteLine($"GetUserDetails called with userID: {userID}");

            // Tìm người dùng theo userID
            var user = _context.Users.FirstOrDefault(u => u.UserID == userID);

            // Kiểm tra nếu không tìm thấy người dùng
            if (user == null)
            {
                Console.WriteLine($"User with ID {userID} not found.");
                return NotFound(new { message = "User not found." });
            }

            // Nếu tìm thấy người dùng, ghi log thông tin người dùng
            Console.WriteLine($"User found: {user.Username}");

            // Trả về thông tin người dùng dưới dạng JSON
            return Json(new
            {
                UserID = user.UserID,
                Username = user.Username,
                Name = user.Name,
                Phone = user.Phone,
                Email = user.Email,
                Address = user.Address,
                Birthday = user.Birthday?.ToString("yyyy-MM-dd"),
                Nationality = user.Nationality,
                Role = user.Role,
                Gender = user.Gender
            });
        }

        [HttpPost]
        public IActionResult UpdateUserDetails([FromBody] User updatedUser)
        {
            // Tìm người dùng trong database theo UserID
            var user = _context.Users.FirstOrDefault(u => u.UserID == updatedUser.UserID);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            // Cập nhật thông tin người dùng
            user.Username = updatedUser.Username;
            user.Name = updatedUser.Name;
            user.Phone = updatedUser.Phone;
            user.Email = updatedUser.Email;
            user.Address = updatedUser.Address;
            user.Birthday = updatedUser.Birthday;
            user.Nationality = updatedUser.Nationality;
            user.Role = updatedUser.Role;
            user.Gender = updatedUser.Gender;

            user.Updated_At = DateTime.Now;

            // Lưu thay đổi vào database
            _context.SaveChanges();

            return Json(new { success = true, message = "User updated successfully." });
        }

    }
}
