using Amazon;
using Amazon.GuardDuty;
using Amazon.GuardDuty.Model;
using Amazon.Runtime;

namespace CloudOps.GuardDuty
{
    public class ListDetectorsOperation : Operation
    {
        public override string Name => "ListDetectors";

        public override string Description => "Lists detectorIds of all the existing Amazon GuardDuty detector resources.";
 
        public override string RequestURI => "/detector";

        public override string Method => "GET";

        public override string ServiceName => "GuardDuty";

        public override string ServiceID => "GuardDuty";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGuardDutyConfig config = new AmazonGuardDutyConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGuardDutyClient client = new AmazonGuardDutyClient(creds, config);
            
            ListDetectorsResponse resp = new ListDetectorsResponse();
            do
            {
                ListDetectorsRequest req = new ListDetectorsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListDetectorsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DetectorIds)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}