using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace TreeSizer
{
    public class CMDTreeListProcessor : TreeListProcessor
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
            List<int> lstBuf = new List<int>();
            long lngIdx = 0;
            lngFileSize = strm.BaseStream.Length;
            int intLineCnt = 0;
            Stream strmB = strm.BaseStream;
            byte[] bytBuff = new byte[1];
            bool blnRead = true;
            bool blnIsDosDir = false;
            while (intLineCnt < 5)
            {
                lngIdx++;
                if (strmB.CanSeek)
                {
                    strmB.Seek(lngFileSize - lngIdx, SeekOrigin.Begin);
                    lstBuf.Add(strmB.ReadByte());
                    if (lstBuf[lstBuf.Count -1] == 10) intLineCnt++;
                    Debug.Write(Char.ConvertFromUtf32(lstBuf[lstBuf.Count - 1]));
                    

                }
                else
                {
                    blnRead = false;
                }

            }
            string strLastLines = null;
            lstBuf.Reverse();
            foreach (var item in lstBuf)
            {
                strLastLines += Char.ConvertFromUtf32(item);
            }
            Debug.WriteLine(strLastLines);
        }//Start Method

    }
}
