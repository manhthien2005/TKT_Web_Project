using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace Tiki_Web_Project.Models
{
    [Table("Brands")]
    public class Brand
    {
        [Key]
        public int BrandID { get; set; }

        [Required]
        public string BrandName { get; set; }
    }

    [Table("Sellers")]
    public class Seller
    {
        [Key]
        public int SellerID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public byte Active { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Udpated_At { get; set; }

    }

    [Table("Products")]
    public class Product
    {
        [Key]
        public string ProductID { get; set; }

        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock {  get; set; }
        public int SubID { get; set; }
        public int? BrandID { get; set; }
        public int SellerID { get; set; }
        public byte Active {  get; set; }
        public string? Keywords { get; set; }
        public int Discount { get; set; }
        public string? Description { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }

        [ForeignKey("SubID")]
        public SubCategorie SubCategory { get; set; }

        public ICollection<Image> Images { get; set; }
    }

    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<SubCategorie> SubCategories { get; set; }

    }

    [Table("SubCategories")]
    public class SubCategorie
    {
        [Key]
        public int SubID { get; set; }

        [Required]
        public int CategoryID { get; set; }
        public string SubName { get; set; }
        public string? SubImg { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }  // Add this line

        public ICollection<Product> Products { get; set; }
    }

    [Table("Services")]
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }

        [Required]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
    }

    [Table("QuickLinks")]
    public class QuickLink
    {
        [Key]
        public int QuickLinkID { get; set; }

        [Required]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
    }

    [Table("Users")]
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string? Avatar { get; set; }

        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public string? Address { get; set; }
        public byte Active { get; set; }
        public byte Role { get; set; }

        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }

    [Table("ProductAttributes")]
    public class ProductAttribute
    {
        [Key]
        public int AttributeID { get; set; }

        [Required]
        public string ProductID { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }

    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public string UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
    }

    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        [Required]
        public int OrderID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    [Table("Images")]
    public class Image
    {
        [Key]
        public int ImageID { get; set; }

        [Required]
        public string ProductID { get; set; }
        public string ImageUrl { get; set; }
    }


    public class HomeViewModel
    {
        
        public List<Product> Products { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }

        public List<Category> Categories { get; set; }
        public List<SubCategorie> SubCategories { get; set; }

        public List<Service> Services { get; set; }
        public List<QuickLink> QuickLinks { get; set; }

        public List<Seller> Sellers { get; set; }
        public List<Brand> Brands { get; set; }

        public List<User> Users { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public List<Image> Images { get; set; }

    }

    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới không được để trống.")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }
    }

}