using Amazon;
using Amazon.SageMaker;
using Amazon.SageMaker.Model;
using Amazon.Runtime;

namespace CloudOps.SageMaker
{
    public class ListDeviceFleetsOperation : Operation
    {
        public override string Name => "ListDeviceFleets";

        public override string Description => "Returns a list of devices in the fleet.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SageMaker";

        public override string ServiceID => "SageMaker";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSageMakerConfig config = new AmazonSageMakerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSageMakerClient client = new AmazonSageMakerClient(creds, config);
            
            ListDeviceFleetsResponse resp = new ListDeviceFleetsResponse();
            do
            {
                ListDeviceFleetsRequest req = new ListDeviceFleetsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListDeviceFleets(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.DeviceFleetSummaries)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}