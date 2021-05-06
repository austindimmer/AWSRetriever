using Amazon;
using Amazon.IoTWireless;
using Amazon.IoTWireless.Model;
using Amazon.Runtime;

namespace CloudOps.IoTWireless
{
    public class ListServiceProfilesOperation : Operation
    {
        public override string Name => "ListServiceProfiles";

        public override string Description => "Lists the service profiles registered to your AWS account.";
 
        public override string RequestURI => "/service-profiles";

        public override string Method => "GET";

        public override string ServiceName => "IoTWireless";

        public override string ServiceID => "IoT Wireless";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTWirelessConfig config = new AmazonIoTWirelessConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTWirelessClient client = new AmazonIoTWirelessClient(creds, config);
            
            ListServiceProfilesResponse resp = new ListServiceProfilesResponse();
            do
            {
                try
                {
                    ListServiceProfilesRequest req = new ListServiceProfilesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListServiceProfilesAsync(req);
                    
                    foreach (var obj in resp.ServiceProfileList)
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