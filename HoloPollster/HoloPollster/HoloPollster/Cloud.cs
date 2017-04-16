using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace HoloPollster
{
    public class Cloud
    {
        public Cloud() { }

        public static async Task performBlobOperation(PollsWithMetaData pollData)
        {
            ObjectSerializer objSerializer = new ObjectSerializer();
            MemoryStream stream = objSerializer.StreamData(pollData);
            //Get storage account
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT_NAME;AccountKey=YOUR_ACCOUNT_KEY");

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("pollswithmetadata");

            await container.CreateIfNotExistsAsync();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("poll/" + pollData.PollCreator + "," + pollData.PollName + "," + pollData.CreationTime);



            //create new blob storing serialized data
            await blockBlob.UploadFromStreamAsync(stream);
        }
    }
}
