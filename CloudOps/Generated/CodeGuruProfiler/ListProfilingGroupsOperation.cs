using Amazon;
using Amazon.CodeGuruProfiler;
using Amazon.CodeGuruProfiler.Model;
using Amazon.Runtime;

namespace CloudOps.CodeGuruProfiler
{
    public class ListProfilingGroupsOperation : Operation
    {
        public override string Name => "ListProfilingGroups";

        public override string Description => " Returns a list of profiling groups. The profiling groups are returned as  ProfilingGroupDescription  objects. ";
 
        public override string RequestURI => "/profilingGroups";

        public override string Method => "GET";

        public override string ServiceName => "CodeGuruProfiler";

        public override string ServiceID => "CodeGuruProfiler";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeGuruProfilerConfig config = new AmazonCodeGuruProfilerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeGuruProfilerClient client = new AmazonCodeGuruProfilerClient(creds, config);
            
            ListProfilingGroupsResponse resp = new ListProfilingGroupsResponse();
            do
            {
                ListProfilingGroupsRequest req = new ListProfilingGroupsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = await client.ListProfilingGroupsAsync(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.ProfilingGroupNames)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.ProfilingGroups)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}