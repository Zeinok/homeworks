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
            None,
            FindLowestBound,
            SubstractLowestBound,
            FindMaximumSum,
            MakeColRowInfinite,
            Finish
        }

        float[,] TSPMatrix;
        float[,] nextTSPMatrix;
        float[] xLowestBound;
        float[] yLowestBound;
        Dictionary<int, int> solution = new Dictionary<int, int>();
        CurrentStatus currentStatus = CurrentStatus.None;
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
            for(int line=0; line<splittedContent.Length; line++) {
                List<float> numbers = splittedContent[line]
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .ConvertAll(
                        (v) => float.Parse(v)
                    );
                int i = 0;
                numbers.ForEach((v) => TSPMatrix[i++, line] = v);
            }
            appendLog("Finished parsing");
            PaintTSPMatrix();
            appendLog("Painted TSP Matrix");
            currentStatus = CurrentStatus.FindLowestBound;
            solution.Clear();
        }

        private void toolStripButtonStep_Click(object sender, EventArgs e) {
            if(TSPMatrix == null) return;
            switch(currentStatus) {
                case CurrentStatus.None:
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
            currentStatus = (CurrentStatus)((int)++currentStatus % (int)CurrentStatus.Finish);
        }

        private void appendLog(string s) {
            richTextBox1.AppendText(string.Format("{0}\n", s));
            richTextBox1.SelectionStart = richTextBox1.TextLength;
        }
        private void PaintTSPMatrix() {
            const int cellSize = 32;
            const float cellSizeF = cellSize;
            Font cellFont = new Font(SystemFonts.DefaultFont.FontFamily, 12);
            if(TSPMatrix == null) return;
            List<int> skippedRow = new List<int>();
            List<int> skippedCol = new List<int>();
/*            for(int x = 0; x < TSPMatrix.GetLength(0); x++) {
                if(Enumerable.Range(0, TSPMatrix.GetLength(1))
                    .Select(
                        (y) => TSPMatrix[x, y] == float.PositiveInfinity
                    ).Count() == TSPMatrix.GetLength(1))
                    skippedCol.Add(x);
            }
            for(int y = 0; y < TSPMatrix.GetLength(1); y++) {
                if(Enumerable.Range(0, TSPMatrix.GetLength(0))
                    .Select(
                        (x) => TSPMatrix[x, y] == float.PositiveInfinity
                    ).Count() == TSPMatrix.GetLength(1))
                    skippedCol.Add(y);
            }*/
            var bmp = new Bitmap(TSPMatrix.GetLength(0) * cellSize + cellSize, TSPMatrix.GetLength(1) * cellSize + cellSize);
            var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            for(int y = 0; y < TSPMatrix.GetLength(1); y++) {
                //if(skippedCol.Contains(y)) continue;
                for(int x = 0; x < TSPMatrix.GetLength(0); x++) {
                    //if(skippedRow.Contains(x)) continue;
                    g.DrawRectangle(Pens.Black, new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize));
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

        private void FindLowestBound() {
            float[,] tempTSPMatrix = (float[,])TSPMatrix.Clone();
            for(int y = 0; y < tempTSPMatrix.GetLength(1); y++) {
                yLowestBound[y] = Enumerable.Range(0, tempTSPMatrix.GetLength(0))
                                    .Select(
                                        (x) => tempTSPMatrix[x, y]
                                    ).Min();
                for(int x = 0; x < tempTSPMatrix.GetLength(0); x++)
                    tempTSPMatrix[x, y] -= yLowestBound[y];
            }

            for(int x = 0; x < tempTSPMatrix.GetLength(0); x++) {
                xLowestBound[x] = Enumerable.Range(0, tempTSPMatrix.GetLength(1))
                                    .Select(
                                        (y) => tempTSPMatrix[x, y]
                                    ).Min();
                for(int y = 0; y < tempTSPMatrix.GetLength(1); y++)
                    tempTSPMatrix[x, y] -= yLowestBound[y];
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
            
            foreach(var t in indexOfZero) {
                values.Add(GetSumOfMinimumValue(TSPMatrix, t));
            }
        }
        private void MakeColRowInfinite() {

        }

        List<Point> GetIndex(float[,] matrix, float value) {
            List<Point> points = new List<Point>();
            for(int y = 0; y < matrix.GetLength(1); y++) 
                for(int x = 0; x < matrix.GetLength(0); x++) 
                    points.Add(new Point(x, y));
            return points;
            
        }
        float GetSumOfMinimumValue(float[,] matrix, int x, int y) {
            return
                Enumerable.Range(0, matrix.GetLength(0))
                                    .Select(
                                        _x => matrix[_x, y]
                ).Where(_x => x != _x).Min()
                +
                Enumerable.Range(0, matrix.GetLength(1))
                                    .Select(
                                        _y => matrix[x, _y]
                ).Where(_y => y != _y).Min();

        }
        float GetSumOfMinimumValue(float[,] matrix, Point p) {
            return GetSumOfMinimumValue(matrix, p.X, p.Y);
        }
}
