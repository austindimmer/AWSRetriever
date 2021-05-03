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

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonPrometheusServiceConfig config = new AmazonPrometheusServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonPrometheusServiceClient client = new AmazonPrometheusServiceClient(creds, config);
            
            ListWorkspacesResponse resp = new ListWorkspacesResponse();
            do
            {
                ListWorkspacesRequest req = new ListWorkspacesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListWorkspaces(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Workspaces)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}