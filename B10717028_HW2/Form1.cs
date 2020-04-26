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

        bool isLooped(LinkedPoint firstPoint, LinkedPoint currentPoint, List<LinkedPoint> remainPoints, List<LinkedPoint> passedPoints) {
            if(remainPoints.Count == 0) return false;

            remainPoints.Remove(currentPoint);
            //if(passedPoints.Count == 0) passedPoints.Add(currentPoint);
            
            foreach(var p in currentPoint.GetLinkedPoints()) {
                if(p!=firstPoint) p.RemoveNextPoint(currentPoint);
                if(passedPoints.Contains(p)) return true;
                
                if(isLooped(firstPoint, p, remainPoints, passedPoints)) 
                    return true;

                passedPoints.Add(p);
                //passedPoints.Add(p);
            }
            
            if(remainPoints.Count == 0) return false;
            return isLooped(firstPoint, remainPoints[0], remainPoints, passedPoints);
        }

        private void timer1_Tick(object sender, EventArgs e) {
            //planarGraph1.Focus();
            statusStrip1.Text = "";
            
            List<LinkedPoint> remainPoints = planarGraph1.Points.ToList();
            List<LinkedPoint> passedPoints = new List<LinkedPoint>();
            bool loopFlag = false;
            if(remainPoints.Count > 0)
                loopFlag = isLooped(remainPoints[0], remainPoints[0], remainPoints, passedPoints);


/*            if(remainPoints.Count > 0) {
                LinkedPoint currentPoint = remainPoints[0];
                while(true) {
                    foreach(var p in remainPoints) {

                    }
                    remainPoints.Remove(currentPoint);
                    passedPoints.Add(currentPoint);


                }
            }*/

            toolStripStatusLabel1.Text = String.Format("{0} Points {1}", planarGraph1.Points.Length, loopFlag ? "Loop detected!" : ""); 
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
