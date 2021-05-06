using Amazon;
using Amazon.RoboMaker;
using Amazon.RoboMaker.Model;
using Amazon.Runtime;

namespace CloudOps.RoboMaker
{
    public class ListWorldExportJobsOperation : Operation
    {
        public override string Name => "ListWorldExportJobs";

        public override string Description => "Lists world export jobs.";
 
        public override string RequestURI => "/listWorldExportJobs";

        public override string Method => "POST";

        public override string ServiceName => "RoboMaker";

        public override string ServiceID => "RoboMaker";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoboMakerConfig config = new AmazonRoboMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoboMakerClient client = new AmazonRoboMakerClient(creds, config);
            
            ListWorldExportJobsResponse resp = new ListWorldExportJobsResponse();
            do
            {
                try
                {
                    ListWorldExportJobsRequest req = new ListWorldExportJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListWorldExportJobsAsync(req);
                    
                    foreach (var obj in resp.WorldExportJobSummaries)
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