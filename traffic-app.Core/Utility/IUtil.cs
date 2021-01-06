using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace traffic_app.Core.Utility
{
    public interface IUtil
    {
        string GetHash(string value);
        int getUserIdFromToken(string tokenString);
        public void VaryQualityLevel(Bitmap bmp, string savePath);
    }
}
