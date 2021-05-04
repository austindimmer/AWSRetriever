using Amazon;
using Amazon.AppConfig;
using Amazon.AppConfig.Model;
using Amazon.Runtime;

namespace CloudOps.AppConfig
{
    public class ListApplicationsOperation : Operation
    {
        public override string Name => "ListApplications";

        public override string Description => "List all applications in your AWS account.";
 
        public override string RequestURI => "/applications";

        public override string Method => "GET";

        public override string ServiceName => "AppConfig";

        public override string ServiceID => "AppConfig";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAppConfigConfig config = new AmazonAppConfigConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAppConfigClient client = new AmazonAppConfigClient(creds, config);
            
            ListApplicationsResponse resp = new ListApplicationsResponse();
            do
            {
                ListApplicationsRequest req = new ListApplicationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListApplications(req);
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