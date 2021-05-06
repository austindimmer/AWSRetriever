using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.Runtime;

namespace CloudOps.Rekognition
{
    public class ListStreamProcessorsOperation : Operation
    {
        public override string Name => "ListStreamProcessors";

        public override string Description => "Gets a list of stream processors that you have created with CreateStreamProcessor. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Rekognition";

        public override string ServiceID => "Rekognition";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRekognitionConfig config = new AmazonRekognitionConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRekognitionClient client = new AmazonRekognitionClient(creds, config);
            
            ListStreamProcessorsResponse resp = new ListStreamProcessorsResponse();
            do
            {
                ListStreamProcessorsRequest req = new ListStreamProcessorsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListStreamProcessorsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.StreamProcessors)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}