using Amazon;
using Amazon.Athena;
using Amazon.Athena.Model;
using Amazon.Runtime;

namespace CloudOps.Athena
{
    public class ListWorkGroupsOperation : Operation
    {
        public override string Name => "ListWorkGroups";

        public override string Description => "Lists available workgroups for the account.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "Athena";

        public override string ServiceID => "Athena";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAthenaConfig config = new AmazonAthenaConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonAthenaClient client = new AmazonAthenaClient(creds, config);
            
            ListWorkGroupsResponse resp = new ListWorkGroupsResponse();
            do
            {
                try
                {
                    ListWorkGroupsRequest req = new ListWorkGroupsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListWorkGroupsAsync(req);
                    
                    foreach (var obj in resp.WorkGroups)
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