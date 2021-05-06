using Amazon;
using Amazon.Macie;
using Amazon.Macie.Model;
using Amazon.Runtime;

namespace CloudOps.Macie
{
    public class ListS3ResourcesOperation : Operation
    {
        public override string Name => "ListS3Resources";

        public override string Description => "Lists all the S3 resources associated with Amazon Macie Classic. If memberAccountId isn&#39;t specified, the action lists the S3 resources associated with Macie Classic for the current Macie Classic administrator account. If memberAccountId is specified, the action lists the S3 resources associated with Macie Classic for the specified member account. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Macie";

        public override string ServiceID => "Macie";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMacieConfig config = new AmazonMacieConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMacieClient client = new AmazonMacieClient(creds, config);
            
            ListS3ResourcesResponse resp = new ListS3ResourcesResponse();
            do
            {
                ListS3ResourcesRequest req = new ListS3ResourcesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListS3ResourcesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.S3Resources)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}