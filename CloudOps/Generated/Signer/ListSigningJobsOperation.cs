using Amazon;
using Amazon.Signer;
using Amazon.Signer.Model;
using Amazon.Runtime;

namespace CloudOps.Signer
{
    public class ListSigningJobsOperation : Operation
    {
        public override string Name => "ListSigningJobs";

        public override string Description => "Lists all your signing jobs. You can use the maxResults parameter to limit the number of signing jobs that are returned in the response. If additional jobs remain to be listed, code signing returns a nextToken value. Use this value in subsequent calls to ListSigningJobs to fetch the remaining values. You can continue calling ListSigningJobs with your maxResults parameter and with new values that code signing returns in the nextToken parameter until all of your signing jobs have been returned. ";
 
        public override string RequestURI => "/signing-jobs";

        public override string Method => "GET";

        public override string ServiceName => "Signer";

        public override string ServiceID => "signer";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSignerConfig config = new AmazonSignerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSignerClient client = new AmazonSignerClient(creds, config);
            
            ListSigningJobsResponse resp = new ListSigningJobsResponse();
            do
            {
                try
                {
                    ListSigningJobsRequest req = new ListSigningJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListSigningJobsAsync(req);
                    
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