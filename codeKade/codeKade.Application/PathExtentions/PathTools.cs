using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeKade.Application.PathExtentions
{
    public static class PathTools
    {
        #region Products

        public static string UserImagePath = "/Upload/Images/Users/";
        public static string UserImageUpload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Images/Users/");

        public static string BlogImagePath = "/Upload/Images/Blog/";
        public static string DefaultBlogImage = "/Upload/Images/Blog/Default.png";
        public static string BlogImageUpload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Images/Blog/");

        public static string EventImagePath = "/Upload/Images/Event/";
        public static string EventThumbImagePath = "/Upload/Images/Event/thumb/";


        public static string DefaultUserImage = "/Upload/Images/usr_avatar.png";



        #endregion
    }
}
