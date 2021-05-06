using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeTransitGatewayVpcAttachmentsOperation : Operation
    {
        public override string Name => "DescribeTransitGatewayVpcAttachments";

        public override string Description => "Describes one or more VPC attachments. By default, all VPC attachments are described. Alternatively, you can filter the results.";
 
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
            
            DescribeTransitGatewayVpcAttachmentsResponse resp = new DescribeTransitGatewayVpcAttachmentsResponse();
            do
            {
                try
                {
                    DescribeTransitGatewayVpcAttachmentsRequest req = new DescribeTransitGatewayVpcAttachmentsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeTransitGatewayVpcAttachmentsAsync(req);
                    
                    foreach (var obj in resp.TransitGatewayVpcAttachments)
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