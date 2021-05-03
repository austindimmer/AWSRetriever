using Amazon;
using Amazon.CodeBuild;
using Amazon.CodeBuild.Model;
using Amazon.Runtime;

namespace CloudOps.CodeBuild
{
    public class ListReportsForReportGroupOperation : Operation
    {
        public override string Name => "ListReportsForReportGroup";

        public override string Description => " Returns a list of ARNs for the reports that belong to a ReportGroup. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeBuild";

        public override string ServiceID => "CodeBuild";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeBuildConfig config = new AmazonCodeBuildConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeBuildClient client = new AmazonCodeBuildClient(creds, config);
            
            ListReportsForReportGroupResponse resp = new ListReportsForReportGroupResponse();
            do
            {
                ListReportsForReportGroupRequest req = new ListReportsForReportGroupRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.ListReportsForReportGroup(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Reports)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}