using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MarketFlowLibrary
{
    public class MarketFlowRepository
    {
        MarketFlowDatabase db = null;
        protected static string dbLocation;
        protected static MarketFlowRepository me;

        //public static string sqliteFilename = "Reader.db";
        private static string NoteTableName = "Note2";
        private static string HighlightTableName = "Highlight2";

        static MarketFlowRepository()
        {
            me = new MarketFlowRepository();
        }

        protected MarketFlowRepository()
        {
            // set the db location
            dbLocation = DatabaseFilePath;
            Debug.WriteLine(dbLocation);
            // instantiate the database	
            db = new MarketFlowDatabase(dbLocation);
        }

        public static string DatabaseFilePath
        {
            get
            {
                string dbName = "MarketFlow.db";
                string dbPath;

                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "../Library/"); // Library folder
                dbPath = Path.Combine(libraryPath, dbName);


                return dbPath;
            }
        }

        public static void ResetTables()
        {
            me.db.DropAllTables();
        }

        public static bool DropUserSettingsTable(bool recreate = true)
        {
            return me.db.DropTable<UserSettings>(recreate);
        }




        #region Generic Methods
        public static T GetIem<T>(int id) where T : IBusinessEntity, new()
        {
            return me.db.GetItem<T>(id);
        }

        public static IEnumerable<T> GetItems<T>() where T : IBusinessEntity, new()
        {
            return me.db.GetItems<T>();
        }

        public static int SaveItem<T>(T item) where T : IBusinessEntity, new()
        {
            return me.db.SaveItem<T>(item);
        }

        public static int DeleteItem<T>(int id) where T : IBusinessEntity, new()
        {
            return me.db.DeleteItem<T>(id);
        }

        public static bool DropTable<T>(bool recreate = true) where T : IBusinessEntity, new()
        {
            return me.db.DropTable<T>(recreate);
        }
        #endregion

        //      #region Bookmarks
        //      public static bool DropBookmarkTable(bool recreate = true)
        //      {
        //          return me.db.DropTable<Bookmark>(recreate);
        //      }

        //      public static Bookmark GetBookmark(int id)
        //      {
        //          return me.db.GetItem<Bookmark>(id);
        //      }

        //      public static List<Bookmark> GetBookBookmarks(int bookId)
        //      {
        //          return me.db.Query<Bookmark>("SELECT * From Bookmark where BookID = " + bookId + " ORDER BY PageNumber");
        //      }

        //      public static int SaveBookmark(Bookmark item)
        //      {
        //          return me.db.SaveItem<Bookmark>(item);
        //      }

        //      public static int DeleteBookmark(int id)
        //      {
        //          return me.db.DeleteItem<Bookmark>(id);
        //      }

        //public static int DeleteBookBookmarks (IEnumerable<int> bookIds)
        //{
        //	string bookIdsString = string.Join (",", bookIds);

        //	return me.db.Execute(string.Format("Delete FROM Bookmark WHERE BookID IN ({0})", bookIdsString));
        //}
        //#endregion

        //#region Annotations
        //public static List<AnnotationView> GetBookAnnotations(int bookId)
        //{
        //	return me.db.Query<AnnotationView>(string.Format("SELECT A.ID, BookID, SpineIndex, Type, BlockID, AnnotationImageID, Image, ImageURL, PageWidth, PageHeight FROM Annotation AS A LEFT JOIN AnnotationImage AS AI ON A.AnnotationImageID = AI.ID WHERE A.BookID = '{0}'", bookId));
        //}

        //public static AnnotationView GetAnnotationByBlock(int bookId, int blockId, int spineIndex)
        //{
        //	return me.db.Query<AnnotationView>(string.Format("SELECT A.ID, BookID, SpineIndex, Type, BlockID, AnnotationImageID, Image, ImageURL, PageWidth, PageHeight FROM Annotation AS A LEFT JOIN AnnotationImage AS AI ON A.AnnotationImageID = AI.ID WHERE (A.BookID = '{0}') AND (A.BlockId = '{1}') AND (A.SpineIndex = '{2}')", bookId, blockId, spineIndex)).FirstOrDefault();
        //}

        //public static int DeleteBookAnnotations (IEnumerable<int> bookIds)
        //{
        //	string bookIdsString = string.Join (",", bookIds);

        //	List<int> annotationImageIds = me.db.Table<Annotation> ().Where (a => bookIds.Contains (a.BookID)).Select (a => a.AnnotationImageID).ToList();
        //	string annotationImageIdsString = string.Join (",", annotationImageIds);

        //	int result = me.db.Execute(string.Format("Delete FROM Annotation WHERE BookID IN ({0})", bookIdsString));
        //	result += me.db.Execute(string.Format("Delete FROM AnnotationImage WHERE ID IN ({0})", annotationImageIdsString));

        //	return result;
        //}
        //#endregion

    }
}
