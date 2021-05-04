using Amazon;
using Amazon.ElasticMapReduce;
using Amazon.ElasticMapReduce.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticMapReduce
{
    public class ListStepsOperation : Operation
    {
        public override string Name => "ListSteps";

        public override string Description => "Provides a list of steps for the cluster in reverse order unless you specify stepIds with the request of filter by StepStates. You can specify a maximum of 10 stepIDs.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "ElasticMapReduce";

        public override string ServiceID => "ElasticMapReduce";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticMapReduceConfig config = new AmazonElasticMapReduceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElasticMapReduceClient client = new AmazonElasticMapReduceClient(creds, config);
            
            ListStepsResponse resp = new ListStepsResponse();
            do
            {
                ListStepsRequest req = new ListStepsRequest
                {
                    Marker = resp.Marker
                                        
                };

                resp = client.ListSteps(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Steps)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}