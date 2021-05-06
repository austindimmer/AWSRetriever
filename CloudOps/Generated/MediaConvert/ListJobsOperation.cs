using Amazon;
using Amazon.MediaConvert;
using Amazon.MediaConvert.Model;
using Amazon.Runtime;

namespace CloudOps.MediaConvert
{
    public class ListJobsOperation : Operation
    {
        public override string Name => "ListJobs";

        public override string Description => "Retrieve a JSON array of up to twenty of your most recently created jobs. This array includes in-process, completed, and errored jobs. This will return the jobs themselves, not just a list of the jobs. To retrieve the twenty next most recent jobs, use the nextToken string returned with the array.";
 
        public override string RequestURI => "/2017-08-29/jobs";

        public override string Method => "GET";

        public override string ServiceName => "MediaConvert";

        public override string ServiceID => "MediaConvert";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaConvertConfig config = new AmazonMediaConvertConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaConvertClient client = new AmazonMediaConvertClient(creds, config);
            
            ListJobsResponse resp = new ListJobsResponse();
            do
            {
                try
                {
                    ListJobsRequest req = new ListJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListJobsAsync(req);
                    
                    foreach (var obj in resp.Jobs)
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