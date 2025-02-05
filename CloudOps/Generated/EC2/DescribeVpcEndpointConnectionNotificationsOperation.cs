using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeVpcEndpointConnectionNotificationsOperation : Operation
    {
        public override string Name => "DescribeVpcEndpointConnectionNotifications";

        public override string Description => "Describes the connection notifications for VPC endpoints and VPC endpoint services.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EC2";

        public override string ServiceID => "EC2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEC2Config config = new AmazonEC2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEC2Client client = new AmazonEC2Client(creds, config);
            
            DescribeVpcEndpointConnectionNotificationsResponse resp = new DescribeVpcEndpointConnectionNotificationsResponse();
            do
            {
                DescribeVpcEndpointConnectionNotificationsRequest req = new DescribeVpcEndpointConnectionNotificationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeVpcEndpointConnectionNotifications(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ConnectionNotificationSet)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}