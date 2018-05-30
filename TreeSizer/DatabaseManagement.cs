using System;
using log4net;
using System.IO;
using System.Data.SQLite;

namespace com.treesizer.data
{
    public static class DatabaseManagement
    {



        #region Constants

        #endregion //================================================

        #region Enums

        #endregion //================================================

        #region Variables
        private static readonly ILog mobjLog =
        LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion //================================================

        #region Constructors

        #endregion //================================================

        #region Properties

        #endregion //================================================

        #region Methods
        public static void CheckDatabase(string strDatabasePath)
        {
            string strMess = null;
            mobjLog.Debug("Enter");

            if (String.IsNullOrEmpty(strDatabasePath))
            {
                throw new ArgumentNullException("strDatabasePath");
            }
            /**Terrence Knoesen 
             * Attempt to create the database SQLITE file first.
            **/
            if (!File.Exists(strDatabasePath)) {

                CreateDatabase(strDatabasePath);
            }


            mobjLog.Debug("Exit");
        }


        private static SQLiteConnection CreateDatabase(string strDatabasePath) {
            string strMess = null;
            mobjLog.Debug("Enter");

            if (String.IsNullOrEmpty(strDatabasePath))
            {
                throw new ArgumentNullException("strDatabasePath");
            }
            FileInfo objfl = new FileInfo(strDatabasePath);
            if (!objfl.Directory.Exists)
            {
                strMess = String.Format("The directory '{0}' doesn't exist!", objfl.Directory);
                throw new IOException(strMess);
            }
            SQLiteConnection objCon = null;
            try
            {
                objCon = new SQLiteConnection(String.Format("Data Source={0};", objfl.FullName));
                
            }
            catch (Exception ex)
            {
                strMess = String.Format("Unable to create database '{0}'!", objfl.FullName);
                mobjLog.Error(strMess, ex);
                throw new ApplicationException(strMess, ex);
            }




            mobjLog.Debug("Exit");
            return null;
        }
        #endregion //================================================

        #region Events

        #endregion //================================================








    }
}
