using Amazon;
using Amazon.Macie;
using Amazon.Macie.Model;
using Amazon.Runtime;

namespace CloudOps.Macie
{
    public class ListFindingsFiltersOperation : Operation
    {
        public override string Name => "ListFindingsFilters";

        public override string Description => "Retrieves a subset of information about all the findings filters for an account.";
 
        public override string RequestURI => "/findingsfilters";

        public override string Method => "GET";

        public override string ServiceName => "Macie";

        public override string ServiceID => "Macie";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacieConfig config = new AmazonMacieConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacieClient client = new AmazonMacieClient(creds, config);
            
            ListFindingsFiltersResponse resp = new ListFindingsFiltersResponse();
            do
            {
                ListFindingsFiltersRequest req = new ListFindingsFiltersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListFindingsFilters(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.FindingsFilterListItems)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}