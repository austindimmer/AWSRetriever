using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListOTAUpdatesOperation : Operation
    {
        public override string Name => "ListOTAUpdates";

        public override string Description => "Lists OTA updates.";
 
        public override string RequestURI => "/otaUpdates";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListOTAUpdatesResponse resp = new ListOTAUpdatesResponse();
            do
            {
                try
                {
                    ListOTAUpdatesRequest req = new ListOTAUpdatesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListOTAUpdatesAsync(req);
                    
                    foreach (var obj in resp.OtaUpdates)
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