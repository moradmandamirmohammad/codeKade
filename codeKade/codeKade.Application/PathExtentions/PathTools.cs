﻿using System;
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
        public static string UserImageUpload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products/");


        public static string ProductThumbImagePath = "/Images/Products/thumb/";
        public static string ProductThumbImageUpload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products/thumb/");


        public static string DefaultUserImage = "/Upload/Images/usr_avatar.png";



        #endregion
    }
}
