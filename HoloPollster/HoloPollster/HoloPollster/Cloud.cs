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
    /// <summary>
    /// Class Cloud.
    /// </summary>
    public class Cloud
    {
        /// <summary>
        /// The account key
        /// </summary>
        private static string accountKey;
        /// <summary>
        /// The account name
        /// </summary>
        private static string accountName;
        /// <summary>
        /// The object serializer
        /// </summary>
        private static ObjectSerializer objSerializer;
        /// <summary>
        /// Initializes a new instance of the <see cref="Cloud"/> class.
        /// </summary>
        public Cloud()
        {
            accountName = "holopollster";
            accountKey = "DefaultEndpointsProtocol=https;AccountName=holopollster;AccountKey=EdZDW0Pw1v/n7mNCw7jfnMTYBI6zYuhDHrnjdxXTSOctMdc6Iu8wlOLJz+I+1HBAcS/1FkqDR0VJ/2wnFBeukA==;EndpointSuffix=core.windows.net";
            objSerializer = new ObjectSerializer();
        }

        /// <summary>
        /// Uploads the poll to cloud serialized.
        /// </summary>
        /// <param name="pollData">The poll data.</param>
        /// <returns>Task.</returns>
        public static async Task UploadPollToCloudSerialized(PollsWithMetaData pollData)
        {
            ///serialize pollData and convert to stream for upload to azure
            MemoryStream stream = objSerializer.SerializeToStream(pollData);
            ///Get storage account

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);
            ///open connection to azure account

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("pollswithmetadata");

            await container.CreateIfNotExistsAsync();

            string blobName = pollData.CreationTime.ToString("yyyyMMddHHmmss");

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("poll/" + blobName); ///get string version of CreationTime, remove "," and "/" from it so that azure does not create virtual directories
                                                                                            ///Then, use this modified string as "poll/CreationTimeString" to create a unique blob name 

            ///create new blob storing serialized data
            await blockBlob.UploadFromStreamAsync(stream);
        }

        /// <summary>
        /// Retrieves the poll from cloud.
        /// </summary>
        /// <param name="allPolls">All polls.</param>
        /// <returns>Task.</returns>
        public static async Task RetrievePollFromCloud(AllPollsCreated allPolls)
        {
            ///connect to azure account
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("pollswithmetadata");

            if (await container.ExistsAsync())
            {
                ///gets list of items stored in container
                List<IListBlobItem> results = await ListBlobsAsync(container);
                ///iterate over items in container
                foreach (IListBlobItem item in results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {

                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        ///deserialize data
                        PollsWithMetaData poll = await objSerializer.Deserialize(blob);
                        ///add poll to local list of all polls
                        if (!allPolls.CreatedPolls.Contains(poll))
                        {
                            allPolls.CreatedPolls.Add(poll);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// list blobs as an asynchronous operation.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>Task&lt;List&lt;IListBlobItem&gt;&gt;.</returns>
        private static async Task<List<IListBlobItem>> ListBlobsAsync(CloudBlobContainer container)
        {
            BlobContinuationToken continuationToken = null;
            List<IListBlobItem> results = new List<IListBlobItem>();

            do
            {
                ///get blobs in container with virtual directory prefix "poll/"
                BlobResultSegment result = await container.ListBlobsSegmentedAsync("poll/", continuationToken);
                continuationToken = result.ContinuationToken;
                ///store blob in list of results
                results.AddRange(result.Results);
            }
            while (continuationToken != null);
            return results;
        }

        /// <summary>
        /// Usernames the upload to cloud serialized.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>Task.</returns>
        public static async Task UsernameUploadToCloudSerialized(LoginData userData)
        {
            ///serialize pollData and convert to stream for upload to azure
            MemoryStream stream = objSerializer.UsernameSerializeToStream(userData);
            //Get storage account

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);
            ///open connection to azure account

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("pollswithmetadata");

            await container.CreateIfNotExistsAsync();

            string blobName = userData.username;


            CloudBlockBlob blockBlob = container.GetBlockBlobReference("username/" + blobName);
            await blockBlob.UploadFromStreamAsync(stream);
        }

        /// <summary>
        /// Usernames the retrieve from cloud.
        /// </summary>
        /// <param name="loginInfo">The login information.</param>
        /// <param name="infoFound">The information found.</param>
        /// <returns>Task&lt;LoginData&gt;.</returns>
        public static async Task<LoginData> UsernameRetrieveFromCloud(string loginInfo, LoginData infoFound)
        {

            ///connect to azure account
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("pollswithmetadata");

            if (await container.ExistsAsync())
            {
                ///gets list of items stored in container
                List<IListBlobItem> results = await UsernameListBlobsAsync(container);
                ///iterate over items in container
                foreach (IListBlobItem item in results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {

                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        ///deserialize data
                        LoginData user = await objSerializer.UsernameDeserialize(blob);
                        ///add poll to local list of all polls
                        if (user.username == loginInfo)
                        {
                            infoFound.username = user.username;
                            infoFound.password = user.password;
                            infoFound.pollsCreated = user.pollsCreated;
                            infoFound.pollsTaken = user.pollsTaken;
                            return user;
                        }

                    }
                }

            }
            return null;
        }
        /// <summary>
        /// username list blobs as an asynchronous operation.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>Task&lt;List&lt;IListBlobItem&gt;&gt;.</returns>
        private static async Task<List<IListBlobItem>> UsernameListBlobsAsync(CloudBlobContainer container)
        {
            BlobContinuationToken continuationToken = null;
            List<IListBlobItem> results = new List<IListBlobItem>();

            do
            {
                ///get blobs in container with virtual directory prefix "poll/"
                BlobResultSegment result = await container.ListBlobsSegmentedAsync("username/", continuationToken);
                continuationToken = result.ContinuationToken;
                ///store blob in list of results
                results.AddRange(result.Results);
            }
            while (continuationToken != null);
            return results;
        }
    }
}
