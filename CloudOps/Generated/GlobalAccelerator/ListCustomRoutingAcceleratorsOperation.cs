using Amazon;
using Amazon.GlobalAccelerator;
using Amazon.GlobalAccelerator.Model;
using Amazon.Runtime;

namespace CloudOps.GlobalAccelerator
{
    public class ListCustomRoutingAcceleratorsOperation : Operation
    {
        public override string Name => "ListCustomRoutingAccelerators";

        public override string Description => "List the custom routing accelerators for an AWS account. ";
 
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
            
            ListCustomRoutingAcceleratorsResponse resp = new ListCustomRoutingAcceleratorsResponse();
            do
            {
                ListCustomRoutingAcceleratorsRequest req = new ListCustomRoutingAcceleratorsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListCustomRoutingAcceleratorsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Accelerators)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}