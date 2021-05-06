using Amazon;
using Amazon.CodeCommit;
using Amazon.CodeCommit.Model;
using Amazon.Runtime;

namespace CloudOps.CodeCommit
{
    public class GetMergeConflictsOperation : Operation
    {
        public override string Name => "GetMergeConflicts";

        public override string Description => "Returns information about merge conflicts between the before and after commit IDs for a pull request in a repository.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CodeCommit";

        public override string ServiceID => "CodeCommit";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCodeCommitConfig config = new AmazonCodeCommitConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonCodeCommitClient client = new AmazonCodeCommitClient(creds, config);
            
            GetMergeConflictsResponse resp = new GetMergeConflictsResponse();
            do
            {
                try
                {
                    GetMergeConflictsRequest req = new GetMergeConflictsRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxConflictFiles = maxItems
                                            
                    };

                    resp = await client.GetMergeConflictsAsync(req);
                    
                    foreach (var obj in resp.Mergeable)
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
                    
                    foreach (var obj in resp.ConflictMetadataList)
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