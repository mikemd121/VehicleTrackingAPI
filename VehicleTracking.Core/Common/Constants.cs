using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracking.Core.Common
{
   public static class Constants
    {
        /// <summary>
        /// Templates folder.
        /// </summary>
        public static string HdInsightClusterUserName = "admin";

        /// <summary>
        /// Helm files folder.
        /// </summary>
        public static string HdInsightClusterPassword = "abcABC@1234";

        /// <summary>
        /// Template app folder.
        /// </summary>
        public static string HdInsightDnsName = "hdinsightclustereastus";

        /// <summary>
        /// Chart folder.
        /// </summary>
        public static string HdInsightAgentPrefix = "HDInsightODBClient";

        /// <summary>
        /// Chart folder.
        /// </summary>
        public static string AzureEventHubConnection = @"Endpoint=sb://vehicletrackingstreamlab.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccesskey;SharedAccessKey=2x7Xe0qEvca/RgWr+L8/S3OaKxUB65k+0Vp017hEQA4=;EntityPath=trackingdatahub";

        /// <summary>
        /// The API endpoint
        /// </summary>
        public const string ApiEndpoint = @"https://maps.googleapis.com/maps/api/geocode/json";

        /// <summary>
        /// The API key
        /// </summary>
        public const string ApiKey = @"AIzaSyD_c0-VV1agJsotu4WHhjAdkwcxF-ucF-8";
    }
}
