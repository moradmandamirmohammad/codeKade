using codeKade.DataLayer.DTOs.Paging;
using codeKade.DataLayer.Entities.Account;

namespace codeKade.DataLayer.DTOs.Account
{
    public class FilterUserDTO : BasePaging
    {
        public List<User> Users { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
    }
}
