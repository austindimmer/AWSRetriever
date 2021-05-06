using Amazon;
using Amazon.WorkSpaces;
using Amazon.WorkSpaces.Model;
using Amazon.Runtime;

namespace CloudOps.WorkSpaces
{
    public class DescribeWorkspaceBundlesOperation : Operation
    {
        public override string Name => "DescribeWorkspaceBundles";

        public override string Description => "Retrieves a list that describes the available WorkSpace bundles. You can filter the results using either bundle ID or owner, but not both.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "WorkSpaces";

        public override string ServiceID => "WorkSpaces";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonWorkSpacesConfig config = new AmazonWorkSpacesConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonWorkSpacesClient client = new AmazonWorkSpacesClient(creds, config);
            
            DescribeWorkspaceBundlesResponse resp = new DescribeWorkspaceBundlesResponse();
            do
            {
                DescribeWorkspaceBundlesRequest req = new DescribeWorkspaceBundlesRequest
                {
                    NextToken = resp.NextToken
                                        
                };

                resp = await client.DescribeWorkspaceBundlesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Bundles)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}