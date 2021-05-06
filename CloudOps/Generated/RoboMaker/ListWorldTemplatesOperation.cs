using Amazon;
using Amazon.RoboMaker;
using Amazon.RoboMaker.Model;
using Amazon.Runtime;

namespace CloudOps.RoboMaker
{
    public class ListWorldTemplatesOperation : Operation
    {
        public override string Name => "ListWorldTemplates";

        public override string Description => "Lists world templates.";
 
        public override string RequestURI => "/listWorldTemplates";

        public override string Method => "POST";

        public override string ServiceName => "RoboMaker";

        public override string ServiceID => "RoboMaker";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoboMakerConfig config = new AmazonRoboMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoboMakerClient client = new AmazonRoboMakerClient(creds, config);
            
            ListWorldTemplatesResponse resp = new ListWorldTemplatesResponse();
            do
            {
                try
                {
                    ListWorldTemplatesRequest req = new ListWorldTemplatesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListWorldTemplatesAsync(req);
                    
                    foreach (var obj in resp.TemplateSummaries)
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