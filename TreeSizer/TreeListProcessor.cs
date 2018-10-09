using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace com.treesizer.process
{
    public class TreeListProcessor
    {
        protected FileInfo mobjTreeListFile = null;
        private BackgroundWorker mobjBckWrk = null;


        public TreeListProcessor(FileInfo objFile)
        {
            string strMess;
            if (objFile == null)
            {
                strMess = "There is no TreeListFile specified!";
                throw new ArgumentNullException("objFile", strMess);
            }
            if (!objFile.Exists)
            {
                strMess = String.Format("The TreeListFile doesn't exist!\n\nFile is '{0}'.", objFile.FullName);
                throw new IOException(strMess);
            }
            if (objFile.Length == 0)
            {
                strMess = String.Format("The TreeListFile '{0}' \n\nIs zero bytes in length!", objFile.FullName);
                throw new ApplicationException(strMess);
            }

            //Terrence Knoesen Make a copy of the file object.
            mobjTreeListFile = new FileInfo(objFile.FullName);
        }


        public virtual void Start()
        {
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
            mobjBckWrk = new BackgroundWorker();
            mobjBckWrk.WorkerReportsProgress = true;
            mobjBckWrk.WorkerSupportsCancellation = true;
        }



    }



}
