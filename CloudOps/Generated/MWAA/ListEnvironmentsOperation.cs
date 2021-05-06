using Amazon;
using Amazon.MWAA;
using Amazon.MWAA.Model;
using Amazon.Runtime;

namespace CloudOps.MWAA
{
    public class ListEnvironmentsOperation : Operation
    {
        public override string Name => "ListEnvironments";

        public override string Description => "List Amazon MWAA Environments.";
 
        public override string RequestURI => "/environments";

        public override string Method => "GET";

        public override string ServiceName => "MWAA";

        public override string ServiceID => "MWAA";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMWAAConfig config = new AmazonMWAAConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMWAAClient client = new AmazonMWAAClient(creds, config);
            
            ListEnvironmentsResponse resp = new ListEnvironmentsResponse();
            do
            {
                ListEnvironmentsRequest req = new ListEnvironmentsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListEnvironmentsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Environments)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}