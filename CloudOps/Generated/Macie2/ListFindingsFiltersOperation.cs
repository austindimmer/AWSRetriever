using Amazon;
using Amazon.Macie2;
using Amazon.Macie2.Model;
using Amazon.Runtime;

namespace CloudOps.Macie2
{
    public class ListFindingsFiltersOperation : Operation
    {
        public override string Name => "ListFindingsFilters";

        public override string Description => "Retrieves a subset of information about all the findings filters for an account.";
 
        public override string RequestURI => "/findingsfilters";

        public override string Method => "GET";

        public override string ServiceName => "Macie2";

        public override string ServiceID => "Macie2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacie2Config config = new AmazonMacie2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacie2Client client = new AmazonMacie2Client(creds, config);
            
            ListFindingsFiltersResponse resp = new ListFindingsFiltersResponse();
            do
            {
                try
                {
                    ListFindingsFiltersRequest req = new ListFindingsFiltersRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListFindingsFiltersAsync(req);
                    
                    foreach (var obj in resp.FindingsFilterList.Items)
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