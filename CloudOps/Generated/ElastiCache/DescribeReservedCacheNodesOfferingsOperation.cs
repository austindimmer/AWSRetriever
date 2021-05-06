using Amazon;
using Amazon.ElastiCache;
using Amazon.ElastiCache.Model;
using Amazon.Runtime;

namespace CloudOps.ElastiCache
{
    public class DescribeReservedCacheNodesOfferingsOperation : Operation
    {
        public override string Name => "DescribeReservedCacheNodesOfferings";

        public override string Description => "Lists available reserved cache node offerings.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ElastiCache";

        public override string ServiceID => "ElastiCache";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElastiCacheConfig config = new AmazonElastiCacheConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElastiCacheClient client = new AmazonElastiCacheClient(creds, config);
            
            DescribeReservedCacheNodesOfferingsResponse resp = new DescribeReservedCacheNodesOfferingsResponse();
            do
            {
                try
                {
                    DescribeReservedCacheNodesOfferingsRequest req = new DescribeReservedCacheNodesOfferingsRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeReservedCacheNodesOfferingsAsync(req);
                    
                    foreach (var obj in resp.ReservedCacheNodesOfferings)
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
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}