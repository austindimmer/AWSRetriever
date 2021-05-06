using Amazon;
using Amazon.CodeDeploy;
using Amazon.CodeDeploy.Model;
using Amazon.Runtime;

namespace CloudOps.CodeDeploy
{
    public class ListDeploymentsOperation : Operation
    {
        public override string Name => "ListDeployments";

        public override string Description => "Lists the deployments in a deployment group for an application registered with the IAM user or AWS account.";
 
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
            
            ListDeploymentsResponse resp = new ListDeploymentsResponse();
            do
            {
                try
                {
                    ListDeploymentsRequest req = new ListDeploymentsRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListDeploymentsAsync(req);
                    
                    foreach (var obj in resp.Deployments)
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