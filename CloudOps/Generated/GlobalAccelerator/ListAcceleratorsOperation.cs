using Amazon;
using Amazon.GlobalAccelerator;
using Amazon.GlobalAccelerator.Model;
using Amazon.Runtime;

namespace CloudOps.GlobalAccelerator
{
    public class ListAcceleratorsOperation : Operation
    {
        public override string Name => "ListAccelerators";

        public override string Description => "List the accelerators for an AWS account. ";
 
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
            
            ListAcceleratorsResponse resp = new ListAcceleratorsResponse();
            do
            {
                ListAcceleratorsRequest req = new ListAcceleratorsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListAcceleratorsAsync(req);
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