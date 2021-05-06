using Amazon;
using Amazon.CodeGuruProfiler;
using Amazon.CodeGuruProfiler.Model;
using Amazon.Runtime;

namespace CloudOps.CodeGuruProfiler
{
    public class GetFindingsReportAccountSummaryOperation : Operation
    {
        public override string Name => "GetFindingsReportAccountSummary";

        public override string Description => " Returns a list of  FindingsReportSummary  objects that contain analysis results for all profiling groups in your AWS account. ";
 
        public override string RequestURI => "/internal/findingsReports";

        public override string Method => "GET";

        public override string ServiceName => "CodeGuruProfiler";

        public override string ServiceID => "CodeGuruProfiler";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeGuruProfilerConfig config = new AmazonCodeGuruProfilerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeGuruProfilerClient client = new AmazonCodeGuruProfilerClient(creds, config);
            
            GetFindingsReportAccountSummaryResponse resp = new GetFindingsReportAccountSummaryResponse();
            do
            {
                try
                {
                    GetFindingsReportAccountSummaryRequest req = new GetFindingsReportAccountSummaryRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.GetFindingsReportAccountSummaryAsync(req);
                    
                    foreach (var obj in resp.ReportSummaries)
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