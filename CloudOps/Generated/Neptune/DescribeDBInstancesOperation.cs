using Amazon;
using Amazon.Neptune;
using Amazon.Neptune.Model;
using Amazon.Runtime;

namespace CloudOps.Neptune
{
    public class DescribeDBInstancesOperation : Operation
    {
        public override string Name => "DescribeDBInstances";

        public override string Description => "Returns information about provisioned instances, and supports pagination.  This operation can also return information for Amazon RDS instances and Amazon DocDB instances. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Neptune";

        public override string ServiceID => "Neptune";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonNeptuneConfig config = new AmazonNeptuneConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonNeptuneClient client = new AmazonNeptuneClient(creds, config);
            
            DescribeDBInstancesResponse resp = new DescribeDBInstancesResponse();
            do
            {
                try
                {
                    DescribeDBInstancesRequest req = new DescribeDBInstancesRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeDBInstancesAsync(req);
                    
                    foreach (var obj in resp.DBInstances)
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