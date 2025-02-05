using Amazon;
using Amazon.CodeCommit;
using Amazon.CodeCommit.Model;
using Amazon.Runtime;

namespace CloudOps.CodeCommit
{
    public class GetCommentsForPullRequestOperation : Operation
    {
        public override string Name => "GetCommentsForPullRequest";

        public override string Description => "Returns comments made on a pull request.  Reaction counts might include numbers from user identities who were deleted after the reaction was made. For a count of reactions from active identities, use GetCommentReactions. ";
 
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
            
            GetCommentsForPullRequestResponse resp = new GetCommentsForPullRequestResponse();
            do
            {
                GetCommentsForPullRequestRequest req = new GetCommentsForPullRequestRequest
                {
                    NextToken = resp.NextToken
                    ,
                    MaxResults = maxItems
                                        
                };

                resp = client.GetCommentsForPullRequest(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.CommentsForPullRequestData)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}