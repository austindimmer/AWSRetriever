using Amazon;
using Amazon.TranscribeService;
using Amazon.TranscribeService.Model;
using Amazon.Runtime;

namespace CloudOps.TranscribeService
{
    public class ListMedicalTranscriptionJobsOperation : Operation
    {
        public override string Name => "ListMedicalTranscriptionJobs";

        public override string Description => "Lists medical transcription jobs with a specified status or substring that matches their names.";
 
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
            
            ListMedicalTranscriptionJobsResponse resp = new ListMedicalTranscriptionJobsResponse();
            do
            {
                try
                {
                    ListMedicalTranscriptionJobsRequest req = new ListMedicalTranscriptionJobsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListMedicalTranscriptionJobsAsync(req);
                    
                    foreach (var obj in resp.MedicalTranscriptionJobSummaries)
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