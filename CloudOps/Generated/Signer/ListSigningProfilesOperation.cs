using Amazon;
using Amazon.Signer;
using Amazon.Signer.Model;
using Amazon.Runtime;

namespace CloudOps.Signer
{
    public class ListSigningProfilesOperation : Operation
    {
        public override string Name => "ListSigningProfiles";

        public override string Description => "Lists all available signing profiles in your AWS account. Returns only profiles with an ACTIVE status unless the includeCanceled request field is set to true. If additional jobs remain to be listed, code signing returns a nextToken value. Use this value in subsequent calls to ListSigningJobs to fetch the remaining values. You can continue calling ListSigningJobs with your maxResults parameter and with new values that code signing returns in the nextToken parameter until all of your signing jobs have been returned.";
 
        public override string RequestURI => "/signing-profiles";

        public override string Method => "GET";

        public override string ServiceName => "Signer";

        public override string ServiceID => "signer";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSignerConfig config = new AmazonSignerConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSignerClient client = new AmazonSignerClient(creds, config);
            
            ListSigningProfilesResponse resp = new ListSigningProfilesResponse();
            do
            {
                try
                {
                    ListSigningProfilesRequest req = new ListSigningProfilesRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.ListSigningProfilesAsync(req);
                    
                    foreach (var obj in resp.Profiles)
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