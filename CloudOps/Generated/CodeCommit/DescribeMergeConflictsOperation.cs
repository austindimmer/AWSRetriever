using Amazon;
using Amazon.CodeCommit;
using Amazon.CodeCommit.Model;
using Amazon.Runtime;

namespace CloudOps.CodeCommit
{
    public class DescribeMergeConflictsOperation : Operation
    {
        public override string Name => "DescribeMergeConflicts";

        public override string Description => "Returns information about one or more merge conflicts in the attempted merge of two commit specifiers using the squash or three-way merge strategy. If the merge option for the attempted merge is specified as FAST_FORWARD_MERGE, an exception is thrown.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeCommit";

        public override string ServiceID => "CodeCommit";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeCommitConfig config = new AmazonCodeCommitConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeCommitClient client = new AmazonCodeCommitClient(creds, config);
            
            DescribeMergeConflictsResponse resp = new DescribeMergeConflictsResponse();
            do
            {
                DescribeMergeConflictsRequest req = new DescribeMergeConflictsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxMergeHunks = maxItems
                                        
                };

                resp = client.DescribeMergeConflicts(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.MergeHunks)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.DestinationCommitId)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.SourceCommitId)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.BaseCommitId)
                {
                    AddObject(obj);
                }
                
                foreach (var obj in resp.ConflictMetadata)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}