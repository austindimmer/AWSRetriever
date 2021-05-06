using Amazon;
using Amazon.LookoutEquipment;
using Amazon.LookoutEquipment.Model;
using Amazon.Runtime;

namespace CloudOps.LookoutEquipment
{
    public class ListDatasetsOperation : Operation
    {
        public override string Name => "ListDatasets";

        public override string Description => "Lists all datasets currently available in your account, filtering on the dataset name. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "LookoutEquipment";

        public override string ServiceID => "LookoutEquipment";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonLookoutEquipmentConfig config = new AmazonLookoutEquipmentConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonLookoutEquipmentClient client = new AmazonLookoutEquipmentClient(creds, config);
            
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
                    
                    foreach (var obj in resp.DatasetSummaries)
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