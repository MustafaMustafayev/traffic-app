using System;
using System.Collections.Generic;
using System.Text;

namespace traffic_app.Core.Utility
{
    public static class Messages
    {
        public const string GeneralError = "Xəta yaşandı";
        public const string RootNotFound = "This action was not found";
        public const string Forbidden = "You do not have permission to this action";
        public const string MethodNotAllowed = "Check your request type (POST/GET/PATCH/DELETE/PUT)";
        public const string InvalidModel = "Məlumatlar tam daxil edilməyib";
        public const string UserIsExist = "İstifadəçi mövcuddur";
        public const string LoginFailed = "İstifadəçi məlumatları yanlışdır";
        public const string InvalidImageFileFormat = "Əlavə etdiyiniz şəklin formatı qəbul edilmir";
        public const string MessageAccepted = "Sorğunuz qəbul edildi";
    }
}
