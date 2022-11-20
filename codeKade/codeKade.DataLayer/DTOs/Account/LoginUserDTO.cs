using System.ComponentModel.DataAnnotations;


namespace codeKade.DataLayer.DTOs.Account
{
    public class LoginUserDTO
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        [DataType(DataType.EmailAddress, ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public enum LoginUserResult
    {
        NotActive,
        Success,
        NotFound
    }
}
