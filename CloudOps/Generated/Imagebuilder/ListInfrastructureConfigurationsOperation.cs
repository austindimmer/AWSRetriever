using Amazon;
using Amazon.Imagebuilder;
using Amazon.Imagebuilder.Model;
using Amazon.Runtime;

namespace CloudOps.Imagebuilder
{
    public class ListInfrastructureConfigurationsOperation : Operation
    {
        public override string Name => "ListInfrastructureConfigurations";

        public override string Description => " Returns a list of infrastructure configurations.";
 
        public override string RequestURI => "/ListInfrastructureConfigurations";

        public override string Method => "POST";

        public override string ServiceName => "Imagebuilder";

        public override string ServiceID => "imagebuilder";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonImagebuilderConfig config = new AmazonImagebuilderConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonImagebuilderClient client = new AmazonImagebuilderClient(creds, config);
            
            ListInfrastructureConfigurationsResponse resp = new ListInfrastructureConfigurationsResponse();
            do
            {
                ListInfrastructureConfigurationsRequest req = new ListInfrastructureConfigurationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListInfrastructureConfigurations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.InfrastructureConfigurationSummaryList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}