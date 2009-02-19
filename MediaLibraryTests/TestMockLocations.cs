using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace MediaLibraryTests {

    [TestFixture]
    public class TestMockLocations {

        [Test]
        public void TestTreeCreation() {
            var folders = MockFolderMediaLocation.CreateMockLocations(
@"
|DemoLib
 |Movies
  |Fight Club
   part1.avi
   part2.avi
  |Rushmore
   movie.mpg
 |TV
  |The Simpsons
   |Season 1
    01.avi
    02.mkv
");
            Assert.AreEqual("DemoLib", folders[0].Path);
            Assert.AreEqual(2, folders[0].Children.Count);
        }

    }
}
