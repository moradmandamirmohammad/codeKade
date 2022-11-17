using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeKade.DataLayer.Entities.Account;
using codeKade.DataLayer.Entities.Common;

namespace codeKade.DataLayer.Entities.Comment
{
    public class Comment :BaseEntity
    {
        public long? ParentId { get; set; }

        public long UserId { get; set; }

        public long EventId { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "تعداد کارکتر ها بیش از حد مجاز است")]
        public string Text { get; set; }
        public bool ShowInIndex { get; set; }

        public Event.Event Event { get; set; }

        public User User { get; set; }

        [ForeignKey("ParentId")]
        public List<Comment> Comments { get; set; }
    }
}
