using Amazon;
using Amazon.GreengrassV2;
using Amazon.GreengrassV2.Model;
using Amazon.Runtime;

namespace CloudOps.GreengrassV2
{
    public class ListCoreDevicesOperation : Operation
    {
        public override string Name => "ListCoreDevices";

        public override string Description => "Retrieves a paginated list of AWS IoT Greengrass core devices.";
 
        public override string RequestURI => "/greengrass/v2/coreDevices";

        public override string Method => "GET";

        public override string ServiceName => "GreengrassV2";

        public override string ServiceID => "GreengrassV2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGreengrassV2Config config = new AmazonGreengrassV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGreengrassV2Client client = new AmazonGreengrassV2Client(creds, config);
            
            ListCoreDevicesResponse resp = new ListCoreDevicesResponse();
            do
            {
                try
                {
                    ListCoreDevicesRequest req = new ListCoreDevicesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListCoreDevicesAsync(req);
                    
                    foreach (var obj in resp.CoreDevices)
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