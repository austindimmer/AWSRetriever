using Amazon;
using Amazon.Macie2;
using Amazon.Macie2.Model;
using Amazon.Runtime;

namespace CloudOps.Macie2
{
    public class ListFindingsOperation : Operation
    {
        public override string Name => "ListFindings";

        public override string Description => " Retrieves a subset of information about one or more findings.";
 
        public override string RequestURI => "/findings";

        public override string Method => "POST";

        public override string ServiceName => "Macie2";

        public override string ServiceID => "Macie2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacie2Config config = new AmazonMacie2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacie2Client client = new AmazonMacie2Client(creds, config);
            
            ListFindingsResponse resp = new ListFindingsResponse();
            do
            {
                ListFindingsRequest req = new ListFindingsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListFindings(req);
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