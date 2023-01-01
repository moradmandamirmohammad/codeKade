using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeKade.DataLayer.Entities.Account;
using codeKade.DataLayer.Entities.Common;

namespace codeKade.DataLayer.Entities.School
{
    public class School : BaseEntity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public List<User> Users { get; set; }
    }
}
