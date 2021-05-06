using Amazon;
using Amazon.DeviceFarm;
using Amazon.DeviceFarm.Model;
using Amazon.Runtime;

namespace CloudOps.DeviceFarm
{
    public class ListDevicesOperation : Operation
    {
        public override string Name => "ListDevices";

        public override string Description => "Gets information about unique device types.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DeviceFarm";

        public override string ServiceID => "Device Farm";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDeviceFarmConfig config = new AmazonDeviceFarmConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDeviceFarmClient client = new AmazonDeviceFarmClient(creds, config);
            
            ListDevicesResponse resp = new ListDevicesResponse();
            do
            {
                try
                {
                    ListDevicesRequest req = new ListDevicesRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListDevicesAsync(req);
                    
                    foreach (var obj in resp.Devices)
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