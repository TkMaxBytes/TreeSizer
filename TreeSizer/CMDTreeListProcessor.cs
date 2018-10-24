using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using log4net;
using System.Data.SQLite;
using com.treesizer.exceptions;

namespace com.treesizer.process
{

    /// <summary>
    /// This class will accept a file containing a DOS Directory listing and after checking to see that it is a of the expected type it will produce a set of nodes for saving in a database.
    /// </summary>
    /// <seealso cref="com.treesizer.process.TreeListProcessor" />
    public class CMDTreeListProcessor : TreeListProcessor
    {

        #region Constants

        #endregion //================================================

        #region Enums

        #endregion //================================================

        #region Variables
        private ILog mobjLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public CMDTreeListProcessor(FileInfo objfile) : base(objfile) { }
        // Terrence Knoesen Number of Dirs in TreeListFile.
        long mlngDirs = 0;
        // Terrence Knoesen Number of Files in TreeListFile.
        long mlngFiles = 0;
        #endregion //================================================

        #region Constructors

        #endregion //================================================

        #region Properties

        #endregion //================================================

        #region Methods
        /// <summary>
        /// This is is the main process and should process the command line file into the SQLite database.
        /// </summary>
        public override void Start()
        {
            string strMess = null;
            mobjLog.Debug("Enter");
            /**Terrence Knoesen 
             * Check to see if this is a DOS directory listing
            **/
            try
            {
                CheckIsDosFile(mobjTreeListFile, out mlngDirs, out mlngFiles);
            }
            catch (Exception ex)
            {
                strMess = "Failed to establish type of file!";
                mobjLog.Fatal(strMess, ex);
                mobjLog.Debug("Exit");
                throw ex;
            }
            /**Terrence Knoesen 
             * Now we can process the file.
            **/

            mobjLog.Debug("Exit");
        }//Start Method

        /// <summary>
        /// The method will check to see if the file that <code>objTreeListFile</code> parameter is actually a DOS directory listing file.
        /// </summary>
        /// <param name="objTreeListFile">A DOS directory listing file.</param>
        /// <param name="lngDirsCnt">Number of directories found in the file by reading the summary at the end of the file.</param>
        /// <param name="lngFilesCnt">Again the number of files found in the summary at the end of the DOS Directory Listing file.</param>
        /// <exception cref="ApplicationException">This error is thrown if there is no DOS Directory listing file.</exception>
        private void CheckIsDosFile(FileInfo objTreeListFile, out long lngDirsCnt, out long lngFilesCnt)
        {

            string strMess = null;
            mobjLog.Debug("Enter");
            lngDirsCnt = 0;
            lngFilesCnt = 0;

            strMess = "Check to see if this is a DOS type directory listing.";
            mobjLog.Debug(strMess);

            if (objTreeListFile == null)
            {
                strMess = "There is no TreeListFile specified!";
                throw new ApplicationException(strMess);
            }
            if (!objTreeListFile.Exists)
            {
                strMess = String.Format("The TreeListFile doesn't exist!\n\nFile is '{0}' ", objTreeListFile.FullName);
                throw new ApplicationException(strMess);
            }
            /**Terrence Knoesen 
             * Check to make sure that the file is not zero Length
            **/
            if (objTreeListFile.Length == 0)
            {
                strMess = String.Format("The TreeListFile '{0}' \n\nIs zero bytes in length!", objTreeListFile.FullName);
                //throw new TSFileZeroLengthException(strMess);
                throw new TSFileZeroLengthException(strMess);
            }


            long lngFileSize = 0;
            StreamReader strm = new StreamReader(objTreeListFile.FullName);
            List<int> lstBuf = new List<int>();
            long lngIdx = 0;
            lngFileSize = strm.BaseStream.Length;
            //int intLineCnt = 0;
            Stream strmB = strm.BaseStream;
            byte[] bytBuff = new byte[1];
            //bool blnRead = true;
            bool blnIsDosDir = false;
            while (lngIdx < lngFileSize & lngIdx < 200)
            {
                lngIdx++;
                if (strmB.CanSeek)
                {
                    strmB.Seek(lngFileSize - lngIdx, SeekOrigin.Begin);
                    lstBuf.Add(strmB.ReadByte());

                }
                else
                {
                    strMess = "TreeListFile does not support seeking!\nCan't check to see if it is a DOS directory listing!!";
                    throw new ApplicationException(strMess);
                }

            }//While Loop
             /**Terrence Knoesen 
              * Check that the amount over
            **/
            if (lngIdx >= lngFileSize)
            {
                strMess = String.Format("The TreeListFile '{0}' is too small to be a DOS directory list!", objTreeListFile.FullName);
                throw new ApplicationException(strMess);
            }
            string strLastLines = null;
            lstBuf.Reverse();
            foreach (var item in lstBuf)
            {
                strLastLines += Char.ConvertFromUtf32(item);
            }

            strLastLines = strLastLines.Replace("\r", String.Empty);
            strLastLines = strLastLines.Replace("\n", String.Empty);

            /**Terrence Knoesen 
             * Check that we have a DOS dir command produced file.
            **/
            //"Total Files Listed:"1
            Regex objRegx = new Regex(@"Total\sFiles\sListed\:");
            string fldFiles = null;
            string fldDirs = null;
            MatchCollection colMatches = null;
            if (objRegx.IsMatch(strLastLines))
            {
                blnIsDosDir = true;
                objRegx = new Regex(@"\:\s+(?<fldFiles>.+)\sFile\(s\)");
                colMatches = objRegx.Matches(strLastLines);
                if (colMatches.Count > 0)
                {
                    fldFiles = colMatches[0].Groups["fldFiles"].Value;
                }
                else
                {
                    blnIsDosDir = false;
                }
                if (blnIsDosDir == true)
                {
                    objRegx = new Regex(@"\sbytes\s+(?<fldDirs>.+)\sDir\(s\)");
                    colMatches = objRegx.Matches(strLastLines);
                    if (colMatches.Count > 0)
                    {
                        fldDirs = colMatches[0].Groups["fldDirs"].Value;

                    }
                    else
                    {
                        blnIsDosDir = false;
                    }
                }
            }
            if (blnIsDosDir)
            {
                fldDirs = fldDirs.Replace(",", string.Empty);
                long.TryParse(fldDirs, out lngDirsCnt);

                fldFiles = fldFiles.Replace(",", string.Empty);
                long.TryParse(fldFiles, out lngFilesCnt);
            }
            else
            {
                /**Terrence Knoesen 
                 * This is not a DOS Directory listing file so throw an error
                **/
                strMess = String.Format("The TreeListFile '{0}' is not a DOS directory listing!", objTreeListFile.FullName);
                throw new ApplicationException(strMess);
            }

            mobjLog.Debug("Exit");
        }


        private void ProcessCommandLineDirListing(FileInfo objListingFile, SQLiteConnection objDbCon)
        {
            mobjLog.Debug("Enter");
            string strMess = null;
            /**Terrence Knoesen 
             * Check the incomming parameters
            **/
            if (objListingFile == null)
            {
                strMess = "The DOS directory listing file must be specified!";
                throw new ArgumentNullException("objTreeListFile", strMess);
            }


            mobjLog.Debug("Exit");
        }

        #endregion //================================================

        #region Events

        #endregion //================================================

    }//Class

}//NameSpcae

