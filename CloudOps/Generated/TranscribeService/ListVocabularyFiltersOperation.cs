using Amazon;
using Amazon.TranscribeService;
using Amazon.TranscribeService.Model;
using Amazon.Runtime;

namespace CloudOps.TranscribeService
{
    public class ListVocabularyFiltersOperation : Operation
    {
        public override string Name => "ListVocabularyFilters";

        public override string Description => "Gets information about vocabulary filters.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "TranscribeService";

        public override string ServiceID => "Transcribe";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonTranscribeServiceConfig config = new AmazonTranscribeServiceConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonTranscribeServiceClient client = new AmazonTranscribeServiceClient(creds, config);
            
            ListVocabularyFiltersResponse resp = new ListVocabularyFiltersResponse();
            do
            {
                ListVocabularyFiltersRequest req = new ListVocabularyFiltersRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListVocabularyFilters(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.VocabularyFilters)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}