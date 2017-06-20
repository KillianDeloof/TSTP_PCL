using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace TSTP_PCL.MobileSDK.AzureMobileClient
{
    public class AzureMobileClient
    {
        public static string ApplicationURL { get; set; } = @"https://tstpbackend.azurewebsites.net/";
        private static MobileServiceClient _defaultClient;

        public static MobileServiceClient DefaultClient
        {
            get { return _defaultClient ?? (_defaultClient = new MobileServiceClient(ApplicationURL)); }
        }
    }
}
