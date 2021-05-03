using Amazon;
using Amazon.IoTWireless;
using Amazon.IoTWireless.Model;
using Amazon.Runtime;

namespace CloudOps.IoTWireless
{
    public class ListWirelessDevicesOperation : Operation
    {
        public override string Name => "ListWirelessDevices";

        public override string Description => "Lists the wireless devices registered to your AWS account.";
 
        public override string RequestURI => "/wireless-devices";

        public override string Method => "GET";

        public override string ServiceName => "IoTWireless";

        public override string ServiceID => "IoT Wireless";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTWirelessConfig config = new AmazonIoTWirelessConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTWirelessClient client = new AmazonIoTWirelessClient(creds, config);
            
            ListWirelessDevicesResponse resp = new ListWirelessDevicesResponse();
            do
            {
                ListWirelessDevicesRequest req = new ListWirelessDevicesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListWirelessDevices(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.WirelessDeviceList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}