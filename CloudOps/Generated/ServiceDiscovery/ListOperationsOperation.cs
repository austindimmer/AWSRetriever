using Amazon;
using Amazon.ServiceDiscovery;
using Amazon.ServiceDiscovery.Model;
using Amazon.Runtime;

namespace CloudOps.ServiceDiscovery
{
    public class ListOperationsOperation : Operation
    {
        public override string Name => "ListOperations";

        public override string Description => "Lists operations that match the criteria that you specify.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ServiceDiscovery";

        public override string ServiceID => "ServiceDiscovery";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonServiceDiscoveryConfig config = new AmazonServiceDiscoveryConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonServiceDiscoveryClient client = new AmazonServiceDiscoveryClient(creds, config);
            
            ListOperationsResponse resp = new ListOperationsResponse();
            do
            {
                try
                {
                    ListOperationsRequest req = new ListOperationsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListOperationsAsync(req);
                    
                    foreach (var obj in resp.Operations)
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