using Amazon;
using Amazon.IoT;
using Amazon.IoT.Model;
using Amazon.Runtime;

namespace CloudOps.IoT
{
    public class ListRoleAliasesOperation : Operation
    {
        public override string Name => "ListRoleAliases";

        public override string Description => "Lists the role aliases registered in your account.";
 
        public override string RequestURI => "/role-aliases";

        public override string Method => "GET";

        public override string ServiceName => "IoT";

        public override string ServiceID => "IoT";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonIoTConfig config = new AmazonIoTConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonIoTClient client = new AmazonIoTClient(creds, config);
            
            ListRoleAliasesResponse resp = new ListRoleAliasesResponse();
            do
            {
                ListRoleAliasesRequest req = new ListRoleAliasesRequest
                {
                    Marker = resp.NextMarker
                    ,
                    PageSize = maxItems
                                        
                };

                resp = await client.ListRoleAliasesAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.RoleAliases)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextMarker));
        }
    }
}