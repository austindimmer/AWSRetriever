using Amazon;
using Amazon.DeviceFarm;
using Amazon.DeviceFarm.Model;
using Amazon.Runtime;

namespace CloudOps.DeviceFarm
{
    public class ListTestGridProjectsOperation : Operation
    {
        public override string Name => "ListTestGridProjects";

        public override string Description => "Gets a list of all Selenium testing projects in your account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DeviceFarm";

        public override string ServiceID => "Device Farm";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDeviceFarmConfig config = new AmazonDeviceFarmConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonDeviceFarmClient client = new AmazonDeviceFarmClient(creds, config);
            
            ListTestGridProjectsResponse resp = new ListTestGridProjectsResponse();
            do
            {
                ListTestGridProjectsRequest req = new ListTestGridProjectsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResult = maxItems
                                        
                };

                resp = await client.ListTestGridProjectsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.TestGridProjects)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}