using Amazon;
using Amazon.ElastiCache;
using Amazon.ElastiCache.Model;
using Amazon.Runtime;

namespace CloudOps.ElastiCache
{
    public class DescribeReplicationGroupsOperation : Operation
    {
        public override string Name => "DescribeReplicationGroups";

        public override string Description => "Returns information about a particular replication group. If no identifier is specified, DescribeReplicationGroups returns information about all replication groups.  This operation is valid for Redis only. ";
 
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
            
            DescribeReplicationGroupsResponse resp = new DescribeReplicationGroupsResponse();
            do
            {
                try
                {
                    DescribeReplicationGroupsRequest req = new DescribeReplicationGroupsRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeReplicationGroupsAsync(req);
                    
                    foreach (var obj in resp.ReplicationGroups)
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