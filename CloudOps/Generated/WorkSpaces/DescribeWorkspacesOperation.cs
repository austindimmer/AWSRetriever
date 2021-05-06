using Amazon;
using Amazon.WorkSpaces;
using Amazon.WorkSpaces.Model;
using Amazon.Runtime;

namespace CloudOps.WorkSpaces
{
    public class DescribeWorkspacesOperation : Operation
    {
        public override string Name => "DescribeWorkspaces";

        public override string Description => "Describes the specified WorkSpaces. You can filter the results by using the bundle identifier, directory identifier, or owner, but you can specify only one filter at a time.";
 
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
            
            DescribeWorkspacesResponse resp = new DescribeWorkspacesResponse();
            do
            {
                try
                {
                    DescribeWorkspacesRequest req = new DescribeWorkspacesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        Limit = maxItems
                                            
                    };

                    resp = await client.DescribeWorkspacesAsync(req);
                    
                    foreach (var obj in resp.Workspaces)
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