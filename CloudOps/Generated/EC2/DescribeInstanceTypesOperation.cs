using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeInstanceTypesOperation : Operation
    {
        public override string Name => "DescribeInstanceTypes";

        public override string Description => "Describes the details of the instance types that are offered in a location. The results can be filtered by the attributes of the instance types.";
 
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
            
            DescribeInstanceTypesResponse resp = new DescribeInstanceTypesResponse();
            do
            {
                DescribeInstanceTypesRequest req = new DescribeInstanceTypesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeInstanceTypes(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.InstanceTypes)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}