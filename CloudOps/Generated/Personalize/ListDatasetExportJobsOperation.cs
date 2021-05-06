using Amazon;
using Amazon.Personalize;
using Amazon.Personalize.Model;
using Amazon.Runtime;

namespace CloudOps.Personalize
{
    public class ListDatasetExportJobsOperation : Operation
    {
        public override string Name => "ListDatasetExportJobs";

        public override string Description => "Returns a list of dataset export jobs that use the given dataset. When a dataset is not specified, all the dataset export jobs associated with the account are listed. The response provides the properties for each dataset export job, including the Amazon Resource Name (ARN). For more information on dataset export jobs, see CreateDatasetExportJob. For more information on datasets, see CreateDataset.";
 
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
            
            ListDatasetExportJobsResponse resp = new ListDatasetExportJobsResponse();
            do
            {
                try
                {
                    ListDatasetExportJobsRequest req = new ListDatasetExportJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListDatasetExportJobsAsync(req);
                    
                    foreach (var obj in resp.DatasetExportJobs)
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