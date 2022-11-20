namespace codeKade.DataLayer.DTOs.Paging
{
    public class BasePaging
    {
        public BasePaging()
        {
            this.PageID = 1;
            this.TakeEntity = 9;
        }

        public int PageID { get; set; }

        public int TakeEntity { get; set; }

        public int SkipEntity { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public int PageCount { get; set; }

        public BasePaging GetCurrentPaging()
        {
            return this;
        }
    }
}
