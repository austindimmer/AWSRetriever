using Amazon;
using Amazon.FIS;
using Amazon.FIS.Model;
using Amazon.Runtime;

namespace CloudOps.FIS
{
    public class ListExperimentTemplatesOperation : Operation
    {
        public override string Name => "ListExperimentTemplates";

        public override string Description => "Lists your experiment templates.";
 
        public override string RequestURI => "/experimentTemplates";

        public override string Method => "GET";

        public override string ServiceName => "FIS";

        public override string ServiceID => "fis";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonFISConfig config = new AmazonFISConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonFISClient client = new AmazonFISClient(creds, config);
            
            ListExperimentTemplatesResponse resp = new ListExperimentTemplatesResponse();
            do
            {
                try
                {
                    ListExperimentTemplatesRequest req = new ListExperimentTemplatesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListExperimentTemplatesAsync(req);
                    
                    foreach (var obj in resp.ExperimentTemplates)
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