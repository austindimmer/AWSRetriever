using Amazon;
using Amazon.Chime;
using Amazon.Chime.Model;
using Amazon.Runtime;

namespace CloudOps.Chime
{
    public class ListSipRulesOperation : Operation
    {
        public override string Name => "ListSipRules";

        public override string Description => "Lists the SIP rules under the administrator&#39;s AWS account.";
 
        public override string RequestURI => "/sip-rules";

        public override string Method => "GET";

        public override string ServiceName => "Chime";

        public override string ServiceID => "Chime";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonChimeConfig config = new AmazonChimeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonChimeClient client = new AmazonChimeClient(creds, config);
            
            ListSipRulesResponse resp = new ListSipRulesResponse();
            do
            {
                ListSipRulesRequest req = new ListSipRulesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListSipRules(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SipRules)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}