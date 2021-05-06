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

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonImagebuilderConfig config = new AmazonImagebuilderConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonImagebuilderClient client = new AmazonImagebuilderClient(creds, config);
            
            ListInfrastructureConfigurationsResponse resp = new ListInfrastructureConfigurationsResponse();
            do
            {
                try
                {
                    ListInfrastructureConfigurationsRequest req = new ListInfrastructureConfigurationsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListInfrastructureConfigurationsAsync(req);
                    
                    foreach (var obj in resp.InfrastructureConfigurationSummaryList)
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