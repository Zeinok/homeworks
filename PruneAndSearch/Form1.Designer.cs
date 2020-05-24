namespace PruneAndSearch
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.labelK = new System.Windows.Forms.Label();
            this.comboBoxK = new System.Windows.Forms.ComboBox();
            this.labelP = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonStep = new System.Windows.Forms.Button();
            this.comboBoxP = new System.Windows.Forms.ComboBox();
            this.labelEqu = new System.Windows.Forms.Label();
            this.comboBoxPval = new System.Windows.Forms.ComboBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpen});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(80, 22);
            this.toolStripButtonOpen.Text = "開啟檔案 (&O)";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(10, 10);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(10);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(780, 331);
            this.textBoxLog.TabIndex = 1;
            this.textBoxLog.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // labelK
            // 
            this.labelK.AutoSize = true;
            this.labelK.Location = new System.Drawing.Point(8, 11);
            this.labelK.Name = "labelK";
            this.labelK.Size = new System.Drawing.Size(13, 12);
            this.labelK.TabIndex = 2;
            this.labelK.Text = "K";
            // 
            // comboBoxK
            // 
            this.comboBoxK.FormattingEnabled = true;
            this.comboBoxK.Location = new System.Drawing.Point(27, 8);
            this.comboBoxK.Name = "comboBoxK";
            this.comboBoxK.Size = new System.Drawing.Size(50, 20);
            this.comboBoxK.TabIndex = 3;
            // 
            // labelP
            // 
            this.labelP.AutoSize = true;
            this.labelP.Location = new System.Drawing.Point(83, 11);
            this.labelP.Name = "labelP";
            this.labelP.Size = new System.Drawing.Size(11, 12);
            this.labelP.TabIndex = 4;
            this.labelP.Text = "P";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(10);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxPval);
            this.splitContainer1.Panel1.Controls.Add(this.labelEqu);
            this.splitContainer1.Panel1.Controls.Add(this.buttonStep);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxP);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxK);
            this.splitContainer1.Panel1.Controls.Add(this.labelK);
            this.splitContainer1.Panel1.Controls.Add(this.labelP);
            this.splitContainer1.Panel1.Margin = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxLog);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.splitContainer1.Size = new System.Drawing.Size(800, 384);
            this.splitContainer1.SplitterDistance = 30;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 5;
            // 
            // buttonStep
            // 
            this.buttonStep.Location = new System.Drawing.Point(229, 6);
            this.buttonStep.Name = "buttonStep";
            this.buttonStep.Size = new System.Drawing.Size(75, 23);
            this.buttonStep.TabIndex = 7;
            this.buttonStep.Text = "Step";
            this.buttonStep.UseVisualStyleBackColor = true;
            this.buttonStep.Click += new System.EventHandler(this.buttonStep_Click);
            // 
            // comboBoxP
            // 
            this.comboBoxP.FormattingEnabled = true;
            this.comboBoxP.Location = new System.Drawing.Point(100, 8);
            this.comboBoxP.Name = "comboBoxP";
            this.comboBoxP.Size = new System.Drawing.Size(50, 20);
            this.comboBoxP.TabIndex = 6;
            this.comboBoxP.SelectedIndexChanged += new System.EventHandler(this.comboBoxP_SelectedIndexChanged);
            // 
            // labelEqu
            // 
            this.labelEqu.AutoSize = true;
            this.labelEqu.Location = new System.Drawing.Point(156, 11);
            this.labelEqu.Name = "labelEqu";
            this.labelEqu.Size = new System.Drawing.Size(11, 12);
            this.labelEqu.TabIndex = 8;
            this.labelEqu.Text = "=";
            // 
            // comboBoxPval
            // 
            this.comboBoxPval.FormattingEnabled = true;
            this.comboBoxPval.Location = new System.Drawing.Point(173, 7);
            this.comboBoxPval.Name = "comboBoxPval";
            this.comboBoxPval.Size = new System.Drawing.Size(50, 20);
            this.comboBoxPval.TabIndex = 9;
            this.comboBoxPval.SelectedIndexChanged += new System.EventHandler(this.comboBoxPval_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 409);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label labelK;
        private System.Windows.Forms.ComboBox comboBoxK;
        private System.Windows.Forms.Label labelP;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox comboBoxP;
        private System.Windows.Forms.Button buttonStep;
        private System.Windows.Forms.ComboBox comboBoxPval;
        private System.Windows.Forms.Label labelEqu;
    }
}

