using Amazon;
using Amazon.GlueDataBrew;
using Amazon.GlueDataBrew.Model;
using Amazon.Runtime;

namespace CloudOps.GlueDataBrew
{
    public class ListSchedulesOperation : Operation
    {
        public override string Name => "ListSchedules";

        public override string Description => "Lists the DataBrew schedules that are defined.";
 
        public override string RequestURI => "/schedules";

        public override string Method => "GET";

        public override string ServiceName => "GlueDataBrew";

        public override string ServiceID => "DataBrew";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGlueDataBrewConfig config = new AmazonGlueDataBrewConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGlueDataBrewClient client = new AmazonGlueDataBrewClient(creds, config);
            
            ListSchedulesResponse resp = new ListSchedulesResponse();
            do
            {
                ListSchedulesRequest req = new ListSchedulesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListSchedulesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Schedules)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}