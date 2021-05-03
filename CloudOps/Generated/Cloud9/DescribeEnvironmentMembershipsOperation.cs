using Amazon;
using Amazon.Cloud9;
using Amazon.Cloud9.Model;
using Amazon.Runtime;

namespace CloudOps.Cloud9
{
    public class DescribeEnvironmentMembershipsOperation : Operation
    {
        public override string Name => "DescribeEnvironmentMemberships";

        public override string Description => "Gets information about environment members for an AWS Cloud9 development environment.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Cloud9";

        public override string ServiceID => "Cloud9";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloud9Config config = new AmazonCloud9Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloud9Client client = new AmazonCloud9Client(creds, config);
            
            DescribeEnvironmentMembershipsResponse resp = new DescribeEnvironmentMembershipsResponse();
            do
            {
                DescribeEnvironmentMembershipsRequest req = new DescribeEnvironmentMembershipsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeEnvironmentMemberships(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Memberships)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}