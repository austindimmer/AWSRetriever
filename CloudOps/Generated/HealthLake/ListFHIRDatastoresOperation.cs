using Amazon;
using Amazon.HealthLake;
using Amazon.HealthLake.Model;
using Amazon.Runtime;

namespace CloudOps.HealthLake
{
    public class ListFHIRDatastoresOperation : Operation
    {
        public override string Name => "ListFHIRDatastores";

        public override string Description => "Lists all FHIR Data Stores that are in the user’s account, regardless of Data Store status.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "HealthLake";

        public override string ServiceID => "HealthLake";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonHealthLakeConfig config = new AmazonHealthLakeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonHealthLakeClient client = new AmazonHealthLakeClient(creds, config);
            
            ListFHIRDatastoresResponse resp = new ListFHIRDatastoresResponse();
            do
            {
                ListFHIRDatastoresRequest req = new ListFHIRDatastoresRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListFHIRDatastoresAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DatastorePropertiesList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}