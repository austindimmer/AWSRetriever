using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListProvisioningTemplatesOperation : Operation
    {
        public override string Name => "ListProvisioningTemplates";

        public override string Description => "Lists the fleet provisioning templates in your AWS account.";
 
        public override string RequestURI => "/provisioning-templates";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListProvisioningTemplatesResponse resp = new ListProvisioningTemplatesResponse();
            do
            {
                ListProvisioningTemplatesRequest req = new ListProvisioningTemplatesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListProvisioningTemplates(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Templates)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}