using Amazon;
using Amazon.TranscribeService;
using Amazon.TranscribeService.Model;
using Amazon.Runtime;

namespace CloudOps.TranscribeService
{
    public class ListLanguageModelsOperation : Operation
    {
        public override string Name => "ListLanguageModels";

        public override string Description => "Provides more information about the custom language models you&#39;ve created. You can use the information in this list to find a specific custom language model. You can then use the operation to get more information about it.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "TranscribeService";

        public override string ServiceID => "Transcribe";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonTranscribeServiceConfig config = new AmazonTranscribeServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonTranscribeServiceClient client = new AmazonTranscribeServiceClient(creds, config);
            
            ListLanguageModelsResponse resp = new ListLanguageModelsResponse();
            do
            {
                try
                {
                    ListLanguageModelsRequest req = new ListLanguageModelsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListLanguageModelsAsync(req);
                    
                    foreach (var obj in resp.Models)
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