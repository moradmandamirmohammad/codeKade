using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeKade.DataLayer.Entities.Account;
using codeKade.DataLayer.Entities.Common;

namespace codeKade.DataLayer.Entities.Blog
{
    public class Blog : BaseEntity
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string Title { get; set; }

        public long BlogCategoryId { get; set; }

        public long? UserId { get; set; }

        [Display(Name = "بدنه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Body { get; set; }

        [Display(Name = "بازدید")]
        public int Seen { get; set; }

        [Display(Name = "نام عکس")]
        public string ImageName { get; set; }

        public BlogCategory BlogCategory { get; set; }

        public User User { get; set; }
    }
}
