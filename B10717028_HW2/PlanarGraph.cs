using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B10717028_HW2
{

    public partial class PlanarGraph : UserControl
    {
        public enum EditMode
        {
            ReadOnly,
            AddPoint,
            AddLine
        }
        public EditMode CurrentEditMode { get; set; }

        public LinkedPoint[] Points { get => linkedPoints.ToArray(); }
        List<LinkedPoint> linkedPoints = new List<LinkedPoint>();
        LinkedPoint selectedPoint = null;
        LinkedPoint hoveredPoint = null;
        Point currentMouseLocation = new Point();
        Point mouseDownLocation = new Point();

        Pen linePen = Pens.Black;
        Brush pointBrush = new SolidBrush(Color.DarkRed);
        Pen pointPen = new Pen(Color.Black, 1);
        Pen pointPenSelected = new Pen(Color.Red, 1);
        int pointWidth = 8;

        public PlanarGraph() {
            InitializeComponent();
        }

        public void DeleteSelectedPoint() {

        }
        public void Clear() {
            selectedPoint = null;
            hoveredPoint = null;
            linkedPoints.Clear();
            updateGraph();
        }

        private RectangleF GetPointRectangleF(Point p) {
            RectangleF rectF = new RectangleF(p.X - pointWidth / 2, p.Y - pointWidth / 2, pointWidth, pointWidth);
            return rectF;
        }

        private bool InsideHitbox(Point location, LinkedPoint point, int hitboxWidth) {
            return (Math.Abs(location.X - point.X) < hitboxWidth && Math.Abs(location.Y - point.Y) < hitboxWidth);
        }

        private void updateGraph() {
            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics g = Graphics.FromImage(bitmap);
            foreach(var p in linkedPoints) {
                Point point = new Point(p.X, p.Y);
                foreach(var np in p.GetLinkedPoints()) {
                    g.DrawLine(linePen, p.GetPointStruct(), np.GetPointStruct());
                }
                RectangleF rectF = GetPointRectangleF(p.GetPointStruct());
                g.FillEllipse(pointBrush, rectF);
                g.DrawEllipse(pointPen, rectF);
            }
            pictureBox.Image = bitmap;
        }
        private void pictureBox_Paint(object sender, PaintEventArgs e) {

            e.Graphics.DrawLine(Pens.Black, new Point(currentMouseLocation.X, 0), new Point(currentMouseLocation.X, this.Height));
            e.Graphics.DrawLine(Pens.Black, new Point(0, currentMouseLocation.Y), new Point(this.Width, currentMouseLocation.Y));

            if(CurrentEditMode == EditMode.AddLine) 
                if(selectedPoint != null)
                    e.Graphics.DrawLine(linePen, selectedPoint.GetPointStruct(), currentMouseLocation);
            

            if(selectedPoint != null) {
                RectangleF rectF = GetPointRectangleF(selectedPoint.GetPointStruct());
                e.Graphics.DrawEllipse(pointPenSelected, rectF);
            }
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e) {
           
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e) {
            currentMouseLocation = this.PointToClient(Cursor.Position);
            foreach(var p in linkedPoints) {
                if(InsideHitbox(e.Location, p, pointWidth)) {
                    currentMouseLocation = p.GetPointStruct();
                    hoveredPoint = p;
                }
            }
            pictureBox.Invalidate();
        }

        private void PlanarGraph_KeyDown(object sender, KeyEventArgs e) {
            if(CurrentEditMode != EditMode.ReadOnly) {
                if(e.Control) {
                    CurrentEditMode = EditMode.AddLine;
                } else CurrentEditMode = EditMode.AddPoint;
            }
        }

        private void PlanarGraph_KeyUp(object sender, KeyEventArgs e) {
            if(CurrentEditMode != EditMode.ReadOnly) {
                //CurrentEditMode = EditMode.AddPoint;
                if(selectedPoint != null) {
                    if(e.KeyCode == Keys.Delete) {
                        foreach(var p in linkedPoints) {
                            p.RemoveNextPoint(selectedPoint);
                        }
                        linkedPoints.Remove(selectedPoint);
                        selectedPoint = null;
                        updateGraph();
                    }
                }
            }
        }

        private void PlanarGraph_MouseDown(object sender, MouseEventArgs e) {

        }

        private void PlanarGraph_MouseUp(object sender, MouseEventArgs e) {

        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e) {
            if(CurrentEditMode == EditMode.ReadOnly) return;

            if(e.Button == MouseButtons.Right) return;
            bool doUpdateGraph = false;
            bool notDragging = (mouseDownLocation.X == e.Location.X) && (mouseDownLocation.Y == e.Location.Y);
            //

            //
            if(CurrentEditMode == EditMode.AddLine && selectedPoint != null) { // add line
                if(!hoveredPoint.GetLinkedPoints().Contains(selectedPoint))
                    selectedPoint.AddNextPoint(hoveredPoint);
                hoveredPoint = null;
                doUpdateGraph = true;
            }

            selectedPoint = null;
            foreach(var p in linkedPoints) { // focus point on click
                if(InsideHitbox(currentMouseLocation, p, pointWidth))
                    selectedPoint = p;
            }

            if(selectedPoint == null && notDragging) { // add point if no point is selected
                var np = new LinkedPoint(currentMouseLocation);
                linkedPoints.Add(np);
                doUpdateGraph = true;
            }


            CurrentEditMode = EditMode.AddPoint;
            if(doUpdateGraph) updateGraph();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e) {
            mouseDownLocation = e.Location;
            if(CurrentEditMode == EditMode.ReadOnly) return;
            if(hoveredPoint == selectedPoint) {
                CurrentEditMode = EditMode.AddLine;
            }
        }
    }
}
