using Amazon;
using Amazon.CodeBuild;
using Amazon.CodeBuild.Model;
using Amazon.Runtime;

namespace CloudOps.CodeBuild
{
    public class ListReportsOperation : Operation
    {
        public override string Name => "ListReports";

        public override string Description => " Returns a list of ARNs for the reports in the current AWS account. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeBuild";

        public override string ServiceID => "CodeBuild";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeBuildConfig config = new AmazonCodeBuildConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeBuildClient client = new AmazonCodeBuildClient(creds, config);
            
            ListReportsResponse resp = new ListReportsResponse();
            do
            {
                try
                {
                    ListReportsRequest req = new ListReportsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListReportsAsync(req);
                    
                    foreach (var obj in resp.Reports)
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