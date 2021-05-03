using Amazon;
using Amazon.Kendra;
using Amazon.Kendra.Model;
using Amazon.Runtime;

namespace CloudOps.Kendra
{
    public class ListIndicesOperation : Operation
    {
        public override string Name => "ListIndices";

        public override string Description => "Lists the Amazon Kendra indexes that you have created.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Kendra";

        public override string ServiceID => "kendra";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonKendraConfig config = new AmazonKendraConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonKendraClient client = new AmazonKendraClient(creds, config);
            
            ListIndicesResponse resp = new ListIndicesResponse();
            do
            {
                ListIndicesRequest req = new ListIndicesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListIndices(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.IndexConfigurationSummaryItems)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}