using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BranchAndBound
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }

        enum CurrentStatus
        {
            LoopStart,
            FindLowestBound,
            SubstractLowestBound,
            FindMaximumSum,
            MakeColRowInfinite,
            Finish
        }
        float[,] originalTSPMatrix;
        float[,] TSPMatrix;
        float[,] nextTSPMatrix;
        float[] xLowestBound;
        float[] yLowestBound;
        Dictionary<int, int> solution = new Dictionary<int, int>();
        CurrentStatus currentStatus = CurrentStatus.LoopStart;
        Point coordinateReadyToRemove = new Point(-1, -1);

        private void toolStripButtonOpen_Click(object sender, EventArgs e) {
            var ofd = new OpenFileDialog();
            if(ofd.ShowDialog() != DialogResult.OK) return;
            var s = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var sr = new StreamReader(s, Encoding.GetEncoding(950), true);
            richTextBox1.Clear();
            appendLog("File opened.");
            int w = 0, h = 0;
            var content = sr.ReadToEnd();
            string[] splittedContent = content.Split(new char[] { ';', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            h = splittedContent.Length;
            w = splittedContent[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            TSPMatrix = new float[w, h];
            xLowestBound = new float[w];
            yLowestBound = new float[h];
            for(int line = 0; line < splittedContent.Length; line++) {
                List<float> numbers = splittedContent[line]
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .ConvertAll(
                        (v) => float.Parse(v)
                    );
                int i = 0;
                numbers.ForEach((v) => TSPMatrix[i++, line] = v);
            }
            originalTSPMatrix = (float[,])TSPMatrix.Clone();
            appendLog("Finished parsing");
            PaintTSPMatrix();
            appendLog("Painted TSP Matrix");
            currentStatus = CurrentStatus.FindLowestBound;
            solution.Clear();
        }
        private void toolStripButtonStep_Click(object sender, EventArgs e) {
            if(TSPMatrix == null) return;
            appendLog(currentStatus.ToString() + "()");
            switch(currentStatus) {
                case CurrentStatus.LoopStart:
                    LoopStart();
                    break;
                case CurrentStatus.FindLowestBound:
                    FindLowestBound();
                    break;
                case CurrentStatus.SubstractLowestBound:
                    SubstractLowestBound();
                    break;
                case CurrentStatus.FindMaximumSum:
                    FindMaximumSum();
                    break;
                case CurrentStatus.MakeColRowInfinite:
                    MakeColRowInfinite();
                    break;
                case CurrentStatus.Finish:
                    return;
            }
            PaintTSPMatrix();
            if(currentStatus != CurrentStatus.Finish)
                currentStatus = (CurrentStatus)((int)++currentStatus % (int)CurrentStatus.Finish);
        }
        private void appendLog(string s) {
            richTextBox1.AppendText(string.Format("{0}\n", s));
            richTextBox1.SelectionStart = richTextBox1.TextLength-1;
            richTextBox1.SelectionLength = 1;
            richTextBox1.ScrollToCaret();
        }
        private void PaintTSPMatrix() {
            const int cellSize = 32;
            const float cellSizeF = cellSize;
            Font cellFont = new Font(SystemFonts.DefaultFont.FontFamily, 12);
            if(TSPMatrix == null) return;
            List<int> skippedRow = new List<int>();
            List<int> skippedCol = new List<int>();
            for(int x = 0; x < TSPMatrix.GetLength(0); x++) {
                if(Enumerable.Range(0, TSPMatrix.GetLength(1))
                    .Select(
                        (y) => TSPMatrix[x, y]
                    ).Where( e => e == float.PositiveInfinity)
                    .Count() == TSPMatrix.GetLength(1))
                    skippedRow.Add(x);
            }
            for(int y = 0; y < TSPMatrix.GetLength(1); y++) {
                if(Enumerable.Range(0, TSPMatrix.GetLength(0))
                    .Select(
                        (x) => TSPMatrix[x, y]
                    ).Where(e => e == float.PositiveInfinity)
                    .Count() == TSPMatrix.GetLength(0))
                    skippedCol.Add(y);
            }
            var bmp = new Bitmap(TSPMatrix.GetLength(0) * cellSize + cellSize, TSPMatrix.GetLength(1) * cellSize + cellSize);
            var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            for(int y = 0; y < TSPMatrix.GetLength(1); y++) {
                for(int x = 0; x < TSPMatrix.GetLength(0); x++) {
                    g.DrawRectangle(Pens.Black, new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize));
                    if(skippedRow.Contains(x)) continue;
                    if(skippedCol.Contains(y)) continue;
                    if(currentStatus == CurrentStatus.Finish)
                        if(float.IsInfinity(TSPMatrix[x, y]))
                            continue;
                    var sSize = g.MeasureString(TSPMatrix[x, y].ToString(), cellFont);
                    g.DrawString(
                        TSPMatrix[x, y].ToString(),
                        cellFont,
                        Brushes.Black,
                        x * cellSizeF + cellSizeF / 2 - sSize.Width / 2,
                        y * cellSizeF + cellSizeF / 2 - sSize.Height / 2
                        );
                }
            }

            pictureBox1.Image = new Bitmap(bmp);
            bmp.Dispose();
        }
        private void LoopStart() {
            // check if matrix is empty
            Console.WriteLine("TSP infinity = {0}", TSPMatrix.Cast<float>().Where(e => float.IsInfinity(e)).Count());
            TSPMatrix.Cast<float>().Where(e => float.IsInfinity(e)).Count();
            if(TSPMatrix.Cast<float>().Where(e => float.IsInfinity(e)).Count() == TSPMatrix.GetLength(0) * TSPMatrix.GetLength(1)) {
                currentStatus = CurrentStatus.Finish;
                StringBuilder sb = new StringBuilder("Solution: ");
                Dictionary<int, int> swappedSolution = solution.ToDictionary(v => v.Value, k => k.Key);
                int nextValue = swappedSolution.Keys.Min();
                sb.Append(nextValue + 1);
                float totalCost = 0;
                while(swappedSolution.Count > 0) {
                    sb.AppendFormat("->{0}", swappedSolution[nextValue] + 1);
                    TSPMatrix[swappedSolution[nextValue], nextValue] = originalTSPMatrix[swappedSolution[nextValue], nextValue];
                    totalCost += TSPMatrix[swappedSolution[nextValue], nextValue];
                    var tmp = swappedSolution[nextValue];
                    swappedSolution.Remove(nextValue);
                    nextValue = tmp;
                }
                appendLog(sb.ToString());
                appendLog(string.Format("Total cost: {0}", totalCost));
            }

        }
        private void FindLowestBound() {
            float[,] tempTSPMatrix = (float[,])TSPMatrix.Clone();
            for(int y = 0; y < tempTSPMatrix.GetLength(1); y++) {
                yLowestBound[y] = Enumerable.Range(0, tempTSPMatrix.GetLength(0))
                                    .Select(
                                        (x) => tempTSPMatrix[x, y]
                                    ).Min();
                for(int x = 0; x < tempTSPMatrix.GetLength(0); x++)
                    if(!float.IsInfinity(tempTSPMatrix[x, y])) tempTSPMatrix[x, y] -= yLowestBound[y];
                if(yLowestBound[y]!=0&&!float.IsInfinity(yLowestBound[y]))
                    appendLog(string.Format("LB for Y:{0}={1}", y, yLowestBound[y]));
            }

            for(int x = 0; x < tempTSPMatrix.GetLength(0); x++) {
                xLowestBound[x] = Enumerable.Range(0, tempTSPMatrix.GetLength(1))
                                    .Select(
                                        (y) => tempTSPMatrix[x, y]
                                    ).Min();
                for(int y = 0; y < tempTSPMatrix.GetLength(1); y++)
                    if(!float.IsInfinity(tempTSPMatrix[x, y])) tempTSPMatrix[x, y] -= xLowestBound[x];
                if(xLowestBound[x] != 0 && !float.IsInfinity(xLowestBound[x]))
                    appendLog(string.Format("LB for X:{0}={1}", x, xLowestBound[x]));
            }
            nextTSPMatrix = tempTSPMatrix;
        }
        private void SubstractLowestBound() {
            TSPMatrix = nextTSPMatrix;
            nextTSPMatrix = null;
        }
        private void FindMaximumSum() {
            var indexOfZero = GetIndex(TSPMatrix, 0);
            List<float> values = new List<float>();
            float maxSum = float.MinValue;
            Point maxCoord = new Point(-1, -1);
            if(indexOfZero.Count > 0) maxCoord = indexOfZero[0];
            foreach(var t in indexOfZero) {
                var result = GetMinimumValue(TSPMatrix, t);
                if(float.IsInfinity(result.Item1) || float.IsInfinity(result.Item2)) {
                    appendLog(string.Format("No solution on ({0},{1})", t.X, t.Y));
                    if(!solution.ContainsKey(t.X)) solution.Add(t.X, t.Y);
                    TSPMatrix[t.X, t.Y] = float.PositiveInfinity;
                    maxCoord = new Point(-1, -1);
                    break;
                }
                float sum = result.Item1 + result.Item2;
                appendLog(string.Format("({0}, {1}) {2} + {3} = {4}", t.X, t.Y, result.Item1, result.Item2, sum));
                if(sum > maxSum) {
                    maxSum = sum;
                    maxCoord = t;
                }
            }
            if(maxCoord.X != -1)
                coordinateReadyToRemove = maxCoord;
        }
        private void MakeColRowInfinite() {
            if(coordinateReadyToRemove.X == -1) {
                return;
            }
            appendLog(string.Format("Removing {0}", coordinateReadyToRemove));
            solution.Add(coordinateReadyToRemove.X, coordinateReadyToRemove.Y);

            for(int x = 0; x < TSPMatrix.GetLength(0); x++) 
                TSPMatrix[x, coordinateReadyToRemove.Y] = float.PositiveInfinity;
            for(int y = 0; y < TSPMatrix.GetLength(1); y++) 
                TSPMatrix[coordinateReadyToRemove.X, y] = float.PositiveInfinity;
            TSPMatrix[coordinateReadyToRemove.Y, coordinateReadyToRemove.X] = float.PositiveInfinity;
            coordinateReadyToRemove = new Point(-1, -1);
        }
        List<Point> GetIndex(float[,] matrix, float value) {
            List<Point> points = new List<Point>();
            for(int y = 0; y < matrix.GetLength(1); y++)
                for(int x = 0; x < matrix.GetLength(0); x++)
                    if(matrix[x,y] == value) points.Add(new Point(x, y));
            return points;

        }
        Tuple<float, float> GetMinimumValue(float[,] matrix, int x, int y) {
            return new Tuple<float, float>(
                Enumerable.Range(0, matrix.GetLength(0))
                .Where(_x => x != _x)
                .Select(
                    _x => matrix[_x, y]
                ).Min()
                ,
                Enumerable.Range(0, matrix.GetLength(1))
                .Where(_y => y != _y)
                .Select(
                    _y => matrix[x, _y]
                ).Min()
            );
        }
        Tuple<float, float> GetMinimumValue(float[,] matrix, Point p) {
            return GetMinimumValue(matrix, p.X, p.Y);
        }
    }
}
