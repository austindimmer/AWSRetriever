using Amazon;
using Amazon.IoTAnalytics;
using Amazon.IoTAnalytics.Model;
using Amazon.Runtime;

namespace CloudOps.IoTAnalytics
{
    public class ListDatastoresOperation : Operation
    {
        public override string Name => "ListDatastores";

        public override string Description => "Retrieves a list of data stores.";
 
        public override string RequestURI => "/datastores";

        public override string Method => "GET";

        public override string ServiceName => "IoTAnalytics";

        public override string ServiceID => "IoTAnalytics";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTAnalyticsConfig config = new AmazonIoTAnalyticsConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTAnalyticsClient client = new AmazonIoTAnalyticsClient(creds, config);
            
            ListDatastoresResponse resp = new ListDatastoresResponse();
            do
            {
                try
                {
                    ListDatastoresRequest req = new ListDatastoresRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListDatastoresAsync(req);
                    
                    foreach (var obj in resp.DatastoreSummaries)
                    {
                        AddObject(obj);
                    }
                    
                }
                catch (System.Exception)
                {
                    CheckError(resp.HttpStatusCode, "200");                
                    throw;
                }

            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}