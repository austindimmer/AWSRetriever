using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeTransitGatewayPeeringAttachmentsOperation : Operation
    {
        public override string Name => "DescribeTransitGatewayPeeringAttachments";

        public override string Description => "Describes your transit gateway peering attachments.";
 
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
            
            DescribeTransitGatewayPeeringAttachmentsResponse resp = new DescribeTransitGatewayPeeringAttachmentsResponse();
            do
            {
                DescribeTransitGatewayPeeringAttachmentsRequest req = new DescribeTransitGatewayPeeringAttachmentsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeTransitGatewayPeeringAttachments(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.TransitGatewayPeeringAttachments)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}