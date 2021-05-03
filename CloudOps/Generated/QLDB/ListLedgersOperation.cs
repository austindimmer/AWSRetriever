using Amazon;
using Amazon.QLDB;
using Amazon.QLDB.Model;
using Amazon.Runtime;

namespace CloudOps.QLDB
{
    public class ListLedgersOperation : Operation
    {
        public override string Name => "ListLedgers";

        public override string Description => "Returns an array of ledger summaries that are associated with the current AWS account and Region. This action returns a maximum of 100 items and is paginated so that you can retrieve all the items by calling ListLedgers multiple times.";
 
        public override string RequestURI => "/ledgers";

        public override string Method => "GET";

        public override string ServiceName => "QLDB";

        public override string ServiceID => "QLDB";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonQLDBConfig config = new AmazonQLDBConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonQLDBClient client = new AmazonQLDBClient(creds, config);
            
            ListLedgersResponse resp = new ListLedgersResponse();
            do
            {
                ListLedgersRequest req = new ListLedgersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListLedgers(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Ledgers)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}