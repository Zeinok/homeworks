using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B10717028_HW2
{
    public partial class Form1 : Form
    {
        bool messageBoxShown = false;
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            planarGraph1.CurrentEditMode = PlanarGraph.EditMode.AddPoint;
            
        }

        private void timer1_Tick(object sender, EventArgs e) {
            //planarGraph1.Focus();
            statusStrip1.Text = "";
            bool loopFlag = false;
            List<LinkedPoint> points = new List<LinkedPoint>();
            
            foreach(var p in planarGraph1.Points) {
                if(points.Count == 0) points.Add(p);
                foreach(var np in p.GetLinkedPoints()) {
                    if(points.Contains(np)) {
                        loopFlag = true;
                        break;
                    }
                    points.Add(np);
                }
                if(loopFlag) break;
                
            }
            toolStripStatusLabel1.Text = String.Format("{0} Points {1}", points.Count, loopFlag ? "Loop detected!" : ""); 
            if(loopFlag) { 
                if(checkBoxMsgBox.Checked && messageBoxShown == false) {
                    messageBoxShown = true;
                    MessageBox.Show("Loop detected!");
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e) {
            messageBoxShown = false;
            planarGraph1.Clear();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
/*            if(planarGraph1.CurrentEditMode != PlanarGraph.EditMode.ReadOnly) {
                if(e.Control) {
                    planarGraph1.CurrentEditMode = PlanarGraph.EditMode.AddLine;
                } else planarGraph1.CurrentEditMode = PlanarGraph.EditMode.AddPoint;
            }*/
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) {

        }

        private void planarGraph1_Load(object sender, EventArgs e) {
            planarGraph1.Focus();
        }
    }
}
