using Amazon;
using Amazon.CloudDirectory;
using Amazon.CloudDirectory.Model;
using Amazon.Runtime;

namespace CloudOps.CloudDirectory
{
    public class ListPublishedSchemaArnsOperation : Operation
    {
        public override string Name => "ListPublishedSchemaArns";

        public override string Description => "Lists the major version families of each published schema. If a major version ARN is provided as SchemaArn, the minor version revisions in that family are listed instead.";
 
        public override string RequestURI => "/amazonclouddirectory/2017-01-11/schema/published";

        public override string Method => "POST";

        public override string ServiceName => "CloudDirectory";

        public override string ServiceID => "CloudDirectory";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudDirectoryConfig config = new AmazonCloudDirectoryConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudDirectoryClient client = new AmazonCloudDirectoryClient(creds, config);
            
            ListPublishedSchemaArnsResponse resp = new ListPublishedSchemaArnsResponse();
            do
            {
                try
                {
                    ListPublishedSchemaArnsRequest req = new ListPublishedSchemaArnsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListPublishedSchemaArnsAsync(req);
                    
                    foreach (var obj in resp.SchemaArns)
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