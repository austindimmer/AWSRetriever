using Amazon;
using Amazon.Personalize;
using Amazon.Personalize.Model;
using Amazon.Runtime;

namespace CloudOps.Personalize
{
    public class ListDatasetsOperation : Operation
    {
        public override string Name => "ListDatasets";

        public override string Description => "Returns the list of datasets contained in the given dataset group. The response provides the properties for each dataset, including the Amazon Resource Name (ARN). For more information on datasets, see CreateDataset.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Personalize";

        public override string ServiceID => "Personalize";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonPersonalizeConfig config = new AmazonPersonalizeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonPersonalizeClient client = new AmazonPersonalizeClient(creds, config);
            
            ListDatasetsResponse resp = new ListDatasetsResponse();
            do
            {
                try
                {
                    ListDatasetsRequest req = new ListDatasetsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListDatasetsAsync(req);
                    
                    foreach (var obj in resp.Datasets)
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