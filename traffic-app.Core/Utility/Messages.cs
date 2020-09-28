using System;
using System.Collections.Generic;
using System.Text;

namespace traffic_app.Core.Utility
{
    public static class Messages
    {
        public const string GeneralError = "Error occured";
        public const string RootNotFound = "This action was not found";
        public const string Forbidden = "You do not have permission to this action";
        public const string MethodNotAllowed = "Check your request type (POST/GET/PATCH/DELETE/PUT)";
        public const string InvalidModel = "Data is not valid";
        public const string UserExist = "User is exist";
        public const string UserNotExist = "User is not exist";
        public const string CompanyNotExist = "Company is not exist";
        public const string ServiceNotExist = "Service is not exist";
        public const string LoginFailed = "Login failed";
    }
}
