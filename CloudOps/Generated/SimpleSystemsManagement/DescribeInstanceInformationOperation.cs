using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleSystemsManagement
{
    public class DescribeInstanceInformationOperation : Operation
    {
        public override string Name => "DescribeInstanceInformation";

        public override string Description => "Describes one or more of your instances, including information about the operating system platform, the version of SSM Agent installed on the instance, instance status, and so on. If you specify one or more instance IDs, it returns information for those instances. If you do not specify instance IDs, it returns information for all your instances. If you specify an instance ID that is not valid or an instance that you do not own, you receive an error.  The IamRole field for this API action is the Amazon Identity and Access Management (IAM) role assigned to on-premises instances. This call does not return the IAM role for EC2 instances. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SimpleSystemsManagement";

        public override string ServiceID => "SSM";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSimpleSystemsManagementConfig config = new AmazonSimpleSystemsManagementConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSimpleSystemsManagementClient client = new AmazonSimpleSystemsManagementClient(creds, config);
            
            DescribeInstanceInformationResponse resp = new DescribeInstanceInformationResponse();
            do
            {
                DescribeInstanceInformationRequest req = new DescribeInstanceInformationRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.DescribeInstanceInformationAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.InstanceInformationList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}