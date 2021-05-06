using Amazon;
using Amazon.WorkSpaces;
using Amazon.WorkSpaces.Model;
using Amazon.Runtime;

namespace CloudOps.WorkSpaces
{
    public class DescribeWorkspaceDirectoriesOperation : Operation
    {
        public override string Name => "DescribeWorkspaceDirectories";

        public override string Description => "Describes the available directories that are registered with Amazon WorkSpaces.";
 
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
            
            DescribeWorkspaceDirectoriesResponse resp = new DescribeWorkspaceDirectoriesResponse();
            do
            {
                try
                {
                    DescribeWorkspaceDirectoriesRequest req = new DescribeWorkspaceDirectoriesRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.DescribeWorkspaceDirectoriesAsync(req);
                    
                    foreach (var obj in resp.Directories)
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