using Amazon;
using Amazon.AccessAnalyzer;
using Amazon.AccessAnalyzer.Model;
using Amazon.Runtime;

namespace CloudOps.AccessAnalyzer
{
    public class ListAnalyzersOperation : Operation
    {
        public override string Name => "ListAnalyzers";

        public override string Description => "Retrieves a list of analyzers.";
 
        public override string RequestURI => "/analyzer";

        public override string Method => "GET";

        public override string ServiceName => "AccessAnalyzer";

        public override string ServiceID => "AccessAnalyzer";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAccessAnalyzerConfig config = new AmazonAccessAnalyzerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAccessAnalyzerClient client = new AmazonAccessAnalyzerClient(creds, config);
            
            ListAnalyzersResponse resp = new ListAnalyzersResponse();
            do
            {
                ListAnalyzersRequest req = new ListAnalyzersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListAnalyzers(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Analyzers)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}