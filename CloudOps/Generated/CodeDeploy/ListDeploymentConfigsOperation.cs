using Amazon;
using Amazon.CodeDeploy;
using Amazon.CodeDeploy.Model;
using Amazon.Runtime;

namespace CloudOps.CodeDeploy
{
    public class ListDeploymentConfigsOperation : Operation
    {
        public override string Name => "ListDeploymentConfigs";

        public override string Description => "Lists the deployment configurations with the IAM user or AWS account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeDeploy";

        public override string ServiceID => "CodeDeploy";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeDeployConfig config = new AmazonCodeDeployConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeDeployClient client = new AmazonCodeDeployClient(creds, config);
            
            ListDeploymentConfigsResponse resp = new ListDeploymentConfigsResponse();
            do
            {
                try
                {
                    ListDeploymentConfigsRequest req = new ListDeploymentConfigsRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListDeploymentConfigsAsync(req);
                    
                    foreach (var obj in resp.DeploymentConfigsList)
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