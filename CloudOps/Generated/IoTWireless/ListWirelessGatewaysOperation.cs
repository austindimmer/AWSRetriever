using Amazon;
using Amazon.IoTWireless;
using Amazon.IoTWireless.Model;
using Amazon.Runtime;

namespace CloudOps.IoTWireless
{
    public class ListWirelessGatewaysOperation : Operation
    {
        public override string Name => "ListWirelessGateways";

        public override string Description => "Lists the wireless gateways registered to your AWS account.";
 
        public override string RequestURI => "/wireless-gateways";

        public override string Method => "GET";

        public override string ServiceName => "IoTWireless";

        public override string ServiceID => "IoT Wireless";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTWirelessConfig config = new AmazonIoTWirelessConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTWirelessClient client = new AmazonIoTWirelessClient(creds, config);
            
            ListWirelessGatewaysResponse resp = new ListWirelessGatewaysResponse();
            do
            {
                ListWirelessGatewaysRequest req = new ListWirelessGatewaysRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListWirelessGateways(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.WirelessGatewayList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}