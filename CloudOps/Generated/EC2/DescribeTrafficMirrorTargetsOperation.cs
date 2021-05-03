using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeTrafficMirrorTargetsOperation : Operation
    {
        public override string Name => "DescribeTrafficMirrorTargets";

        public override string Description => "Information about one or more Traffic Mirror targets.";
 
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
            
            DescribeTrafficMirrorTargetsResponse resp = new DescribeTrafficMirrorTargetsResponse();
            do
            {
                DescribeTrafficMirrorTargetsRequest req = new DescribeTrafficMirrorTargetsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeTrafficMirrorTargets(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.TrafficMirrorTargets)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}