
using Data;

using Enyim.Caching;
using Enyim.Caching.Memcached;

using NUnit.Framework;

namespace SerializationTests
{
    [TestFixture]
    public class EnyimCachingTests
    {
        private static string CacheKey = "SampleData";

        [Test]
        public void SerializeObjectWithEnyim()
        {
            var data = new FakeEntity { ID = 1, Name = "John Doe" };

            using (var memcached = new MemcachedClient())
            {
                memcached.Store(StoreMode.Set, CacheKey, data);
            }
        }

        [Test]
        public void DeserializeObjectWithEnyim()
        {
            FakeEntity data;
            using (var memcached = new MemcachedClient())
            {
                data = (FakeEntity)memcached.Get(CacheKey);
            }

            Assert.That(data, Is.Not.Null);
            Assert.That(data.ID, Is.EqualTo(1));
            Assert.That(data.Name, Is.EqualTo("John Doe"));
        }
    }
}
