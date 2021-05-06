using Amazon;
using Amazon.AlexaForBusiness;
using Amazon.AlexaForBusiness.Model;
using Amazon.Runtime;

namespace CloudOps.AlexaForBusiness
{
    public class SearchDevicesOperation : Operation
    {
        public override string Name => "SearchDevices";

        public override string Description => "Searches devices and lists the ones that meet a set of filter criteria.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "AlexaForBusiness";

        public override string ServiceID => "Alexa For Business";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAlexaForBusinessConfig config = new AmazonAlexaForBusinessConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAlexaForBusinessClient client = new AmazonAlexaForBusinessClient(creds, config);
            
            SearchDevicesResponse resp = new SearchDevicesResponse();
            do
            {
                try
                {
                    SearchDevicesRequest req = new SearchDevicesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.SearchDevicesAsync(req);
                    
                    foreach (var obj in resp.Devices)
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