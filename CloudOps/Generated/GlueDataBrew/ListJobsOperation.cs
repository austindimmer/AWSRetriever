using Amazon;
using Amazon.GlueDataBrew;
using Amazon.GlueDataBrew.Model;
using Amazon.Runtime;

namespace CloudOps.GlueDataBrew
{
    public class ListJobsOperation : Operation
    {
        public override string Name => "ListJobs";

        public override string Description => "Lists all of the DataBrew jobs that are defined.";
 
        public override string RequestURI => "/jobs";

        public override string Method => "GET";

        public override string ServiceName => "GlueDataBrew";

        public override string ServiceID => "DataBrew";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGlueDataBrewConfig config = new AmazonGlueDataBrewConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGlueDataBrewClient client = new AmazonGlueDataBrewClient(creds, config);
            
            ListJobsResponse resp = new ListJobsResponse();
            do
            {
                try
                {
                    ListJobsRequest req = new ListJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListJobsAsync(req);
                    
                    foreach (var obj in resp.Jobs)
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