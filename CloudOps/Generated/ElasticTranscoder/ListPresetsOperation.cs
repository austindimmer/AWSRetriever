using Amazon;
using Amazon.ElasticTranscoder;
using Amazon.ElasticTranscoder.Model;
using Amazon.Runtime;

namespace CloudOps.ElasticTranscoder
{
    public class ListPresetsOperation : Operation
    {
        public override string Name => "ListPresets";

        public override string Description => "The ListPresets operation gets a list of the default presets included with Elastic Transcoder and the presets that you&#39;ve added in an AWS region.";
 
        public override string RequestURI => "/2012-09-25/presets";

        public override string Method => "GET";

        public override string ServiceName => "ElasticTranscoder";

        public override string ServiceID => "Elastic Transcoder";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonElasticTranscoderConfig config = new AmazonElasticTranscoderConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonElasticTranscoderClient client = new AmazonElasticTranscoderClient(creds, config);
            
            ListPresetsResponse resp = new ListPresetsResponse();
            do
            {
                ListPresetsRequest req = new ListPresetsRequest
                {
                    PageToken = resp.NextPageToken
                                        
                };

                resp = await client.ListPresetsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Presets)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextPageToken));
        }
    }
}