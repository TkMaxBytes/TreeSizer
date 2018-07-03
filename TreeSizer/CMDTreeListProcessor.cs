using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using log4net;
using System.Data.SQLite;

namespace com.treesizer.process
{

    public class CMDTreeListProcessor : TreeListProcessor
    {

        #region Constants

        #endregion //================================================

        #region Enums

        #endregion //================================================

        #region Variables
        private ILog mobjLog =  LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
        public void Start()
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
                throw new ApplicationException(strMess);
            }


            long lngFileSize = 0;
            StreamReader strm = new StreamReader(objTreeListFile.FullName);
            List<int> lstBuf = new List<int>();
            long lngIdx = 0;
            lngFileSize = strm.BaseStream.Length;
            int intLineCnt = 0;
            Stream strmB = strm.BaseStream;
            byte[] bytBuff = new byte[1];
            bool blnRead = true;
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
                strMess = String.Format("The TreeListFile '{0}' is too small to be a DOS directory list!",objTreeListFile.FullName);
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
        #endregion //================================================

        #region Events

        #endregion //================================================

    }//Class

}//NameSpcae

