using Amazon;
using Amazon.Inspector;
using Amazon.Inspector.Model;
using Amazon.Runtime;

namespace CloudOps.Inspector
{
    public class ListRulesPackagesOperation : Operation
    {
        public override string Name => "ListRulesPackages";

        public override string Description => "Lists all available Amazon Inspector rules packages.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Inspector";

        public override string ServiceID => "Inspector";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonInspectorConfig config = new AmazonInspectorConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonInspectorClient client = new AmazonInspectorClient(creds, config);
            
            ListRulesPackagesResponse resp = new ListRulesPackagesResponse();
            do
            {
                ListRulesPackagesRequest req = new ListRulesPackagesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListRulesPackagesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.RulesPackageArns)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}