using System;
using System.Windows.Forms;
using System.IO;
using DotNetUtils;
using com.treesizer.process;

namespace com.treesizer.ui
{
    public partial class frmMain : Form
    {
        private PSTreeListProcessor objTree;
        public frmMain()
        {
            InitializeComponent();
            //this.Text = Program.GetApplicationName(1);
            this.Text = GeneralUtils.GetApplicationName(1);
        }

        private void button_ProcessListing_Click(object sender, EventArgs e) {
            string strMess;
            //Terrence Knoesen Clear the tree
            treeView_TreeDisplay.Nodes.Clear();
            string strFilePath = this.textBox_TreeListFilePath.Text.Trim();

            if (!File.Exists(strFilePath))
            {
                strMess = String.Format("The TreeListFile doesn't exist!\n\nFile is '{0}' ", strFilePath);
                MessageBox.Show(this, strMess, Program.GetApplicationName(1), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FileInfo fil = new FileInfo(this.textBox_TreeListFilePath.Text.Trim());
            CMDTreeListProcessor objTree = new CMDTreeListProcessor(fil);
            objTree.Start();

        }

        private void button_ProcessListing_Clickex(object sender, EventArgs e)
        {
            string strMess;
            //Terrence Knoesen Clear the tree
            treeView_TreeDisplay.Nodes.Clear();
            string strFilePath = this.textBox_TreeListFilePath.Text.Trim();

            if (!File.Exists(strFilePath))
            {
                strMess = String.Format("The TreeListFile doesn't exist!\n\nFile is '{0}' ", strFilePath);
                MessageBox.Show(this, strMess, Program.GetApplicationName(1), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FileInfo fil = new FileInfo(this.textBox_TreeListFilePath.Text.Trim());
            objTree = new PSTreeListProcessor(fil);
            objTree.OnProgress += ObjTree_OnProgress;
            objTree.OnStart += ObjTree_OnStart;
            objTree.OnComplete += ObjTree_OnComplete;
            objTree.Start();
            textBox_Lines.Text = "Started Runn";
            //ProcessTreeListFile(fil);
        }

        private void ObjTree_OnComplete(object sender, object userInfo)
        {
            textBox_Lines.Clear();
            if (userInfo != null)
            {
                Int64 intLineCnt = 0;
                intLineCnt = (Int64)userInfo;
                textBox_Lines.Text = "Comp " + intLineCnt.ToString();
                textBox_Lines.Refresh();
            }
        }

        private void ObjTree_OnStart(object sender, object userInfo)
        {
            textBox_Lines.Text = (String)userInfo;
        }

        

        private void ObjTree_OnProgress(object sender, object userInfo)
        {
            //textBox_Lines.Clear();
            Int64 intLineCnt = 0;
            intLineCnt = (Int64)userInfo;
            textBox_Lines.Text = intLineCnt.ToString();
            if (intLineCnt % 1000 == 0)
            {
                textBox_Lines.Refresh();
            }
            
        }


        private void button_StopTreeProcess_Click(object sender, EventArgs e)
        {
            if (objTree != null)
            {
                objTree.Stop();
            }
        }
    }
}
