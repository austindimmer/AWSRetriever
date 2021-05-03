using Amazon;
using Amazon.RoboMaker;
using Amazon.RoboMaker.Model;
using Amazon.Runtime;

namespace CloudOps.RoboMaker
{
    public class ListSimulationJobBatchesOperation : Operation
    {
        public override string Name => "ListSimulationJobBatches";

        public override string Description => "Returns a list simulation job batches. You can optionally provide filters to retrieve specific simulation batch jobs. ";
 
        public override string RequestURI => "/listSimulationJobBatches";

        public override string Method => "POST";

        public override string ServiceName => "RoboMaker";

        public override string ServiceID => "RoboMaker";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRoboMakerConfig config = new AmazonRoboMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRoboMakerClient client = new AmazonRoboMakerClient(creds, config);
            
            ListSimulationJobBatchesResponse resp = new ListSimulationJobBatchesResponse();
            do
            {
                ListSimulationJobBatchesRequest req = new ListSimulationJobBatchesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListSimulationJobBatches(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SimulationJobBatchSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}