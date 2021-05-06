using Amazon;
using Amazon.PrometheusService;
using Amazon.PrometheusService.Model;
using Amazon.Runtime;

namespace CloudOps.PrometheusService
{
    public class ListWorkspacesOperation : Operation
    {
        public override string Name => "ListWorkspaces";

        public override string Description => "Lists all AMP workspaces, including workspaces being created or deleted.";
 
        public override string RequestURI => "/workspaces";

        public override string Method => "GET";

        public override string ServiceName => "PrometheusService";

        public override string ServiceID => "amp";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonPrometheusServiceConfig config = new AmazonPrometheusServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonPrometheusServiceClient client = new AmazonPrometheusServiceClient(creds, config);
            
            ListWorkspacesResponse resp = new ListWorkspacesResponse();
            do
            {
                try
                {
                    ListWorkspacesRequest req = new ListWorkspacesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListWorkspacesAsync(req);
                    
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