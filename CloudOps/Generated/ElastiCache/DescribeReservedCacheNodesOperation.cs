using Amazon;
using Amazon.ElastiCache;
using Amazon.ElastiCache.Model;
using Amazon.Runtime;

namespace CloudOps.ElastiCache
{
    public class DescribeReservedCacheNodesOperation : Operation
    {
        public override string Name => "DescribeReservedCacheNodes";

        public override string Description => "Returns information about reserved cache nodes for this account, or about a specified reserved cache node.";
 
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
            
            DescribeReservedCacheNodesResponse resp = new DescribeReservedCacheNodesResponse();
            do
            {
                try
                {
                    DescribeReservedCacheNodesRequest req = new DescribeReservedCacheNodesRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeReservedCacheNodesAsync(req);
                    
                    foreach (var obj in resp.ReservedCacheNodes)
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