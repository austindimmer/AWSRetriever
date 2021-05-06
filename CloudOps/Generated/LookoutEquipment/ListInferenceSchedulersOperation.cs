using Amazon;
using Amazon.LookoutEquipment;
using Amazon.LookoutEquipment.Model;
using Amazon.Runtime;

namespace CloudOps.LookoutEquipment
{
    public class ListInferenceSchedulersOperation : Operation
    {
        public override string Name => "ListInferenceSchedulers";

        public override string Description => "Retrieves a list of all inference schedulers currently available for your account. ";
 
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
            
            ListInferenceSchedulersResponse resp = new ListInferenceSchedulersResponse();
            do
            {
                try
                {
                    ListInferenceSchedulersRequest req = new ListInferenceSchedulersRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListInferenceSchedulersAsync(req);
                    
                    foreach (var obj in resp.InferenceSchedulerSummaries)
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