using Amazon;
using Amazon.LookoutEquipment;
using Amazon.LookoutEquipment.Model;
using Amazon.Runtime;

namespace CloudOps.LookoutEquipment
{
    public class ListModelsOperation : Operation
    {
        public override string Name => "ListModels";

        public override string Description => "Generates a list of all models in the account, including model name and ARN, dataset, and status. ";
 
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
            
            ListModelsResponse resp = new ListModelsResponse();
            do
            {
                ListModelsRequest req = new ListModelsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListModelsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ModelSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}