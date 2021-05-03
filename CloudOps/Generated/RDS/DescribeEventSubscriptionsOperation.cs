using Amazon;
using Amazon.RDS;
using Amazon.RDS.Model;
using Amazon.Runtime;

namespace CloudOps.RDS
{
    public class DescribeEventSubscriptionsOperation : Operation
    {
        public override string Name => "DescribeEventSubscriptions";

        public override string Description => "";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "RDS";

        public override string ServiceID => "RDS";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRDSConfig config = new AmazonRDSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRDSClient client = new AmazonRDSClient(creds, config);
            
            DescribeEventSubscriptionsResponse resp = new DescribeEventSubscriptionsResponse();
            do
            {
                DescribeEventSubscriptionsRequest req = new DescribeEventSubscriptionsRequest
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = client.DescribeEventSubscriptions(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.EventSubscriptionsList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}