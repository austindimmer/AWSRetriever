using Amazon;
using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using Amazon.Runtime;

namespace CloudOps.SimpleEmailV2
{
    public class ListImportJobsOperation : Operation
    {
        public override string Name => "ListImportJobs";

        public override string Description => "Lists all of the import jobs.";
 
        public override string RequestURI => "/v2/email/import-jobs";

        public override string Method => "GET";

        public override string ServiceName => "SimpleEmailV2";

        public override string ServiceID => "SimpleEmailV2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSimpleEmailServiceV2Config config = new AmazonSimpleEmailServiceV2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSimpleEmailServiceV2Client client = new AmazonSimpleEmailServiceV2Client(creds, config);
            
            ListImportJobsResponse resp = new ListImportJobsResponse();
            do
            {
                ListImportJobsRequest req = new ListImportJobsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    PageSize = maxItems
                                        
                };

                resp = client.ListImportJobs(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ImportJobs)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}