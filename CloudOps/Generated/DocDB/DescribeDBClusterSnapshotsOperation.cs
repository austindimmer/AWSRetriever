using Amazon;
using Amazon.DocDB;
using Amazon.DocDB.Model;
using Amazon.Runtime;

namespace CloudOps.DocDB
{
    public class DescribeDBClusterSnapshotsOperation : Operation
    {
        public override string Name => "DescribeDBClusterSnapshots";

        public override string Description => "Returns information about cluster snapshots. This API operation supports pagination.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DocDB";

        public override string ServiceID => "DocDB";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDocDBConfig config = new AmazonDocDBConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDocDBClient client = new AmazonDocDBClient(creds, config);
            
            DescribeDBClusterSnapshotsResponse resp = new DescribeDBClusterSnapshotsResponse();
            do
            {
                try
                {
                    DescribeDBClusterSnapshotsRequest req = new DescribeDBClusterSnapshotsRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeDBClusterSnapshotsAsync(req);
                    
                    foreach (var obj in resp.DBClusterSnapshots)
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