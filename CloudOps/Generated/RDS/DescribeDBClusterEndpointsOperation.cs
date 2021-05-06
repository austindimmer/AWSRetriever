using Amazon;
using Amazon.RDS;
using Amazon.RDS.Model;
using Amazon.Runtime;

namespace CloudOps.RDS
{
    public class DescribeDBClusterEndpointsOperation : Operation
    {
        public override string Name => "DescribeDBClusterEndpoints";

        public override string Description => "Returns information about endpoints for an Amazon Aurora DB cluster.  This action only applies to Aurora DB clusters. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "RDS";

        public override string ServiceID => "RDS";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRDSConfig config = new AmazonRDSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRDSClient client = new AmazonRDSClient(creds, config);
            
            DescribeDBClusterEndpointsResponse resp = new DescribeDBClusterEndpointsResponse();
            do
            {
                try
                {
                    DescribeDBClusterEndpointsRequest req = new DescribeDBClusterEndpointsRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeDBClusterEndpointsAsync(req);
                    
                    foreach (var obj in resp.DBClusterEndpoints)
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