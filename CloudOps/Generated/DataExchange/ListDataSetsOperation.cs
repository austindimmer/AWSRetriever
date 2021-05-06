using Amazon;
using Amazon.DataExchange;
using Amazon.DataExchange.Model;
using Amazon.Runtime;

namespace CloudOps.DataExchange
{
    public class ListDataSetsOperation : Operation
    {
        public override string Name => "ListDataSets";

        public override string Description => "This operation lists your data sets. When listing by origin OWNED, results are sorted by CreatedAt in descending order. When listing by origin ENTITLED, there is no order and the maxResults parameter is ignored.";
 
        public override string RequestURI => "/v1/data-sets";

        public override string Method => "GET";

        public override string ServiceName => "DataExchange";

        public override string ServiceID => "DataExchange";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDataExchangeConfig config = new AmazonDataExchangeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDataExchangeClient client = new AmazonDataExchangeClient(creds, config);
            
            ListDataSetsResponse resp = new ListDataSetsResponse();
            do
            {
                ListDataSetsRequest req = new ListDataSetsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListDataSetsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DataSets)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}