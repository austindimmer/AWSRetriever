using Amazon;
using Amazon.Chime;
using Amazon.Chime.Model;
using Amazon.Runtime;

namespace CloudOps.Chime
{
    public class ListAppInstancesOperation : Operation
    {
        public override string Name => "ListAppInstances";

        public override string Description => "Lists all Amazon Chime AppInstances created under a single AWS account.";
 
        public override string RequestURI => "/app-instances";

        public override string Method => "GET";

        public override string ServiceName => "Chime";

        public override string ServiceID => "Chime";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonChimeConfig config = new AmazonChimeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonChimeClient client = new AmazonChimeClient(creds, config);
            
            ListAppInstancesResponse resp = new ListAppInstancesResponse();
            do
            {
                try
                {
                    ListAppInstancesRequest req = new ListAppInstancesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListAppInstancesAsync(req);
                    
                    foreach (var obj in resp.AppInstances)
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