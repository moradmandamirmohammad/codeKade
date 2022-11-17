using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeKade.DataLayer.Entities.Common;

namespace codeKade.DataLayer.Entities.Blog
{
    public class BlogCategory : BaseEntity
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کارکتر باشد")]
        public string Title { get; set; }

        public List<Blog> Blogs { get; set; }
    }
}
