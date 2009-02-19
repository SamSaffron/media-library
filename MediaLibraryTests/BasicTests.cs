﻿using System;
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
        public void APIDemo()
        {
            var config = Configuration.DefaultVideoLibraryConfig; 
            //config.RootPaths = new string[] {Path.GetFullPath(@"../../DemoLib")};
            var library = Library.Initialize(config);
            var items = library.GetRootItems();

            Assert.AreEqual(1, items.Count);

            var folder = items[0] as Folder;
            Assert.AreEqual(2, folder.Children.Count);

            folder.Sort(SortOrder.Name);

            var movies = (Folder)folder.Children[0];
            var tv = (Folder)folder.Children[1];

            Assert.AreEqual("Movies", movies.Name);
            Assert.AreEqual("TV", tv.Name);


            movies.Sort(SortOrder.Name);

            var fightClub = (Movie)movies[0];
            
        }
    }
}
