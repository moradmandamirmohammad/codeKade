using System.ComponentModel.DataAnnotations;
using codeKade.DataLayer.Entities.Comment;
using codeKade.DataLayer.Entities.Common;

namespace codeKade.DataLayer.Entities.Account
{
    public class User : BaseEntity
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string LastName { get; set; }

        [Display(Name = "شماره تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string Mobile { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string Email { get; set; }

        [Display(Name = "کد هنرستان")]
        public long? SchoolId { get; set; }

        [Display(Name = "آدرس")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string? Address { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string? Description { get; set; }

        [Display(Name = "فعال / غیرفعال")]
        public bool IsActive { get; set; }

        [Display(Name = "نقش")]
        public bool IsAdmin { get; set; }

        [Display(Name = "آیدی اینستاگرام")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string? InstagramId { get; set; }

        [Display(Name = "آیدی تلگرام")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string? TelegramId { get; set; }

        [Display(Name = "کد فعال")]
        public string ActiveCode { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string Password { get; set; }

        [Display(Name = "نام عکس")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string Avatar { get; set; }

        public List<Blog.Blog> Blogs { get; set; }

        public List<BlogComment> BlogComments { get; set; }

        public School.School School { get; set; }

    }
}
