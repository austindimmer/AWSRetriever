using Amazon;
using Amazon.AccessAnalyzer;
using Amazon.AccessAnalyzer.Model;
using Amazon.Runtime;

namespace CloudOps.AccessAnalyzer
{
    public class ListPolicyGenerationsOperation : Operation
    {
        public override string Name => "ListPolicyGenerations";

        public override string Description => "Lists all of the policy generations requested in the last seven days.";
 
        public override string RequestURI => "/policy/generation";

        public override string Method => "GET";

        public override string ServiceName => "AccessAnalyzer";

        public override string ServiceID => "AccessAnalyzer";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAccessAnalyzerConfig config = new AmazonAccessAnalyzerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAccessAnalyzerClient client = new AmazonAccessAnalyzerClient(creds, config);
            
            ListPolicyGenerationsResponse resp = new ListPolicyGenerationsResponse();
            do
            {
                ListPolicyGenerationsRequest req = new ListPolicyGenerationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListPolicyGenerations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.PolicyGenerations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}