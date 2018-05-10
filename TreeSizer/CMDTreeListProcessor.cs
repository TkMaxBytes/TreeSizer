using System;
using System.IO;

namespace TreeSizer
{
    public class CMDTreeListProcessor : CMTreeListProcessor
    {

        public CMDTreeListProcessor(FileInfo objfile) : base (objfile) {}


        public void Start() {
            string strMess;
            if (mobjTreeListFile == null)
            {
                strMess = "There is no TreeListFile specified!";
                throw new ApplicationException(strMess);
            }
            if (!mobjTreeListFile.Exists)
            {
                strMess = String.Format("The TreeListFile doesn't exist!\n\nFile is '{0}' ", mobjTreeListFile.FullName);
                throw new ApplicationException(strMess);
            }
            /**Terrence Knoesen 
             * Check to make sure that the file is not zero Length
            **/
            if (mobjTreeListFile.Length == 0)
            {
                strMess = String.Format("The TreeListFile '{0}' \n\nIs zero bytes in length!", mobjTreeListFile.FullName);
                throw new ApplicationException(strMess);
            }


            long lngFileSize = 0;
            StreamReader strm = new StreamReader(mobjTreeListFile.FullName);
            string strBuf = null;

            lngFileSize = strm.BaseStream.Length;
            Stream strmB = strm.BaseStream;
            byte[] bytBuff = new byte[200];
            if (lngFileSize >= 200)
            {
                if (strmB.CanSeek)
                {
                    
                    strmB.Seek(lngFileSize - 1024, 0);
                    //strmB.Read(bytBuff, (int)lngFileSize - 1024, 1024);
                    strmB.Read(bytBuff, 0, 1024);
                }                

            }


            //while (!strm.EndOfStream)
            //{
            //    strBuf = strm.ReadLine();
            //    if (!String.IsNullOrEmpty(strBuf))
            //    {
            //        lngFileSize++;
            //    }

            //}//End While
        }//Start Method

    }
}
