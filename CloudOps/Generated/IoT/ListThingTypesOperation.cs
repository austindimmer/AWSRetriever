using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListThingTypesOperation : Operation
    {
        public override string Name => "ListThingTypes";

        public override string Description => "Lists the existing thing types.";
 
        public override string RequestURI => "/thing-types";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListThingTypesResponse resp = new ListThingTypesResponse();
            do
            {
                ListThingTypesRequest req = new ListThingTypesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListThingTypes(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ThingTypes)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}