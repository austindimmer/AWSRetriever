using Amazon;
using Amazon.MediaLive;
using Amazon.MediaLive.Model;
using Amazon.Runtime;

namespace CloudOps.MediaLive
{
    public class ListInputsOperation : Operation
    {
        public override string Name => "ListInputs";

        public override string Description => "Produces list of inputs that have been created";
 
        public override string RequestURI => "/prod/inputs";

        public override string Method => "GET";

        public override string ServiceName => "MediaLive";

        public override string ServiceID => "MediaLive";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonMediaLiveConfig config = new AmazonMediaLiveConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonMediaLiveClient client = new AmazonMediaLiveClient(creds, config);
            
            ListInputsResponse resp = new ListInputsResponse();
            do
            {
                try
                {
                    ListInputsRequest req = new ListInputsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListInputsAsync(req);
                    
                    foreach (var obj in resp.Inputs)
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