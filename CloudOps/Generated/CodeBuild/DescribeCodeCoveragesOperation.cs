using Amazon;
using Amazon.CodeBuild;
using Amazon.CodeBuild.Model;
using Amazon.Runtime;

namespace CloudOps.CodeBuild
{
    public class DescribeCodeCoveragesOperation : Operation
    {
        public override string Name => "DescribeCodeCoverages";

        public override string Description => "Retrieves one or more code coverage reports.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeBuild";

        public override string ServiceID => "CodeBuild";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeBuildConfig config = new AmazonCodeBuildConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeBuildClient client = new AmazonCodeBuildClient(creds, config);
            
            DescribeCodeCoveragesResponse resp = new DescribeCodeCoveragesResponse();
            do
            {
                DescribeCodeCoveragesRequest req = new DescribeCodeCoveragesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.DescribeCodeCoverages(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CodeCoverages)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}