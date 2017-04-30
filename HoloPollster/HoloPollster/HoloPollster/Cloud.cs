﻿using System;
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
        private static ObjectSerializer objSerializer;
        public Cloud() {
            accountKey = "DefaultEndpointsProtocol=https;AccountName=holopollster;AccountKey=EdZDW0Pw1v/n7mNCw7jfnMTYBI6zYuhDHrnjdxXTSOctMdc6Iu8wlOLJz+I+1HBAcS/1FkqDR0VJ/2wnFBeukA==;EndpointSuffix=core.windows.net";
            objSerializer = new ObjectSerializer();
        }

        public static async Task UploadUserToCloudSerialized(LoginData loginData)
        {
            MemoryStream stream = objSerializer.SerializeToStream(loginData);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("logindata");

            await container.CreateIfNotExistsAsync();

            string blobName = loginData.username;

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("user/" + blobName);

            await blockBlob.UploadFromStreamAsync(stream);
        }

        public static async Task UploadPollToCloudSerialized(PollsWithMetaData pollData)
        {
            //serialize pollData and convert to stream for upload to azure
            MemoryStream stream = objSerializer.SerializeToStream(pollData);
            //Get storage account

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);
            //open connection to azure account

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("pollswithmetadata");

            await container.CreateIfNotExistsAsync();

            string blobName = pollData.CreationTime.ToString("yyyyMMddHHmmss");
            
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("poll/" + blobName); 



            //create new blob storing serialized data
            await blockBlob.UploadFromStreamAsync(stream);
        }

        public static async Task RetrieveUserFromCloud(LoginData user)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("logindata");

            if(await container.ExistsAsync())
            {

                List<IListBlobItem> results = await ListUserBlobsAsync(container);

                foreach (IListBlobItem item in results)
                {

                    if (item.GetType() == typeof(CloudBlockBlob))
                    {

                        CloudBlockBlob blob = (CloudBlockBlob)item;

                        //if(blob.Name == )
                    }
                }
            }
        }
        

        public static async Task RetrievePollFromCloud(AllPollsCreated allPolls)
        {
            //connect to azure account
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("pollswithmetadata");

            if (await container.ExistsAsync())
            {
                //gets list of items stored in container
                List<IListBlobItem> results = await ListPollBlobsAsync(container);
                //iterate over items in container
                foreach (IListBlobItem item in results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {

                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        //deserialize data
                        PollsWithMetaData poll = await objSerializer.Deserialize(blob);
                        //add poll to local list of all polls
                        if (!allPolls.CreatedPolls.Contains(poll))
                        {
                            allPolls.CreatedPolls.Add(poll);
                        }
                        
                    }
                }
            }
        }

        private static async Task<List<IListBlobItem>> ListPollBlobsAsync(CloudBlobContainer container)
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

        private static async Task<List<IListBlobItem>> ListUserBlobsAsync(CloudBlobContainer container)
        {
            BlobContinuationToken continuationToken = null;
            List<IListBlobItem> results = new List<IListBlobItem>();

            do
            {
                //get blobs in container with virtual directory prefix "poll/"
                BlobResultSegment result = await container.ListBlobsSegmentedAsync("user/", continuationToken);
                continuationToken = result.ContinuationToken;
                //store blob in list of results
                results.AddRange(result.Results);
            }
            while (continuationToken != null);
            return results;
        }



    }
}
