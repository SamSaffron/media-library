using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MediaLibrary;
using System.IO;

namespace MediaLibraryTests
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void TestStandardUsage()
        {
            var config = Configuration.DefaultVideoLibraryConfig; 
            config.RootPaths = new string[] {Path.GetFullPath(@"../../DemoLib")};
            var library = Library.Initialize(config);
            var items = library.GetRootItems();

            Assert.AreEqual(1, items.Count);

            var folder = items[0] as Folder;
            Assert.AreEqual(2, folder.Children.Count); 

        }
    }
}
