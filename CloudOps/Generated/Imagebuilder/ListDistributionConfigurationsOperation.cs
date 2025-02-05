using Amazon;
using Amazon.Imagebuilder;
using Amazon.Imagebuilder.Model;
using Amazon.Runtime;

namespace CloudOps.Imagebuilder
{
    public class ListDistributionConfigurationsOperation : Operation
    {
        public override string Name => "ListDistributionConfigurations";

        public override string Description => "Returns a list of distribution configurations.";
 
        public override string RequestURI => "/ListDistributionConfigurations";

        public override string Method => "POST";

        public override string ServiceName => "Imagebuilder";

        public override string ServiceID => "imagebuilder";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonImagebuilderConfig config = new AmazonImagebuilderConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonImagebuilderClient client = new AmazonImagebuilderClient(creds, config);
            
            ListDistributionConfigurationsResponse resp = new ListDistributionConfigurationsResponse();
            do
            {
                ListDistributionConfigurationsRequest req = new ListDistributionConfigurationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListDistributionConfigurations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DistributionConfigurationSummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}