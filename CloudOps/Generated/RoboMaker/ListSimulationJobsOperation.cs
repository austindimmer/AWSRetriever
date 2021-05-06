using Amazon;
using Amazon.RoboMaker;
using Amazon.RoboMaker.Model;
using Amazon.Runtime;

namespace CloudOps.RoboMaker
{
    public class ListSimulationJobsOperation : Operation
    {
        public override string Name => "ListSimulationJobs";

        public override string Description => "Returns a list of simulation jobs. You can optionally provide filters to retrieve specific simulation jobs. ";
 
        public override string RequestURI => "/listSimulationJobs";

        public override string Method => "POST";

        public override string ServiceName => "RoboMaker";

        public override string ServiceID => "RoboMaker";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoboMakerConfig config = new AmazonRoboMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoboMakerClient client = new AmazonRoboMakerClient(creds, config);
            
            ListSimulationJobsResponse resp = new ListSimulationJobsResponse();
            do
            {
                try
                {
                    ListSimulationJobsRequest req = new ListSimulationJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListSimulationJobsAsync(req);
                    
                    foreach (var obj in resp.SimulationJobSummaries)
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