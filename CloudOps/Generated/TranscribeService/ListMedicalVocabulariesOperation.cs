using Amazon;
using Amazon.TranscribeService;
using Amazon.TranscribeService.Model;
using Amazon.Runtime;

namespace CloudOps.TranscribeService
{
    public class ListMedicalVocabulariesOperation : Operation
    {
        public override string Name => "ListMedicalVocabularies";

        public override string Description => "Returns a list of vocabularies that match the specified criteria. If you don&#39;t enter a value in any of the request parameters, returns the entire list of vocabularies.";
 
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
            
            ListMedicalVocabulariesResponse resp = new ListMedicalVocabulariesResponse();
            do
            {
                try
                {
                    ListMedicalVocabulariesRequest req = new ListMedicalVocabulariesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListMedicalVocabulariesAsync(req);
                    
                    foreach (var obj in resp.Vocabularies)
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