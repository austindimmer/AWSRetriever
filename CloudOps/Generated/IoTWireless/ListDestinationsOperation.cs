using Amazon;
using Amazon.IoTWireless;
using Amazon.IoTWireless.Model;
using Amazon.Runtime;

namespace CloudOps.IoTWireless
{
    public class ListDestinationsOperation : Operation
    {
        public override string Name => "ListDestinations";

        public override string Description => "Lists the destinations registered to your AWS account.";
 
        public override string RequestURI => "/destinations";

        public override string Method => "GET";

        public override string ServiceName => "IoTWireless";

        public override string ServiceID => "IoT Wireless";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTWirelessConfig config = new AmazonIoTWirelessConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTWirelessClient client = new AmazonIoTWirelessClient(creds, config);
            
            ListDestinationsResponse resp = new ListDestinationsResponse();
            do
            {
                ListDestinationsRequest req = new ListDestinationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListDestinations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DestinationList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}