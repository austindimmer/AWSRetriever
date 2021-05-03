using Amazon;
using Amazon.FIS;
using Amazon.FIS.Model;
using Amazon.Runtime;

namespace CloudOps.FIS
{
    public class ListExperimentsOperation : Operation
    {
        public override string Name => "ListExperiments";

        public override string Description => "Lists your experiments.";
 
        public override string RequestURI => "/experiments";

        public override string Method => "GET";

        public override string ServiceName => "FIS";

        public override string ServiceID => "fis";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonFISConfig config = new AmazonFISConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonFISClient client = new AmazonFISClient(creds, config);
            
            ListExperimentsResponse resp = new ListExperimentsResponse();
            do
            {
                ListExperimentsRequest req = new ListExperimentsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListExperiments(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Experiments)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}