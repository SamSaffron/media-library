using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MediaLibrary.Caching;

namespace MediaLibraryTests {
    [TestFixture]
    public class TestCache {

        [Test]
        public void TestSizeIsLimited() {
            var cache = new LruCache<string, int>(10);

            for (int i = 0; i < 20; i++) {
                cache[i.ToString()] = i;
            }

            Assert.AreEqual(10, cache.Count);
            for (int i = 10; i < 20; i++) {
                Assert.IsTrue(cache.ContainsKey(i.ToString()));
            }
        }

        [Test]
        public void CacheAccessShouldBumpItToTheFront() {
            var cache = new LruCache<int, int>(3);

            cache[1] = 1;
            cache[2] = 2;
            cache[3] = 3;

            var a = cache[1];
            cache[4] = 4;

            Assert.AreEqual(cache[1], 1);
            Assert.AreEqual(cache[3], 3);
            Assert.AreEqual(cache[4], 4);
            Assert.IsFalse(cache.ContainsKey(2));
        }
    }
}
