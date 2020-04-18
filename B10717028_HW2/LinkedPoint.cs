using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace B10717028_HW2
{
    public class LinkedPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        List<LinkedPoint> LinkedPoints = new List<LinkedPoint>();

        public LinkedPoint(int x, int y) {
            X = x;
            Y = y;
        }
        public LinkedPoint(Point p) {
            X = p.X;
            Y = p.Y;
        }

        public void AddNextPoint(LinkedPoint point) {
            if(point == this) return;
            if(!LinkedPoints.Contains(point)) LinkedPoints.Add(point);
        }
        public void RemoveNextPoint(LinkedPoint point) {
            if(LinkedPoints.Contains(point)) LinkedPoints.Remove(point);
        }
        public LinkedPoint[] GetLinkedPoints() {
            return LinkedPoints.ToArray();
        }
        public Point GetPointStruct() {
            return new Point(this.X, this.Y);
        }
    }
}
