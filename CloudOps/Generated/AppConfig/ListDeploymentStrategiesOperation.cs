using Amazon;
using Amazon.AppConfig;
using Amazon.AppConfig.Model;
using Amazon.Runtime;

namespace CloudOps.AppConfig
{
    public class ListDeploymentStrategiesOperation : Operation
    {
        public override string Name => "ListDeploymentStrategies";

        public override string Description => "List deployment strategies.";
 
        public override string RequestURI => "/deploymentstrategies";

        public override string Method => "GET";

        public override string ServiceName => "AppConfig";

        public override string ServiceID => "AppConfig";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAppConfigConfig config = new AmazonAppConfigConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAppConfigClient client = new AmazonAppConfigClient(creds, config);
            
            ListDeploymentStrategiesResponse resp = new ListDeploymentStrategiesResponse();
            do
            {
                ListDeploymentStrategiesRequest req = new ListDeploymentStrategiesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListDeploymentStrategies(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Items)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}