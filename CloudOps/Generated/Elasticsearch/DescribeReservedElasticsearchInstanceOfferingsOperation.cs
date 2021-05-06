using Amazon;
using Amazon.Elasticsearch;
using Amazon.Elasticsearch.Model;
using Amazon.Runtime;

namespace CloudOps.Elasticsearch
{
    public class DescribeReservedElasticsearchInstanceOfferingsOperation : Operation
    {
        public override string Name => "DescribeReservedElasticsearchInstanceOfferings";

        public override string Description => "Lists available reserved Elasticsearch instance offerings.";
 
        public override string RequestURI => "/2015-01-01/es/reservedInstanceOfferings";

        public override string Method => "GET";

        public override string ServiceName => "Elasticsearch";

        public override string ServiceID => "Elasticsearch Service";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticsearchConfig config = new AmazonElasticsearchConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElasticsearchClient client = new AmazonElasticsearchClient(creds, config);
            
            DescribeReservedElasticsearchInstanceOfferingsResponse resp = new DescribeReservedElasticsearchInstanceOfferingsResponse();
            do
            {
                try
                {
                    DescribeReservedElasticsearchInstanceOfferingsRequest req = new DescribeReservedElasticsearchInstanceOfferingsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeReservedElasticsearchInstanceOfferingsAsync(req);
                    
                    foreach (var obj in resp.ReservedElasticsearchInstanceOfferings)
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