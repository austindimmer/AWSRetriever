using Amazon;
using Amazon.GlueDataBrew;
using Amazon.GlueDataBrew.Model;
using Amazon.Runtime;

namespace CloudOps.GlueDataBrew
{
    public class ListProjectsOperation : Operation
    {
        public override string Name => "ListProjects";

        public override string Description => "Lists all of the DataBrew projects that are defined.";
 
        public override string RequestURI => "/projects";

        public override string Method => "GET";

        public override string ServiceName => "GlueDataBrew";

        public override string ServiceID => "DataBrew";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonGlueDataBrewConfig config = new AmazonGlueDataBrewConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonGlueDataBrewClient client = new AmazonGlueDataBrewClient(creds, config);
            
            ListProjectsResponse resp = new ListProjectsResponse();
            do
            {
                try
                {
                    ListProjectsRequest req = new ListProjectsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListProjectsAsync(req);
                    
                    foreach (var obj in resp.Projects)
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