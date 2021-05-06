using Amazon;
using Amazon.Inspector;
using Amazon.Inspector.Model;
using Amazon.Runtime;

namespace CloudOps.Inspector
{
    public class ListFindingsOperation : Operation
    {
        public override string Name => "ListFindings";

        public override string Description => "Lists findings that are generated by the assessment runs that are specified by the ARNs of the assessment runs.";
 
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
            
            ListFindingsResponse resp = new ListFindingsResponse();
            do
            {
                try
                {
                    ListFindingsRequest req = new ListFindingsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListFindingsAsync(req);
                    
                    foreach (var obj in resp.FindingArns)
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