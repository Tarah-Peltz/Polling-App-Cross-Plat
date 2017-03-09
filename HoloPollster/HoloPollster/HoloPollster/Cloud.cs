using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Windows.Storage;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace HoloPollster
{
    class Cloud
    {

        public Cloud()
        {

        }




        public void test()
        {
            Debug.WriteLine("THIS SHIT WORKS");

        }
        public async Task<Boolean> uploadFile(StorageFile file, String blobName, String foldName)//String fileName, String saveAs)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=evokeintern;AccountKey=YLCmXUh5O0Dngo0q6eS8c1qiw6Nh93hB2pZzJoHuWxiKvleriHJyguelic0WvXCuqtuVPScOtNSaDmZVgfc+bA==");

            //create the blob client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            //CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");
            CloudBlobContainer container = blobClient.GetContainerReference(foldName);

            // Create the container if it doesn't already exist.
            await container.CreateIfNotExistsAsync();

            //set public
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName); //THIS WILL OVERWRITE YO SHIT 

            // Create or overwrite the "myblob" blob with contents from a local file.

            //var file = await KnownFolders.CameraRoll.GetFileAsync("SimplePhoto (8).jpg");
            await blockBlob.UploadFromFileAsync(file);
            /*
            using (var fileStream = System.IO.File.OpenRead(@"asdf\dsfg"))
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }
            */
            Debug.WriteLine("UPLOADED HOMIE");
            return true;
        }

        public async Task<List<Uri>> downloadContainerUri(String containerName)//CloudBlobContainer container)
        {

            //replace w/ parameters
            ////********************************************
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=evokeintern;AccountKey=YLCmXUh5O0Dngo0q6eS8c1qiw6Nh93hB2pZzJoHuWxiKvleriHJyguelic0WvXCuqtuVPScOtNSaDmZVgfc+bA==");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //CloudBlobContainer tainer = blobClient.GetContainerReference("mycontainer");
            CloudBlobContainer tainer = blobClient.GetContainerReference(containerName);

            // Create the container if it doesn't already exist.
            await tainer.CreateIfNotExistsAsync();

            ////********************************************

            var segment = await tainer.ListBlobsSegmentedAsync(null);

            List<Uri> blobUri = new List<Uri>();
            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in segment.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobUri.Add(blob.Uri);
                    //bug.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);

                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;
                    blobUri.Add(pageBlob.Uri);

                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;
                    blobUri.Add(directory.Uri);
                }
            }

            return blobUri;
        }

        public static async Task<List<Uri>> downloadFolderUri(String folderName, String containerName)//CloudBlobContainer container)
        {

            //replace w/ parameters
            ////********************************************
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=evokeintern;AccountKey=YLCmXUh5O0Dngo0q6eS8c1qiw6Nh93hB2pZzJoHuWxiKvleriHJyguelic0WvXCuqtuVPScOtNSaDmZVgfc+bA==");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //CloudBlobContainer tainer = blobClient.GetContainerReference("mycontainer");
            CloudBlobContainer tainer = blobClient.GetContainerReference(containerName);

            //1. grab a folder from the container
            CloudBlobDirectory folder = tainer.GetDirectoryReference(folderName);

            // Create the container if it doesn't already exist.
            //  await tainer.CreateIfNotExistsAsync();
            //   bool exists =await folder.
            // if (exists == false)
            //{
            //   Debug.WriteLine("container doesn't exist");

            // }


            ////********************************************

            var segment = await folder.ListBlobsSegmentedAsync(null);

            List<Uri> blobUri = new List<Uri>();
            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in segment.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobUri.Add(blob.Uri);
                    Debug.WriteLine("Block blob: {0}", blob.Uri);

                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;
                    blobUri.Add(pageBlob.Uri);
                    Debug.WriteLine("Page blob: {0}", pageBlob.Uri);


                }
                /*     else if (item.GetType() == typeof(CloudBlobDirectory))
                     {
                         CloudBlobDirectory directory = (CloudBlobDirectory)item;
                         blobUri.Add(directory.Uri);
                         Debug.WriteLine("Block blob: {0}", directory.Uri);

                     }*/
            }

            return blobUri;
        }

        public async Task<CloudBlob> getPeoplePermissions(String containerName, String folderName)
        {
            // await setUpPeopleAccess(folderName);

            //replace w/ parameters
            ////********************************************
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=evokeintern;AccountKey=YLCmXUh5O0Dngo0q6eS8c1qiw6Nh93hB2pZzJoHuWxiKvleriHJyguelic0WvXCuqtuVPScOtNSaDmZVgfc+bA==");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //CloudBlobContainer tainer = blobClient.GetContainerReference("mycontainer");
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            BlobResultSegment segment;

            CloudBlobDirectory folder = null;


            folder = container.GetDirectoryReference(folderName);
            segment = await folder.ListBlobsSegmentedAsync(null);






            ////********************************************


            List<Uri> blobUri = new List<Uri>();
            //List<CloudBlob> finalBlobs = new List<CloudBlob>();
            CloudBlob peopleAccess = null;
            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in segment.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {


                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    // blobUri.Add(blob.Uri);

                    if (blob.Name.EndsWith("folderAccessPermissions"))
                    {
                        peopleAccess = new CloudBlob(blob.Uri);
                    }

                    //bug.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
                    CloudBlob b = new CloudBlob(blob.Uri);
                    //  finalBlobs.Add(new CloudBlob(blob.Uri));
                    if (b.Name.EndsWith("folderAccessPermissions"))
                    {
                        peopleAccess = new CloudBlob(blob.Uri);
                    }
                }
            }

            if (peopleAccess == null)
            {

                await setUpPeopleAccess(folderName);
                StorageFolder folderTemp = KnownFolders.PicturesLibrary;
                var file = await folderTemp.CreateFileAsync("folderAccessPermissions", CreationCollisionOption.ReplaceExisting);
                await uploadFile(file, folderName + "/folderAccessPermissions", MainPage.personNamePub);

                segment = await folder.ListBlobsSegmentedAsync(null);
                Debug.WriteLine(segment.ToString());

                foreach (IListBlobItem item in segment.Results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {


                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        // blobUri.Add(blob.Uri);

                        Debug.WriteLine(blob.Name);
                        if (blob.Name.EndsWith("folderAccessPermissions"))
                        {
                            peopleAccess = new CloudBlob(blob.Uri);
                        }

                        CloudBlob b = new CloudBlob(blob.Uri);
                        if (b.Name.EndsWith("folderAccessPermissions"))
                        {
                            peopleAccess = new CloudBlob(blob.Uri);
                        }
                        Debug.WriteLine(blob.Name);

                    }
                }

            }

            Debug.WriteLine(peopleAccess.Uri);


            return peopleAccess;


        }

        public async Task<CloudBlob> getPermissions(String containerName)
        {
            //  await setUpAlbumAccess();

            //replace w/ parameters
            ////********************************************
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=evokeintern;AccountKey=YLCmXUh5O0Dngo0q6eS8c1qiw6Nh93hB2pZzJoHuWxiKvleriHJyguelic0WvXCuqtuVPScOtNSaDmZVgfc+bA==");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //CloudBlobContainer tainer = blobClient.GetContainerReference("mycontainer");
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();

            BlobResultSegment segment;

            //  CloudBlobDirectory folder = null;


            segment = await container.ListBlobsSegmentedAsync(null);




            ////********************************************


            List<Uri> blobUri = new List<Uri>();
            //List<CloudBlob> finalBlobs = new List<CloudBlob>();
            CloudBlob peopleAccess = null;
            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in segment.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {


                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    // blobUri.Add(blob.Uri);

                    if (blob.Name == "folderAccessPermissions")
                    {
                        peopleAccess = new CloudBlob(blob.Uri);
                    }

                    //bug.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
                    CloudBlob b = new CloudBlob(blob.Uri);
                    //  finalBlobs.Add(new CloudBlob(blob.Uri));
                    if (b.Name == "folderAccessPermissions")
                    {
                        peopleAccess = new CloudBlob(blob.Uri);
                    }
                }
            }

            if (peopleAccess == null)
            {

                await setUpAlbumAccess(containerName);
                //    StorageFolder folderTemp = KnownFolders.PicturesLibrary;
                // var file = await folderTemp.CreateFileAsync("folderAccessPermissions", CreationCollisionOption.ReplaceExisting);
                //await uploadFile(file, "folderAccessPermissions", MainPage.personNamePub);
                await container.CreateIfNotExistsAsync();
                //       await uploadFile(file, "folderAccessPermissions", containerName);
                segment = await container.ListBlobsSegmentedAsync(null);
                Debug.WriteLine(segment.ToString());

                foreach (IListBlobItem item in segment.Results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {


                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        // blobUri.Add(blob.Uri);

                        Debug.WriteLine(blob.Name);
                        if (blob.Name == "folderAccessPermissions")
                        {
                            peopleAccess = new CloudBlob(blob.Uri);
                        }

                        CloudBlob b = new CloudBlob(blob.Uri);
                        if (b.Name == "folderAccessPermissions")
                        {
                            peopleAccess = new CloudBlob(blob.Uri);
                        }
                        Debug.WriteLine(blob.Name);

                    }
                }

            }

            Debug.WriteLine(peopleAccess.Uri);


            return peopleAccess;


        }

        public static async Task setUpAlbumAccess(string username)
        {


            StorageFolder folder = KnownFolders.PicturesLibrary;
            StorageFile file = null;
            List<AlbumInfo> albumsAccessing = new List<AlbumInfo>();
            //--- peopleWithAccess.Add(null);

            //string tempFolderName = "";
            //     string FinalGeoFileName = FileResults.GUID;
            try
            {
                file = await folder.CreateFileAsync("folderAccessPermissions", CreationCollisionOption.ReplaceExisting);
                using (Stream outputStream = await file.OpenStreamForWriteAsync())
                {
                    XmlSerializer ser2 = new XmlSerializer(typeof(List<AlbumInfo>));
                    ser2.Serialize(outputStream, albumsAccessing);
                }

                CloudController MyUpload2 = new CloudController();
                Boolean successfull = await MyUpload2.uploadFile(file, "folderAccessPermissions", username); //Change to username eventually
                Debug.WriteLine(successfull);
            }
            catch
            {
                //It already has been transferred
            }


        }

        public static async Task setUpPeopleAccess(String folderName)
        {


            StorageFolder folder = KnownFolders.PicturesLibrary;
            StorageFile file = null;
            List<String> peopleWithAccess = new List<String>();
            //--- peopleWithAccess.Add(null);

            //string tempFolderName = "";
            //     string FinalGeoFileName = FileResults.GUID;
            try
            {
                file = await folder.CreateFileAsync("folderAccessPermissions", CreationCollisionOption.ReplaceExisting);
                using (Stream outputStream = await file.OpenStreamForWriteAsync())
                {
                    XmlSerializer ser2 = new XmlSerializer(typeof(List<String>));
                    ser2.Serialize(outputStream, peopleWithAccess);
                }

                CloudController MyUpload2 = new CloudController();
                Boolean successfull = await MyUpload2.uploadFile(file, folderName + "/folderAccessPermissions", MainPage.personNamePub); //Change to username eventually
                Debug.WriteLine(successfull);
            }
            catch
            {
                //It already has been transferred
            }


        }

        public async Task<List<Uri>> getFolders(String containerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=evokeintern;AccountKey=YLCmXUh5O0Dngo0q6eS8c1qiw6Nh93hB2pZzJoHuWxiKvleriHJyguelic0WvXCuqtuVPScOtNSaDmZVgfc+bA==");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            List<Uri> folders = new List<Uri>();

            //2. Loop over container and grab folders.

            var segment = await container.ListBlobsSegmentedAsync(null);
            foreach (IListBlobItem item in segment.Results)
            {
                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    // we know this is a sub directory now
                    CloudBlobDirectory subFolder = (CloudBlobDirectory)item;

                    Debug.WriteLine("Directory: {0}", subFolder.Uri);
                    folders.Add(subFolder.Uri);

                }
            }

            return folders;
        }
        public static async Task<List<String>> getFolderNames(String containerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=evokeintern;AccountKey=YLCmXUh5O0Dngo0q6eS8c1qiw6Nh93hB2pZzJoHuWxiKvleriHJyguelic0WvXCuqtuVPScOtNSaDmZVgfc+bA==");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            List<String> folders = new List<String>();

            //2. Loop over container and grab folders.

            var segment = await container.ListBlobsSegmentedAsync(null);
            foreach (IListBlobItem item in segment.Results)
            {
                if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    // we know this is a sub directory now
                    CloudBlobDirectory subFolder = (CloudBlobDirectory)item;

                    Debug.WriteLine("Directory: {0}", subFolder.Uri);
                    string name = (subFolder.Uri.Segments.Last().Replace("/", ""));
                    if (!String.IsNullOrEmpty(name))
                    {
                        folders.Add(name);
                    }

                }
            }

            return folders;
        }

        public async Task<List<Uri>> getList(String containerName)//CloudBlobContainer container)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=evokeintern;AccountKey=YLCmXUh5O0Dngo0q6eS8c1qiw6Nh93hB2pZzJoHuWxiKvleriHJyguelic0WvXCuqtuVPScOtNSaDmZVgfc+bA==");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer tainerPics = blobClient.GetContainerReference(containerName);

            await tainerPics.CreateIfNotExistsAsync();

            // Retrieve reference to a blob named "photo1.jpg".
            //  CloudBlockBlob blockBlob = container.GetBlockBlobReference("photo1.jpg");

            // Save blob contents to a file.
            List<Uri> uriis = new List<Uri>();
            var segment = await tainerPics.ListBlobsSegmentedAsync(null);

            foreach (IListBlobItem item in segment.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    Debug.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
                    uriis.Add(blob.Uri);

                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                    Debug.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);
                    uriis.Add(pageBlob.Uri);

                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                    Debug.WriteLine("Directory: {0}", directory.Uri);
                    uriis.Add(directory.Uri);
                }
            }
            return uriis;
        }

        public void deleteBlob(string containerName, string blobName)
        {
            // Retrieve storage account from connection string.

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=evokeintern;AccountKey=YLCmXUh5O0Dngo0q6eS8c1qiw6Nh93hB2pZzJoHuWxiKvleriHJyguelic0WvXCuqtuVPScOtNSaDmZVgfc+bA==");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Retrieve reference to a blob named "myblob.txt".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            // Delete the blob.
            blockBlob.DeleteAsync();
        }

        public static async void deleteFolder(string containerName, string folderName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=evokeintern;AccountKey=YLCmXUh5O0Dngo0q6eS8c1qiw6Nh93hB2pZzJoHuWxiKvleriHJyguelic0WvXCuqtuVPScOtNSaDmZVgfc+bA==");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            //1. grab a folder from the container
            CloudBlobDirectory folder = container.GetDirectoryReference(folderName);




            var segment = await folder.ListBlobsSegmentedAsync(null);

            foreach (String person in await AlbumSettings.getAllPeoplePermissions(folderName))
            {
                await AlbumSettings.deleteFolderPermission(person, folderName, MainPage.personNamePub);



            }

            foreach (IListBlobItem item in segment.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    //blobUri.Add(blob.Uri);
                    Debug.WriteLine("Block blob: {0}", blob.Uri);
                    await blob.DeleteAsync();

                }
            }


            //ALSO DELETE IT FROM THE LIST OF FOLDERS THAT PEOPLE HAVE IT SHARED WITH 



        }
    }
}