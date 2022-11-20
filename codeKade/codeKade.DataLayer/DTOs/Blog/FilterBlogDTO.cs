using codeKade.DataLayer.DTOs.Paging;

namespace codeKade.DataLayer.DTOs.Blog
{
    public class FilterBlogDTO : BasePaging
    {
        public string Title { get; set; }

        public List<Entities.Blog.Blog> Blogs { get; set; }
    }
}
