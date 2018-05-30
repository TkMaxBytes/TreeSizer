using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;

namespace com.treesizer.process
{
    public delegate void ProgressHandler(object sender, object userInfo);
    public delegate void StartHandler(object sender, object userInfo);
    public delegate void CompleteHandler(object sender, object userInfo);

    public class PSTreeListProcessor
    {
        private FileInfo mobjTreeListFile = null;
        private BackgroundWorker mobjBckWrk = null;
        public event ProgressHandler OnProgress;
        public event StartHandler OnStart;
        public event CompleteHandler OnComplete;

        public PSTreeListProcessor(FileInfo objFile) {
            string strMess;
            if (objFile == null)
            {
                strMess = "There is no TreeListFile specified!";
                throw new ArgumentNullException("objFile", strMess);
            }
            if (!objFile.Exists)
            {
                strMess = String.Format("The TreeListFile doesn't exist!\n\nFile is '{0}' ", objFile.FullName);
                throw new ApplicationException(strMess);
            }
            //Terrence Knoesen Make a copy of the file object.
            mobjTreeListFile = new FileInfo(objFile.FullName);
        }

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
            mobjBckWrk = new BackgroundWorker();
            mobjBckWrk.WorkerReportsProgress = true;
            mobjBckWrk.WorkerSupportsCancellation = true;
            mobjBckWrk.DoWork += MobjBckWrk_DoWork;
            mobjBckWrk.ProgressChanged += MobjBckWrk_ProgressChanged;
            mobjBckWrk.RunWorkerCompleted += MobjBckWrk_RunWorkerCompleted;
            
            mobjBckWrk.RunWorkerAsync(mobjTreeListFile);
            RaiseOnStart(this, "Start");
            //OnProgressCall(new EventArgs(""));
        }
        public void Stop()
        {
            if (mobjBckWrk != null && mobjBckWrk.IsBusy == true)
            {
                mobjBckWrk.CancelAsync();
            }
        }

        /**Terrence Knoesen 
         * Raising a local event**/
        protected virtual void RaiseOnProgress(object sender, object userArg)
        {
            ProgressHandler handler = OnProgress;
            if (handler != null)
            {
                handler(this, userArg);
            }
        }
        protected virtual void RaiseOnStart(object sender, object userArg)
        {
            
            StartHandler handler = OnStart;
            if (handler != null)
            {
                handler(this, userArg);
            }
        }
        protected virtual void RaiseOnComplete(object sender, object userArg)
        {
            CompleteHandler handler = OnComplete;
            if (handler != null)
            {
                handler(this, userArg);
            }
        }

        /**Terrence Knoesen 
         * Background Worker Events **/
        private void MobjBckWrk_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //RaiseOnProgress(this, e.UserState);
            RaiseOnProgress(this, e.UserState);
        }
        private void MobjBckWrk_DoWork(object sender, DoWorkEventArgs e)
        {
            string strMess;
            FileInfo objFil = (FileInfo)e.Argument;
            if (objFil == null)
            {
                strMess = "There is no TreeListFile specified!";
                throw new ArgumentNullException("objfil", strMess);
            }
            
            BackgroundWorker objBkWrk = (BackgroundWorker)sender;

            Int64 intCnt = 0;
            StreamReader strm = new StreamReader(objFil.FullName);
            string strBuf = null;
            while (!strm.EndOfStream)
            {
                if (objBkWrk.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                strBuf = strm.ReadLine();
                if (!String.IsNullOrEmpty(strBuf))
                {
                    intCnt++;
                    objBkWrk.ReportProgress(0, intCnt);
                }

            }
            strm.Close();
            RaiseOnComplete(this, intCnt);

        }
        private void MobjBckWrk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
