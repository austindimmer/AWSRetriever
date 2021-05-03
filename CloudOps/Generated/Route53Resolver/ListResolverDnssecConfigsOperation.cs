using Amazon;
using Amazon.Route53Resolver;
using Amazon.Route53Resolver.Model;
using Amazon.Runtime;

namespace CloudOps.Route53Resolver
{
    public class ListResolverDnssecConfigsOperation : Operation
    {
        public override string Name => "ListResolverDnssecConfigs";

        public override string Description => "Lists the configurations for DNSSEC validation that are associated with the current AWS account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Route53Resolver";

        public override string ServiceID => "Route53Resolver";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoute53ResolverConfig config = new AmazonRoute53ResolverConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoute53ResolverClient client = new AmazonRoute53ResolverClient(creds, config);
            
            ListResolverDnssecConfigsResponse resp = new ListResolverDnssecConfigsResponse();
            do
            {
                ListResolverDnssecConfigsRequest req = new ListResolverDnssecConfigsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListResolverDnssecConfigs(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ResolverDnssecConfigs)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}