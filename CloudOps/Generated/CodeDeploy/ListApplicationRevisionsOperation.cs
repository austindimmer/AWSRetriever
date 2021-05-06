using Amazon;
using Amazon.CodeDeploy;
using Amazon.CodeDeploy.Model;
using Amazon.Runtime;

namespace CloudOps.CodeDeploy
{
    public class ListApplicationRevisionsOperation : Operation
    {
        public override string Name => "ListApplicationRevisions";

        public override string Description => "Lists information about revisions for an application.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeDeploy";

        public override string ServiceID => "CodeDeploy";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeDeployConfig config = new AmazonCodeDeployConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeDeployClient client = new AmazonCodeDeployClient(creds, config);
            
            ListApplicationRevisionsResponse resp = new ListApplicationRevisionsResponse();
            do
            {
                try
                {
                    ListApplicationRevisionsRequest req = new ListApplicationRevisionsRequest
                    {
                        NextToken = resp.NextToken
                                            
                    };

                    resp = await client.ListApplicationRevisionsAsync(req);
                    
                    foreach (var obj in resp.Revisions)
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