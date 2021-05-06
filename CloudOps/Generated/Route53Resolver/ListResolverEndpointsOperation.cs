using Amazon;
using Amazon.Route53Resolver;
using Amazon.Route53Resolver.Model;
using Amazon.Runtime;

namespace CloudOps.Route53Resolver
{
    public class ListResolverEndpointsOperation : Operation
    {
        public override string Name => "ListResolverEndpoints";

        public override string Description => "Lists all the Resolver endpoints that were created using the current AWS account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Route53Resolver";

        public override string ServiceID => "Route53Resolver";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoute53ResolverConfig config = new AmazonRoute53ResolverConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoute53ResolverClient client = new AmazonRoute53ResolverClient(creds, config);
            
            ListResolverEndpointsResponse resp = new ListResolverEndpointsResponse();
            do
            {
                try
                {
                    ListResolverEndpointsRequest req = new ListResolverEndpointsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListResolverEndpointsAsync(req);
                    
                    foreach (var obj in resp.ResolverEndpoints)
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