using System;
using System.Collections.Generic;
using System.Text;

namespace traffic_app.Core.Utility
{
    public static class Constants
    {
        public static List<string> ValidImageFileFormats = new List<string>() { "jpg","jpeg","gif","GIF","webp","tiff","psd","raw","bmp","heif","indd","jpeg 2000","png","svg","jfif" };
        public const string AuthorizationHeaderName = "Authorization";
        public const string PageNumberHeaderName = "PageNumber";
        public const string PageSizeHeaderName = "PageSize";
    }
}
