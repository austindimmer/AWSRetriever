using Amazon;
using Amazon.LookoutEquipment;
using Amazon.LookoutEquipment.Model;
using Amazon.Runtime;

namespace CloudOps.LookoutEquipment
{
    public class ListDataIngestionJobsOperation : Operation
    {
        public override string Name => "ListDataIngestionJobs";

        public override string Description => "Provides a list of all data ingestion jobs, including dataset name and ARN, S3 location of the input data, status, and so on. ";
 
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
            
            ListDataIngestionJobsResponse resp = new ListDataIngestionJobsResponse();
            do
            {
                try
                {
                    ListDataIngestionJobsRequest req = new ListDataIngestionJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListDataIngestionJobsAsync(req);
                    
                    foreach (var obj in resp.DataIngestionJobSummaries)
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