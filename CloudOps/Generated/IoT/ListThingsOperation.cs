using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListThingsOperation : Operation
    {
        public override string Name => "ListThings";

        public override string Description => "Lists your things. Use the attributeName and attributeValue parameters to filter your things. For example, calling ListThings with attributeName=Color and attributeValue=Red retrieves all things in the registry that contain an attribute Color with the value Red.   You will not be charged for calling this API if an Access denied error is returned. You will also not be charged if no attributes or pagination token was provided in request and no pagination token and no results were returned. ";
 
        public override string RequestURI => "/things";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListThingsResponse resp = new ListThingsResponse();
            do
            {
                try
                {
                    ListThingsRequest req = new ListThingsRequest
                    {
                         Marker = resp.NextMarker
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListThingsAsync(req);
                    
                    foreach (var obj in resp.Things)
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
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}