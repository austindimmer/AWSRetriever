using Amazon;
using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using Amazon.Runtime;

namespace CloudOps.Comprehend
{
    public class ListDocumentClassifiersOperation : Operation
    {
        public override string Name => "ListDocumentClassifiers";

        public override string Description => "Gets a list of the document classifiers that you have created.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Comprehend";

        public override string ServiceID => "Comprehend";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonComprehendConfig config = new AmazonComprehendConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonComprehendClient client = new AmazonComprehendClient(creds, config);
            
            ListDocumentClassifiersResponse resp = new ListDocumentClassifiersResponse();
            do
            {
                ListDocumentClassifiersRequest req = new ListDocumentClassifiersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListDocumentClassifiers(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DocumentClassifierPropertiesList)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}