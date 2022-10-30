using System.ComponentModel.DataAnnotations;

namespace codeKade.DataLayer.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        public long ID { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastUpdateDate { get; set; }
    }
}
