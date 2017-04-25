using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Microsoft.WindowsAzure.Storage.Auth;

namespace HoloPollster
{
    public class Cloud
    {
        private static string accountKey;
        private static string accountName;
        private static ObjectSerializer objSerializer;
        public Cloud() {
            accountName = "holopollster";
            accountKey = "DefaultEndpointsProtocol=https;AccountName=holopollster;AccountKey=EdZDW0Pw1v/n7mNCw7jfnMTYBI6zYuhDHrnjdxXTSOctMdc6Iu8wlOLJz+I+1HBAcS/1FkqDR0VJ/2wnFBeukA==;EndpointSuffix=core.windows.net";
            objSerializer = new ObjectSerializer();
        }

        public static async Task UploadToCloudSerialized(PollsWithMetaData pollData)
        {
            //serialize pollData and convert to stream for upload to azure
            MemoryStream stream = objSerializer.SerializeToStream(pollData);
            //Get storage account

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);
            //open connection to azure account

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("pollswithmetadata");

            await container.CreateIfNotExistsAsync();
            
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("poll/TempName"); //get string version of CreationTime, remove "," and "/" from it so that azure does not create virtual directories
                                                                                         //Then, use this modified string as "poll/CreationTimeString" to create a unique blob name 



            //create new blob storing serialized data
            await blockBlob.UploadFromStreamAsync(stream);
        }

        public static async Task RetrieveFromCloud(AllPollsCreated allPolls)
        {
            //connect to azure account
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("pollswithmetadata");

            if (await container.ExistsAsync())
            {
                //gets list of items stored in container
                List<IListBlobItem> results = await ListBlobsAsync(container);
                //iterate over items in container
                foreach (IListBlobItem item in results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {

                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        //deserialize data
                        PollsWithMetaData poll = await objSerializer.Deserialize(blob);
                        //add poll to local list of all polls
                        allPolls.CreatedPolls.Add(poll);
                    }
                }
            }
        }

        private static async Task<List<IListBlobItem>> ListBlobsAsync(CloudBlobContainer container)
        {
            BlobContinuationToken continuationToken = null;
            List<IListBlobItem> results = new List<IListBlobItem>();

            do
            {
                //get blobs in container with virtual directory prefix "poll/"
                BlobResultSegment result = await container.ListBlobsSegmentedAsync("poll/", continuationToken);
                continuationToken = result.ContinuationToken;
                //store blob in list of results
                results.AddRange(result.Results);
            }
            while (continuationToken != null);
            return results;
        }

       

    }
}
