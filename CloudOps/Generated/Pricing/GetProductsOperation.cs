using Amazon;
using Amazon.Pricing;
using Amazon.Pricing.Model;
using Amazon.Runtime;

namespace CloudOps.Pricing
{
    public class GetProductsOperation : Operation
    {
        public override string Name => "GetProducts";

        public override string Description => "Returns a list of all products that match the filter criteria.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Pricing";

        public override string ServiceID => "Pricing";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonPricingConfig config = new AmazonPricingConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonPricingClient client = new AmazonPricingClient(creds, config);
            
            GetProductsResponse resp = new GetProductsResponse();
            do
            {
                GetProductsRequest req = new GetProductsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.GetProductsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.FormatVersion)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.PriceList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}