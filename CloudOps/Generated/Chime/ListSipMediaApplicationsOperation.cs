using Amazon;
using Amazon.Chime;
using Amazon.Chime.Model;
using Amazon.Runtime;

namespace CloudOps.Chime
{
    public class ListSipMediaApplicationsOperation : Operation
    {
        public override string Name => "ListSipMediaApplications";

        public override string Description => "Lists the SIP media applications under the administrator&#39;s AWS account.";
 
        public override string RequestURI => "/sip-media-applications";

        public override string Method => "GET";

        public override string ServiceName => "Chime";

        public override string ServiceID => "Chime";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonChimeConfig config = new AmazonChimeConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonChimeClient client = new AmazonChimeClient(creds, config);
            
            ListSipMediaApplicationsResponse resp = new ListSipMediaApplicationsResponse();
            do
            {
                ListSipMediaApplicationsRequest req = new ListSipMediaApplicationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListSipMediaApplications(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.SipMediaApplications)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}