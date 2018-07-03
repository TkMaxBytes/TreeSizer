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
        private static SQLiteConnection mobjDbConnection = null;
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

                mobjDbConnection = CreateDatabase(strDatabasePath);
            }


            mobjLog.Debug("Exit");
        }



        public static SQLiteConnection GetDbConnection(string strDatabasePath)
        {
            string strMess = null;
            mobjLog.Debug("Enter");

            SQLiteConnection objCon = null;

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

            /**Terrence Knoesen 
             * Attempt to create the database SQLITE file first.
            **/
            if (!File.Exists(strDatabasePath))
            {

                mobjDbConnection = CreateDatabase(strDatabasePath);
                mobjLog.Debug("Exit");
                return mobjDbConnection;
            }
            else
            {
                /**Terrence Knoesen 
                 * Just need to open the database
                **/
                try
                {
                    objCon = new SQLiteConnection(String.Format("Data Source={0};", objfl.FullName));
                    mobjDbConnection = objCon;
                    mobjLog.Debug("Exit");
                    return mobjDbConnection;
                }
                catch (Exception ex)
                {
                    strMess = String.Format("There was an error trying to connect to database'{0}' !", strDatabasePath);
                    mobjLog.Error(strMess,ex);
                    throw ex;
                }
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
                CreateDatabaseSchema(objCon);
                return objCon;
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


        private static void CreateDatabaseSchema(SQLiteConnection objCon) {
            string strMess = null;
            mobjLog.Debug("Enter");
            /**Terrence Knoesen 
             * Check to see that there is a conneciton to the SQLite DB
            **/
            if (objCon == null)
            {
                strMess = "There is no SQLiteConnection specified";
                mobjLog.Error(strMess);
                throw new ArgumentNullException("objCon");
            }
            try
            {
                objCon.Open();
            }
            catch (Exception ex)
            {
                strMess = "Unable to open the database connnection!";
                mobjLog.Error(strMess, ex);
                throw ex;
            }

            if (objCon.State != System.Data.ConnectionState.Open)
            {
                strMess = String.Format("The connection to the database is not open!");
                mobjLog.Error(strMess);
                throw new ApplicationException(strMess);
            }
            /**Terrence Knoesen     
             * Create the command that will be run to create
             * the schema for the database.
            **/
            string strSQL = null;
            strSQL = @"CREATE TABLE tblNode(
                        fldID            TEXT PRIMARY KEY    NOT NULL,
                        fldParentID      TEXT                        ,
                        fldPath          TEXT                NOT NULL,
                        fldSize          NUMBER              NOT NULL
                        );";
            SQLiteCommand objCmd = null;
            try
            {
                objCmd = new SQLiteCommand(strSQL, objCon);
            }
            catch (Exception ex)
            {
                strMess = "Unable to create the database command for creating the scheama!";
                mobjLog.Error(strMess, ex);
                throw new ApplicationException(strMess, ex);
            }

            /**Terrence Knoesen 
             * Attempt to create the schema specified above.
            **/
            try
            {
                objCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strMess = "Couldn't execute the command to create the schema!";
                mobjLog.Error(strMess, ex);
                throw new ApplicationException(strMess, ex);
            }



            mobjLog.Debug("Exit");
        }

        #endregion //================================================

        #region Events

        #endregion //================================================








    }
}
