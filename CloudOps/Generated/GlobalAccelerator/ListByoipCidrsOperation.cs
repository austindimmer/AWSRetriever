using Amazon;
using Amazon.GlobalAccelerator;
using Amazon.GlobalAccelerator.Model;
using Amazon.Runtime;

namespace CloudOps.GlobalAccelerator
{
    public class ListByoipCidrsOperation : Operation
    {
        public override string Name => "ListByoipCidrs";

        public override string Description => "Lists the IP address ranges that were specified in calls to ProvisionByoipCidr, including the current state and a history of state changes.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "GlobalAccelerator";

        public override string ServiceID => "Global Accelerator";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGlobalAcceleratorConfig config = new AmazonGlobalAcceleratorConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGlobalAcceleratorClient client = new AmazonGlobalAcceleratorClient(creds, config);
            
            ListByoipCidrsResponse resp = new ListByoipCidrsResponse();
            do
            {
                try
                {
                    ListByoipCidrsRequest req = new ListByoipCidrsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListByoipCidrsAsync(req);
                    
                    foreach (var obj in resp.ByoipCidrs)
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