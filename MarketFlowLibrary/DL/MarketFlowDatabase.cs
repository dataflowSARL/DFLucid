using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketFlowLibrary
{
    /// <summary>
    /// ContactDatabase builds on SQLite.Net and represents a specific database, in our case, the Task DB.
    /// It contains methods for retrieval and persistance as well as db creation, all based on the 
    /// underlying ORM.
    /// </summary>
    public class MarketFlowDatabase : SQLiteConnection
    {
        public static object locker = new object();//locker was private 

        /// <summary>
        /// Initializes a new instance of the <see cref="MyLib.DL.ContactDatabase"/> ContactDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public MarketFlowDatabase(string path) : base(path,SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex ,true)
        {
            // create the tables
            CreateTable<UserSettings>();


//			NSFileManager fileManager = NSFileManager.DefaultManager;
//
//			var URLs = fileManager.GetUrls (NSSearchPathDirectory.LibraryDirectory, NSSearchPathDomain.User);
//			NSUrl LibraryDictionary = URLs [0];
//			NSUrl url = LibraryDictionary.Append(System.IO.Path.GetFileName(path), false);
				
			//Disable Backup
			//DisableiCloudBackup.AddSkipBackupAttributeToItemAtPath (path);
        }

        public void DropAllTables()
        {
            DropTable<UserSettings>(true);
			
        }

        public bool DropTable<T>(bool recreate = true) where T : IBusinessEntity, new()
        {
			lock (locker) {
				bool result = true;
				try {
					string className = typeof(T).Name;

					this.Execute ("Drop Table " + className);

					if (recreate)
						CreateTable<T> ();
				} catch {
					result = false;
				}
				return result;
			}
        }

        public IEnumerable<T> GetItems<T>() where T : IBusinessEntity, new()
        {
            //lock (locker)
            //{
                return (from i in Table<T>() select i).ToList();
            //}
        }

        public T GetItem<T>(int id) where T : IBusinessEntity, new()
        {
            //lock (locker)
            //{
                return Table<T>().FirstOrDefault(x => x.ID == id);
            //}
        }

        public int SaveItem<T>(T item) where T : IBusinessEntity
        {
            lock (locker)
            {
                if (item.ID != 0)
                {
                    Update(item);
                    return item.ID;
                }
                else
                {
                    return Insert(item);
                }
            }
        }

        public int DeleteItem<T>(int id) where T : IBusinessEntity, new()
        {
            lock (locker)
            {
                 return Delete<T>(id);
                 //return Delete<T>(new T() { ID = id });
            }
        }
    }
}
