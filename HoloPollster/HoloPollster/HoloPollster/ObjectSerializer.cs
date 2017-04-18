using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace HoloPollster
{
    class ObjectSerializer
    {
        public ObjectSerializer() { }

        public MemoryStream SerializeToStream(PollsWithMetaData pollData)
        {
            //serialize pollData into a stream that is used to upload the data to azure
            DataContractSerializer serializer = new DataContractSerializer(typeof(PollsWithMetaData));
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, pollData);
            stream.Seek(0, SeekOrigin.Begin); 
            return stream;
        }

        public async Task<PollsWithMetaData> Deserialize(CloudBlockBlob blob)
        {
            //Download serialized data via a stream. Deserialize the stream back into object type PollsWithMetaData
            MemoryStream stream = new MemoryStream();
            DataContractSerializer serializer = new DataContractSerializer(typeof(PollsWithMetaData));
            await blob.DownloadToStreamAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            PollsWithMetaData poll = (PollsWithMetaData)serializer.ReadObject(stream);
            return poll;
        }
    }

    
}
