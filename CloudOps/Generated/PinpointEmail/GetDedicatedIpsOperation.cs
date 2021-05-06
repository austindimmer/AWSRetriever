using Amazon;
using Amazon.PinpointEmail;
using Amazon.PinpointEmail.Model;
using Amazon.Runtime;

namespace CloudOps.PinpointEmail
{
    public class GetDedicatedIpsOperation : Operation
    {
        public override string Name => "GetDedicatedIps";

        public override string Description => "List the dedicated IP addresses that are associated with your Amazon Pinpoint account.";
 
        public override string RequestURI => "/v1/email/dedicated-ips";

        public override string Method => "GET";

        public override string ServiceName => "PinpointEmail";

        public override string ServiceID => "Pinpoint Email";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonPinpointEmailConfig config = new AmazonPinpointEmailConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonPinpointEmailClient client = new AmazonPinpointEmailClient(creds, config);
            
            GetDedicatedIpsResponse resp = new GetDedicatedIpsResponse();
            do
            {
                try
                {
                    GetDedicatedIpsRequest req = new GetDedicatedIpsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.GetDedicatedIpsAsync(req);
                    
                    foreach (var obj in resp.DedicatedIps)
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