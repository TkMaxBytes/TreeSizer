namespace com.treesizer.ui
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView_TreeDisplay = new System.Windows.Forms.TreeView();
            this.button_Browse = new System.Windows.Forms.Button();
            this.textBox_TreeListFilePath = new System.Windows.Forms.TextBox();
            this.button_ProcessListing = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_StopTreeProcess = new System.Windows.Forms.Button();
            this.textBox_Lines = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // treeView_TreeDisplay
            // 
            this.treeView_TreeDisplay.Location = new System.Drawing.Point(16, 55);
            this.treeView_TreeDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.treeView_TreeDisplay.Name = "treeView_TreeDisplay";
            this.treeView_TreeDisplay.Size = new System.Drawing.Size(344, 741);
            this.treeView_TreeDisplay.TabIndex = 0;
            // 
            // button_Browse
            // 
            this.button_Browse.Location = new System.Drawing.Point(369, 23);
            this.button_Browse.Margin = new System.Windows.Forms.Padding(4);
            this.button_Browse.Name = "button_Browse";
            this.button_Browse.Size = new System.Drawing.Size(48, 25);
            this.button_Browse.TabIndex = 1;
            this.button_Browse.Text = "...";
            this.button_Browse.UseVisualStyleBackColor = true;
            // 
            // textBox_TreeListFilePath
            // 
            this.textBox_TreeListFilePath.Location = new System.Drawing.Point(16, 23);
            this.textBox_TreeListFilePath.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_TreeListFilePath.Name = "textBox_TreeListFilePath";
            this.textBox_TreeListFilePath.Size = new System.Drawing.Size(344, 22);
            this.textBox_TreeListFilePath.TabIndex = 2;
            this.textBox_TreeListFilePath.Text = "c:\\temp\\smalldir.txt";
            // 
            // button_ProcessListing
            // 
            this.button_ProcessListing.Location = new System.Drawing.Point(442, 17);
            this.button_ProcessListing.Margin = new System.Windows.Forms.Padding(4);
            this.button_ProcessListing.Name = "button_ProcessListing";
            this.button_ProcessListing.Size = new System.Drawing.Size(139, 36);
            this.button_ProcessListing.TabIndex = 3;
            this.button_ProcessListing.Text = "Process Tree";
            this.button_ProcessListing.UseVisualStyleBackColor = true;
            this.button_ProcessListing.Click += new System.EventHandler(this.button_ProcessListing_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(439, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "LinesRead:";
            // 
            // button_StopTreeProcess
            // 
            this.button_StopTreeProcess.Location = new System.Drawing.Point(442, 80);
            this.button_StopTreeProcess.Margin = new System.Windows.Forms.Padding(4);
            this.button_StopTreeProcess.Name = "button_StopTreeProcess";
            this.button_StopTreeProcess.Size = new System.Drawing.Size(139, 36);
            this.button_StopTreeProcess.TabIndex = 6;
            this.button_StopTreeProcess.Text = "Stop Process Tree";
            this.button_StopTreeProcess.UseVisualStyleBackColor = true;
            this.button_StopTreeProcess.Click += new System.EventHandler(this.button_StopTreeProcess_Click);
            // 
            // textBox_Lines
            // 
            this.textBox_Lines.Location = new System.Drawing.Point(442, 169);
            this.textBox_Lines.Name = "textBox_Lines";
            this.textBox_Lines.Size = new System.Drawing.Size(139, 22);
            this.textBox_Lines.TabIndex = 7;
            this.textBox_Lines.Text = "ee";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 821);
            this.Controls.Add(this.textBox_Lines);
            this.Controls.Add(this.button_StopTreeProcess);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_ProcessListing);
            this.Controls.Add(this.textBox_TreeListFilePath);
            this.Controls.Add(this.button_Browse);
            this.Controls.Add(this.treeView_TreeDisplay);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(610, 860);
            this.MinimumSize = new System.Drawing.Size(610, 860);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView_TreeDisplay;
        private System.Windows.Forms.Button button_Browse;
        private System.Windows.Forms.TextBox textBox_TreeListFilePath;
        private System.Windows.Forms.Button button_ProcessListing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_StopTreeProcess;
        private System.Windows.Forms.TextBox textBox_Lines;
    }
}

