using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListDomainConfigurationsOperation : Operation
    {
        public override string Name => "ListDomainConfigurations";

        public override string Description => "Gets a list of domain configurations for the user. This list is sorted alphabetically by domain configuration name.  The domain configuration feature is in public preview and is subject to change. ";
 
        public override string RequestURI => "/domainConfigurations";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListDomainConfigurationsResponse resp = new ListDomainConfigurationsResponse();
            do
            {
                try
                {
                    ListDomainConfigurationsRequest req = new ListDomainConfigurationsRequest
                    {
                        Marker = resp.NextMarker
                        ,
                        PageSize = maxItems
                                            
                    };

                    resp = await client.ListDomainConfigurationsAsync(req);
                    
                    foreach (var obj in resp.DomainConfigurations)
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