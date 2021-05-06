using Amazon;
using Amazon.IoTSecureTunneling;
using Amazon.IoTSecureTunneling.Model;
using Amazon.Runtime;

namespace CloudOps.IoTSecureTunneling
{
    public class ListTunnelsOperation : Operation
    {
        public override string Name => "ListTunnels";

        public override string Description => "List all tunnels for an AWS account. Tunnels are listed by creation time in descending order, newer tunnels will be listed before older tunnels.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "IoTSecureTunneling";

        public override string ServiceID => "IoTSecureTunneling";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTSecureTunnelingConfig config = new AmazonIoTSecureTunnelingConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTSecureTunnelingClient client = new AmazonIoTSecureTunnelingClient(creds, config);
            
            ListTunnelsResponse resp = new ListTunnelsResponse();
            do
            {
                ListTunnelsRequest req = new ListTunnelsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListTunnelsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.TunnelSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}