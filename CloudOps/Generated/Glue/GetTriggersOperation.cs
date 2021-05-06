using Amazon;
using Amazon.Glue;
using Amazon.Glue.Model;
using Amazon.Runtime;

namespace CloudOps.Glue
{
    public class GetTriggersOperation : Operation
    {
        public override string Name => "GetTriggers";

        public override string Description => "Gets all the triggers associated with a job.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Glue";

        public override string ServiceID => "Glue";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGlueConfig config = new AmazonGlueConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGlueClient client = new AmazonGlueClient(creds, config);
            
            GetTriggersResponse resp = new GetTriggersResponse();
            do
            {
                try
                {
                    GetTriggersRequest req = new GetTriggersRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.GetTriggersAsync(req);
                    
                    foreach (var obj in resp.Triggers)
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