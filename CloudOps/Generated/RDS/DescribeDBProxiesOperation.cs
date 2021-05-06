using Amazon;
using Amazon.RDS;
using Amazon.RDS.Model;
using Amazon.Runtime;

namespace CloudOps.RDS
{
    public class DescribeDBProxiesOperation : Operation
    {
        public override string Name => "DescribeDBProxies";

        public override string Description => "Returns information about DB proxies.";
 
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
            
            DescribeDBProxiesResponse resp = new DescribeDBProxiesResponse();
            do
            {
                try
                {
                    DescribeDBProxiesRequest req = new DescribeDBProxiesRequest
                    {
                        Marker = resp.Marker
                        ,
                        MaxRecords = maxItems
                                            
                    };

                    resp = await client.DescribeDBProxiesAsync(req);
                    
                    foreach (var obj in resp.DBProxies)
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