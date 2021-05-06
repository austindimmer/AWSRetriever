using Amazon;
using Amazon.RAM;
using Amazon.RAM.Model;
using Amazon.Runtime;

namespace CloudOps.RAM
{
    public class GetResourceShareInvitationsOperation : Operation
    {
        public override string Name => "GetResourceShareInvitations";

        public override string Description => "Gets the invitations for resource sharing that you&#39;ve received.";
 
        public override string RequestURI => "/getresourceshareinvitations";

        public override string Method => "POST";

        public override string ServiceName => "RAM";

        public override string ServiceID => "RAM";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonRAMConfig config = new AmazonRAMConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonRAMClient client = new AmazonRAMClient(creds, config);
            
            GetResourceShareInvitationsResponse resp = new GetResourceShareInvitationsResponse();
            do
            {
                try
                {
                    GetResourceShareInvitationsRequest req = new GetResourceShareInvitationsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.GetResourceShareInvitationsAsync(req);
                    
                    foreach (var obj in resp.ResourceShareInvitations)
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