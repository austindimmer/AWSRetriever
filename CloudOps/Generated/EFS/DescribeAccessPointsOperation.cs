using Amazon;
using Amazon.ElasticFileSystem;
using Amazon.ElasticFileSystem.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticFileSystem
{
    public class DescribeAccessPointsOperation : Operation
    {
        public override string Name => "DescribeAccessPoints";

        public override string Description => "Returns the description of a specific Amazon ElasticFileSystem access point if the AccessPointId is provided. If you provide an ElasticFileSystem FileSystemId, it returns descriptions of all access points for that file system. You can provide either an AccessPointId or a FileSystemId in the request, but not both.  This operation requires permissions for the elasticfilesystem:DescribeAccessPoints action.";
 
        public override string RequestURI => "/2015-02-01/access-points";

        public override string Method => "GET";

        public override string ServiceName => "ElasticFileSystem";

        public override string ServiceID => "ElasticFileSystem";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticFileSystemConfig config = new AmazonElasticFileSystemConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElasticFileSystemClient client = new AmazonElasticFileSystemClient(creds, config);
            
            DescribeAccessPointsResponse resp = new DescribeAccessPointsResponse();
            do
            {
                DescribeAccessPointsRequest req = new DescribeAccessPointsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeAccessPoints(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.AccessPoints)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}