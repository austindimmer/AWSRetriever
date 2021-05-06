using Amazon;
using Amazon.Macie;
using Amazon.Macie.Model;
using Amazon.Runtime;

namespace CloudOps.Macie
{
    public class ListFindingsOperation : Operation
    {
        public override string Name => "ListFindings";

        public override string Description => " Retrieves a subset of information about one or more findings.";
 
        public override string RequestURI => "/findings";

        public override string Method => "POST";

        public override string ServiceName => "Macie";

        public override string ServiceID => "Macie";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacieConfig config = new AmazonMacieConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacieClient client = new AmazonMacieClient(creds, config);
            
            ListFindingsResponse resp = new ListFindingsResponse();
            do
            {
                ListFindingsRequest req = new ListFindingsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListFindings(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.FindingIds)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}