using Amazon;
using Amazon.SESV2;
using Amazon.SESV2.Model;
using Amazon.Runtime;

namespace CloudOps.SESV2
{
    public class GetDedicatedIpsOperation : Operation
    {
        public override string Name => "GetDedicatedIps";

        public override string Description => "List the dedicated IP addresses that are associated with your AWS account.";
 
        public override string RequestURI => "/v2/email/dedicated-ips";

        public override string Method => "GET";

        public override string ServiceName => "SESV2";

        public override string ServiceID => "SESv2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSESV2Config config = new AmazonSESV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSESV2Client client = new AmazonSESV2Client(creds, config);
            
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