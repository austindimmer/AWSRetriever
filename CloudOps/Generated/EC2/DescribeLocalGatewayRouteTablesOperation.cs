using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeLocalGatewayRouteTablesOperation : Operation
    {
        public override string Name => "DescribeLocalGatewayRouteTables";

        public override string Description => "Describes one or more local gateway route tables. By default, all local gateway route tables are described. Alternatively, you can filter the results.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EC2";

        public override string ServiceID => "EC2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEC2Config config = new AmazonEC2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEC2Client client = new AmazonEC2Client(creds, config);
            
            DescribeLocalGatewayRouteTablesResponse resp = new DescribeLocalGatewayRouteTablesResponse();
            do
            {
                try
                {
                    DescribeLocalGatewayRouteTablesRequest req = new DescribeLocalGatewayRouteTablesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeLocalGatewayRouteTablesAsync(req);
                    
                    foreach (var obj in resp.LocalGatewayRouteTables)
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