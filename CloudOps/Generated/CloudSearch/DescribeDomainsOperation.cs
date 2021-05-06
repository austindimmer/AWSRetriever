using Amazon;
using Amazon.CloudSearch;
using Amazon.CloudSearch.Model;
using Amazon.Runtime;

namespace CloudOps.CloudSearch
{
    public class DescribeDomainsOperation : Operation
    {
        public override string Name => "DescribeDomains";

        public override string Description => "Gets information about the search domains owned by this account. Can be limited to specific domains. Shows all domains by default. To get the number of searchable documents in a domain, use the console or submit a matchall request to your domain&#39;s search endpoint: q=matchall&amp;amp;amp;q.parser=structured&amp;amp;amp;size=0. For more information, see Getting Information about a Search Domain in the Amazon CloudSearch Developer Guide.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CloudSearch";

        public override string ServiceID => "CloudSearch";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudSearchConfig config = new AmazonCloudSearchConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudSearchClient client = new AmazonCloudSearchClient(creds, config);
            
            DescribeDomainsResponse resp = new DescribeDomainsResponse();
            DescribeDomainsRequest req = new DescribeDomainsRequest
            {                    
                                    
            };
            resp = await client.DescribeDomainsAsync(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.DomainStatusList)
            {
                AddObject(obj);
            }
            
        }
    }
}