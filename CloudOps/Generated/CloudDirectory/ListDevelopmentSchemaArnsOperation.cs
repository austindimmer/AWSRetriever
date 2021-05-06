using Amazon;
using Amazon.CloudDirectory;
using Amazon.CloudDirectory.Model;
using Amazon.Runtime;

namespace CloudOps.CloudDirectory
{
    public class ListDevelopmentSchemaArnsOperation : Operation
    {
        public override string Name => "ListDevelopmentSchemaArns";

        public override string Description => "Retrieves each Amazon Resource Name (ARN) of schemas in the development state.";
 
        public override string RequestURI => "/amazonclouddirectory/2017-01-11/schema/development";

        public override string Method => "POST";

        public override string ServiceName => "CloudDirectory";

        public override string ServiceID => "CloudDirectory";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudDirectoryConfig config = new AmazonCloudDirectoryConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCloudDirectoryClient client = new AmazonCloudDirectoryClient(creds, config);
            
            ListDevelopmentSchemaArnsResponse resp = new ListDevelopmentSchemaArnsResponse();
            do
            {
                try
                {
                    ListDevelopmentSchemaArnsRequest req = new ListDevelopmentSchemaArnsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListDevelopmentSchemaArnsAsync(req);
                    
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