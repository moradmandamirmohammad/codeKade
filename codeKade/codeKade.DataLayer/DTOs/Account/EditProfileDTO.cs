using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace codeKade.DataLayer.DTOs.Account
{
    public class EditProfileDTO
    {
        public long Id { get; set; }

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

        [Display(Name = "آدرس")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string? Address { get; set; }

        public IFormFile AvatarImage { get; set; }
    }
}
