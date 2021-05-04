using Amazon;
using Amazon.Backup;
using Amazon.Backup.Model;
using Amazon.Runtime;

namespace CloudOps.Backup
{
    public class ListProtectedResourcesOperation : Operation
    {
        public override string Name => "ListProtectedResources";

        public override string Description => "Returns an array of resources successfully backed up by AWS Backup, including the time the resource was saved, an Amazon Resource Name (ARN) of the resource, and a resource type.";
 
        public override string RequestURI => "/resources/";

        public override string Method => "GET";

        public override string ServiceName => "Backup";

        public override string ServiceID => "Backup";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonBackupConfig config = new AmazonBackupConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonBackupClient client = new AmazonBackupClient(creds, config);
            
            ListProtectedResourcesResponse resp = new ListProtectedResourcesResponse();
            do
            {
                ListProtectedResourcesRequest req = new ListProtectedResourcesRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListProtectedResources(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Results)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}