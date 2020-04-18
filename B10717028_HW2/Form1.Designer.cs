namespace B10717028_HW2
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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBoxMsgBox = new System.Windows.Forms.CheckBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.planarGraph1 = new B10717028_HW2.PlanarGraph();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBoxMsgBox
            // 
            this.checkBoxMsgBox.AutoSize = true;
            this.checkBoxMsgBox.Checked = true;
            this.checkBoxMsgBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMsgBox.Location = new System.Drawing.Point(90, 16);
            this.checkBoxMsgBox.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxMsgBox.Name = "checkBoxMsgBox";
            this.checkBoxMsgBox.Size = new System.Drawing.Size(112, 16);
            this.checkBoxMsgBox.TabIndex = 2;
            this.checkBoxMsgBox.Text = "Show MessageBox";
            this.checkBoxMsgBox.UseVisualStyleBackColor = true;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(12, 12);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // planarGraph1
            // 
            this.planarGraph1.CurrentEditMode = B10717028_HW2.PlanarGraph.EditMode.ReadOnly;
            this.planarGraph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.planarGraph1.Location = new System.Drawing.Point(0, 0);
            this.planarGraph1.Name = "planarGraph1";
            this.planarGraph1.Size = new System.Drawing.Size(800, 428);
            this.planarGraph1.TabIndex = 1;
            this.planarGraph1.Load += new System.EventHandler(this.planarGraph1_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.checkBoxMsgBox);
            this.Controls.Add(this.planarGraph1);
            this.Controls.Add(this.statusStrip1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Click on point and drag to draw a line ,Click on point and press [Del] to delete";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private PlanarGraph planarGraph1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBoxMsgBox;
        private System.Windows.Forms.Button buttonClear;
    }
}

