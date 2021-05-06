using Amazon;
using Amazon.EMR;
using Amazon.EMR.Model;
using Amazon.Runtime;

namespace CloudOps.EMR
{
    public class ListStepsOperation : Operation
    {
        public override string Name => "ListSteps";

        public override string Description => "Provides a list of steps for the cluster in reverse order unless you specify stepIds with the request of filter by StepStates. You can specify a maximum of 10 stepIDs.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EMR";

        public override string ServiceID => "EMR";

        public override async void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEMRConfig config = new AmazonEMRConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonEMRClient client = new AmazonEMRClient(creds, config);
            
            ListStepsResponse resp = new ListStepsResponse();
            do
            {
                try
                {
                    ListStepsRequest req = new ListStepsRequest
                    {
                        Marker = resp.Marker
                                            
                    };

                    resp = await client.ListStepsAsync(req);
                    
                    foreach (var obj in resp.Steps)
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
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}