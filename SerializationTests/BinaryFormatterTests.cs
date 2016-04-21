using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Data;

using NUnit.Framework;

namespace SerializationTests
{
    [TestFixture]
    public class BinaryFormatterTests
    {
        private static string DataFilePath = "OutputData.bin";

        [Test]
        public void SerializeObjectWithBinaryFormatter()
        {
            var data = new FakeEntity { ID = 1, Name = "John Doe" };
            
            using (var stream = new FileStream(DataFilePath, FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
            }
        }

        [Test]
        public void DeserializeObjectWithBinaryFormatter()
        {
            FakeEntity data;
            using (var stream = new FileStream(DataFilePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                var obj = formatter.Deserialize(stream);
                data = (FakeEntity)obj;
            }
            
            Assert.That(data, Is.Not.Null);
            Assert.That(data.ID, Is.EqualTo(1));
            Assert.That(data.Name, Is.EqualTo("John Doe"));
        }
    }
}
