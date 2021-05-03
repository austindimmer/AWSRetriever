using Amazon;
using Amazon.AppRegistry;
using Amazon.AppRegistry.Model;
using Amazon.Runtime;

namespace CloudOps.AppRegistry
{
    public class ListApplicationsOperation : Operation
    {
        public override string Name => "ListApplications";

        public override string Description => "Retrieves a list of all of your applications. Results are paginated.";
 
        public override string RequestURI => "/applications";

        public override string Method => "GET";

        public override string ServiceName => "AppRegistry";

        public override string ServiceID => "Service Catalog AppRegistry";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAppRegistryConfig config = new AmazonAppRegistryConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAppRegistryClient client = new AmazonAppRegistryClient(creds, config);
            
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
                
                foreach (var obj in resp.Applications)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}