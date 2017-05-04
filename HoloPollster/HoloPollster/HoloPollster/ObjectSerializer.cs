using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace HoloPollster
{
    /// <summary>
    /// Class ObjectSerializer.
    /// </summary>
    class ObjectSerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectSerializer"/> class.
        /// </summary>
        public ObjectSerializer() { }

        /// <summary>
        /// Serializes to stream.
        /// </summary>
        /// <param name="pollData">The poll data.</param>
        /// <returns>MemoryStream.</returns>
        public MemoryStream SerializeToStream(PollsWithMetaData pollData)
        {
            //serialize pollData into a stream that is used to upload the data to azure
            DataContractSerializer serializer = new DataContractSerializer(typeof(PollsWithMetaData));
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, pollData);
            stream.Seek(0, SeekOrigin.Begin); 
            return stream;
        }

        /// <summary>
        /// Serializes to stream.
        /// </summary>
        /// <param name="loginData">The login data.</param>
        /// <returns>MemoryStream.</returns>
        public MemoryStream SerializeToStream(LoginData loginData)
        {
            //serialize pollData into a stream that is used to upload the data to azure
            DataContractSerializer serializer = new DataContractSerializer(typeof(PollsWithMetaData));
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, loginData);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        /// Deserializes the specified BLOB.
        /// </summary>
        /// <param name="blob">The BLOB.</param>
        /// <returns>Task&lt;PollsWithMetaData&gt;.</returns>
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



        /// <summary>
        /// Usernames the serialize to stream.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>MemoryStream.</returns>
        public MemoryStream UsernameSerializeToStream(LoginData userData)
        {
            //serialize pollData into a stream that is used to upload the data to azure
            DataContractSerializer serializer = new DataContractSerializer(typeof(LoginData));
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, userData);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        /// Usernames the deserialize.
        /// </summary>
        /// <param name="blob">The BLOB.</param>
        /// <returns>Task&lt;LoginData&gt;.</returns>
        public async Task<LoginData> UsernameDeserialize(CloudBlockBlob blob)
        {
            //Download serialized data via a stream. Deserialize the stream back into object type PollsWithMetaData
            MemoryStream stream = new MemoryStream();
            DataContractSerializer serializer = new DataContractSerializer(typeof(LoginData));
            await blob.DownloadToStreamAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            LoginData userInfo = (LoginData)serializer.ReadObject(stream);
            return userInfo;
        }
    }

    
}
