using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MediaLibrary.ORM;
using System.IO;
using MediaLibrary;

namespace MediaLibraryTests {

    [TestFixture]
    public class TestItemPersistance {

        [Test]
        public void TestDbMigration() { 
        
        }


        [Test]
        public void TestSchemaExtraction() {
            var schema = Schema.DiscoverSchema();

            var itemDef = schema.Find(def => def.TableName == "Item"); 

            Assert.IsTrue(itemDef.ColumnDefinitions.Exists(
                col => col.Name == "Id" && col.IsPrimaryKey && col.Type == typeof(Guid) )
                );

            var movieDef = schema.Find(def => def.TableName == "Video");
            Assert.IsFalse(movieDef.ColumnDefinitions.Exists(col => col.Name == "Id"));

            // should not create tables for objects with no new properties 
            var actorDef = schema.Find(def => def.TableName == "Actor");
            Assert.IsNull(actorDef);
        }

        [Test]
        public void TestMovieSaving()
        {
            var db = "test.db";
            if (File.Exists(db)) File.Delete(db);
            var repository = new ItemRepository(db);
            repository.MigrateSchema();

            var movie = new Movie();
            movie.Name = "MyMovie";
            movie.IsWatched = true;

            repository.SaveItem(movie);

            var movie2 = repository.GetItem<Movie>(movie.Id);
            Assert.AreEqual(movie.Name, movie2.Name);
            Assert.AreEqual(true, movie2.IsWatched);
        }

    }
}
