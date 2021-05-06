using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.EC2
{
    public class DescribeFastSnapshotRestoresOperation : Operation
    {
        public override string Name => "DescribeFastSnapshotRestores";

        public override string Description => "Describes the state of fast snapshot restores for your snapshots.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EC2";

        public override string ServiceID => "EC2";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEC2Config config = new AmazonEC2Config();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEC2Client client = new AmazonEC2Client(creds, config);
            
            DescribeFastSnapshotRestoresResponse resp = new DescribeFastSnapshotRestoresResponse();
            do
            {
                try
                {
                    DescribeFastSnapshotRestoresRequest req = new DescribeFastSnapshotRestoresRequest
                    {
                        NextToken = resp.NextToken
                        ,
                        MaxResults = maxItems
                                            
                    };

                    resp = await client.DescribeFastSnapshotRestoresAsync(req);
                    
                    foreach (var obj in resp.FastSnapshotRestores)
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