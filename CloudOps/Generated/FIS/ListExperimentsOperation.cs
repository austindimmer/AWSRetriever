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

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonFISConfig config = new AmazonFISConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonFISClient client = new AmazonFISClient(creds, config);
            
            ListExperimentsResponse resp = new ListExperimentsResponse();
            do
            {
                try
                {
                    ListExperimentsRequest req = new ListExperimentsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListExperimentsAsync(req);
                    
                    foreach (var obj in resp.Experiments)
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