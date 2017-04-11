using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HoloPollster
{
    public class Cloud
    {
        public Cloud() { }

        public static async Task performBlobOperation()
        {
            //Get storage account
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=holopollster;AccountKey=WH1IXI99gGbxO/StyP8o/KknvLLPJqJfw3YK1PNplREb2C+NDeesnJFHfmKwCldK35BMNYOyfHaHtwiksY/LVg==");
       
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("polldata");

            await container.CreateIfNotExistsAsync();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("placeholder");

            //create new blob storing serialized data
            await blockBlob.UploadTextAsync("Hello World");
        }
    }
}
