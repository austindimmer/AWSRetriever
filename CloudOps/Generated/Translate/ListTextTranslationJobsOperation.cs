using Amazon;
using Amazon.Translate;
using Amazon.Translate.Model;
using Amazon.Runtime;

namespace CloudOps.Translate
{
    public class ListTextTranslationJobsOperation : Operation
    {
        public override string Name => "ListTextTranslationJobs";

        public override string Description => "Gets a list of the batch translation jobs that you have submitted.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Translate";

        public override string ServiceID => "Translate";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonTranslateConfig config = new AmazonTranslateConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonTranslateClient client = new AmazonTranslateClient(creds, config);
            
            ListTextTranslationJobsResponse resp = new ListTextTranslationJobsResponse();
            do
            {
                try
                {
                    ListTextTranslationJobsRequest req = new ListTextTranslationJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListTextTranslationJobsAsync(req);
                    
                    foreach (var obj in resp.TextTranslationJobPropertiesList)
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