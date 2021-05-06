using Amazon;
using Amazon.IVS;
using Amazon.IVS.Model;
using Amazon.Runtime;

namespace CloudOps.IVS
{
    public class ListRecordingConfigurationsOperation : Operation
    {
        public override string Name => "ListRecordingConfigurations";

        public override string Description => "Gets summary information about all recording configurations in your account, in the AWS region where the API request is processed.";
 
        public override string RequestURI => "/ListRecordingConfigurations";

        public override string Method => "POST";

        public override string ServiceName => "IVS";

        public override string ServiceID => "ivs";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIVSConfig config = new AmazonIVSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIVSClient client = new AmazonIVSClient(creds, config);
            
            ListRecordingConfigurationsResponse resp = new ListRecordingConfigurationsResponse();
            do
            {
                ListRecordingConfigurationsRequest req = new ListRecordingConfigurationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListRecordingConfigurationsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.RecordingConfigurations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}