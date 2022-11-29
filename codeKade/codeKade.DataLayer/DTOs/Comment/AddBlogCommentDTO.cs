using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeKade.DataLayer.DTOs.Comment
{
    public class AddBlogCommentDTO
    {
        public long? ParentId { get; set; }

        public long UserId { get; set; }

        public long BLogId { get; set; }

        public long EventId { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "تعداد کارکتر ها بیش از حد مجاز است")]
        public string Text { get; set; }
    }
}
