using System;
using System.Collections.Generic;
using System.Data.Odbc;
using VehicleTracking.Core.Common;
using VehicleTrackingAPI.Common;
using VehicleTrackingAPI.Interfaces;

namespace VehicleTrackingAPI.Implementations
{
    public class HdInsightConnectService : IHdInsightConnect
    {
        /// <summary>
        /// Hds the insight connect.
        /// </summary>
        private static string HdInsightConnectionString()
        {
            const int totalIteration = 1;
            for (int j = 0; j < totalIteration; j++)
            {
                var defaultAgentString = Constants.HdInsightAgentPrefix + j;
                var connectionString = GenerateConnectionString(defaultAgentString, Constants.HdInsightClusterUserName,
                    Constants.HdInsightClusterPassword, Constants.HdInsightDnsName);
                connectionString = connectionString + "DSN=Sample Microsoft Spark DSN";
                return connectionString;
            }
            return string.Empty;
        }

        /// <summary>
        /// Executes the spark SQL.
        /// </summary>
        /// <param name="sparkQuery">The spark query.</param>
        /// <returns></returns>
        public List<TrackingLocationResponse> ExecuteSparkSQL(string sparkQuery)
        {
            var conString = HdInsightConnectionString();
            var trackLocationList = new List<TrackingLocationResponse>();
            using (var connection = new OdbcConnection(conString))
            {
                connection.Open();
                using (OdbcCommand command = connection.CreateCommand())
                {
                    command.CommandTimeout = 300;
                    command.CommandText = sparkQuery;
                    Console.WriteLine(sparkQuery);
                    var reader = command.ExecuteReader();
                    Console.WriteLine(reader);

                    while (reader.Read())
                    {

                        var trackLocationResponse = new TrackingLocationResponse
                        {
                            VehicleId = Convert.ToInt32(reader.GetValue(1)),
                            Latitude = Convert.ToDouble(reader.GetValue(2)),
                            Longitude = Convert.ToDouble(reader.GetValue(3)),
                            CreatedDateTime = Convert.ToDateTime(reader.GetValue(4))
                        };
                        trackLocationList.Add(trackLocationResponse);
                    }
                    reader.Close();
                }
                connection.Close();
                return trackLocationList;
            }
        }

        /// <summary>
        /// Generates the connection string.
        /// </summary>
        /// <param name="defaultUserAgent">The default user agent.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="dnsName">Name of the DNS.</param>
        /// <returns></returns>
        private static string GenerateConnectionString(string defaultUserAgent, string userName, string password, string dnsName)
        {
            var hiveOdbcDriverName = "Microsoft Spark ODBC Driver";
            var domainName = "azurehdinsight.net";
            var servicePortNumber = "443";
            var timeoutInSeconds = 600;

            return
                string.Format(
                @"DRIVER={0};HOST={1}.{2};PORT={3};SparkServerType=3;AuthMech=6;UID={4};PWD={5};" +
                @"UseNativeQuery=1;WdHttpUserAgent={6};WdSocketTimeout={7};",
                    "{" + hiveOdbcDriverName + "}",
                    dnsName,
                    domainName,
                    servicePortNumber,
                    "{" + userName + "}",
                    "{" + password + "}",
                    defaultUserAgent,
                    timeoutInSeconds
                    );
        }
    }
}
