using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HoloPollster
{
    class ObjectSerializer
    {
        public ObjectSerializer() { }

        public MemoryStream StreamData(PollsWithMetaData pollData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PollsWithMetaData));
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(XmlWriter.Create(stream), pollData);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }

    
}
