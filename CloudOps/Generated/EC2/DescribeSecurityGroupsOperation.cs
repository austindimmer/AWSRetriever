using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeSecurityGroupsOperation : Operation
    {
        public override string Name => "DescribeSecurityGroups";

        public override string Description => "Describes the specified security groups or all of your security groups. A security group is for use with instances either in the EC2-Classic platform or in a specific VPC. For more information, see Amazon EC2 Security Groups in the Amazon Elastic Compute Cloud User Guide and Security Groups for Your VPC in the Amazon Virtual Private Cloud User Guide.";
 
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
            
            DescribeSecurityGroupsResponse resp = new DescribeSecurityGroupsResponse();
            do
            {
                DescribeSecurityGroupsRequest req = new DescribeSecurityGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeSecurityGroups(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SecurityGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}