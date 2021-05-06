using Amazon;
using Amazon.Macie2;
using Amazon.Macie2.Model;
using Amazon.Runtime;

namespace CloudOps.Macie2
{
    public class ListClassificationJobsOperation : Operation
    {
        public override string Name => "ListClassificationJobs";

        public override string Description => "Retrieves a subset of information about one or more classification jobs.";
 
        public override string RequestURI => "/jobs/list";

        public override string Method => "POST";

        public override string ServiceName => "Macie2";

        public override string ServiceID => "Macie2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacie2Config config = new AmazonMacie2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacie2Client client = new AmazonMacie2Client(creds, config);
            
            ListClassificationJobsResponse resp = new ListClassificationJobsResponse();
            do
            {
                try
                {
                    ListClassificationJobsRequest req = new ListClassificationJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListClassificationJobsAsync(req);
                    
                    foreach (var obj in resp.Items)
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