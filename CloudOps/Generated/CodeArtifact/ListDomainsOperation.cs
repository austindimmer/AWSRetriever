using Amazon;
using Amazon.CodeArtifact;
using Amazon.CodeArtifact.Model;
using Amazon.Runtime;

namespace CloudOps.CodeArtifact
{
    public class ListDomainsOperation : Operation
    {
        public override string Name => "ListDomains";

        public override string Description => " Returns a list of DomainSummary objects for all domains owned by the AWS account that makes this call. Each returned DomainSummary object contains information about a domain. ";
 
        public override string RequestURI => "/v1/domains";

        public override string Method => "POST";

        public override string ServiceName => "CodeArtifact";

        public override string ServiceID => "codeartifact";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeArtifactConfig config = new AmazonCodeArtifactConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeArtifactClient client = new AmazonCodeArtifactClient(creds, config);
            
            ListDomainsResponse resp = new ListDomainsResponse();
            do
            {
                ListDomainsRequest req = new ListDomainsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListDomainsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Domains)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}