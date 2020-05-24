using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruneAndSearch
{
    public partial class Form1 : Form
    {
        private List<int> elements = new List<int>();
        private int nextK = 0;
        public Form1() {
            InitializeComponent();
        }

        private class State
        {
            public int[] S1;
            public int[] S2;
            public int[] S3;
            public Set SetLeft;
            public bool Found = false;
            public enum Set {
                S1,
                S2,
                S3
            }
            public State(int[] S1, int[] S2, int[] S3, Set SetLeft) {
                this.S1 = S1;
                this.S2 = S2;
                this.S3 = S3;
                this.SetLeft = SetLeft;
                this.Found = false;
            }
            public State(int[] S1, int[] S2, int[] S3, Set SetLeft, bool Found) {
                this.S1 = S1;
                this.S2 = S2;
                this.S3 = S3;
                this.SetLeft = SetLeft;
                this.Found = Found;
            }
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e) {
            try {
                var ofd = new OpenFileDialog();
                if(ofd.ShowDialog() != DialogResult.OK) return;
                elements.Clear();
                var s = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var sr = new StreamReader(s);
                string text = sr.ReadToEnd();
                foreach(var val in text.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)) {
                    elements.Add(int.Parse(val));
                }
                comboBoxK.Enabled = true;
                buttonStep.Enabled = true;
                comboBoxK.Items.Clear();
                comboBoxP.Items.Clear();
                comboBoxPval.Items.Clear();
                for(int i = 0; i < elements.Count; i++) {
                    comboBoxK.Items.Add(i+1);
                    comboBoxP.Items.Add(i+1);
                    comboBoxPval.Items.Add(elements[i]);
                }
                comboBoxK.SelectedIndex = 0;
                comboBoxP.SelectedIndex = 0;
                comboBoxPval.SelectedIndex = 0;
                textBoxLog.Clear();
                StringBuilder sb = new StringBuilder("S = ");
                sb.AppendFormat("{{{0}}}", string.Join(", ", elements.ToArray()));
                appendText(sb.ToString());
            } catch(Exception ex) {
                MessageBox.Show(ex.ToString(), ex.Message);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = true;
            
        }

        private void appendText(string s) {
            textBoxLog.Text += s + "\r\n";
            textBoxLog.SelectionStart = textBoxLog.TextLength;
            textBoxLog.ScrollToCaret();

        }

        private void buttonStep_Click(object sender, EventArgs e) {
            if(comboBoxK.Enabled) {
                nextK = comboBoxK.SelectedIndex;
                comboBoxK.Enabled = false;
            }
            appendText(new string('-', 10));
            State s = pruneAndSearch_Step(nextK, (int)comboBoxPval.SelectedItem, elements);
            appendText(string.Format("K = {0}, P = {2}", comboBoxK.SelectedItem, comboBoxP.SelectedItem));
            appendText(string.Format("S1 = {{{0}}} {1}", string.Join(", ", s.S1), s.SetLeft == State.Set.S1 ? "保留" : "刪除"));
            appendText(string.Format("S2 = {{{0}}} {1}", string.Join(", ", s.S2), s.SetLeft == State.Set.S2 ? "保留" : "刪除"));
            appendText(string.Format("S3 = {{{0}}} {1}", string.Join(", ", s.S3), s.SetLeft == State.Set.S3 ? "保留" : "刪除"));
            comboBoxP.Items.Clear();
            comboBoxPval.Items.Clear();
            for(int i = 0; i < elements.Count; i++) {
                comboBoxP.Items.Add(i + 1);
                comboBoxPval.Items.Add(elements[i]);
            }
            if(s.SetLeft == State.Set.S3) {
                nextK -= s.S1.Length + s.S2.Length;
            }
            if(comboBoxP.Items.Count > 0) {
                comboBoxP.SelectedIndex = 0;
                comboBoxPval.SelectedIndex = 0;
            }
            if(s.Found) {
                appendText(string.Format("已找到第K小的值，K值 = {0}", s.S2[0]));
                buttonStep.Enabled = false;
            }
        }

        private State pruneAndSearch_Step(int k, int pVal, List<int> Set) {
            List<int> lessThan = new List<int>();
            List<int> equals = new List<int>();
            List<int> largerThan = new List<int>();
            foreach(var v in Set) {
                if(v < pVal) lessThan.Add(v);
                if(v == pVal) equals.Add(v);
                if(v > pVal) largerThan.Add(v);
            }
            State s;
            Set.Clear();
            if(lessThan.Count >= k+1) {
                s = new State(lessThan.ToArray(), equals.ToArray(), largerThan.ToArray(), State.Set.S1);
                Set.AddRange(lessThan);
            } else if(lessThan.Count + equals.Count >= k+1) {
                s = new State(lessThan.ToArray(), equals.ToArray(), largerThan.ToArray(), State.Set.S2, true);
            } else {
                s = new State(lessThan.ToArray(), equals.ToArray(), largerThan.ToArray(), State.Set.S3);
                Set.AddRange(largerThan);
            }

            return s;
        }

        private void comboBoxP_SelectedIndexChanged(object sender, EventArgs e) {
            comboBoxPval.SelectedIndex = comboBoxP.SelectedIndex;
        }

        private void comboBoxPval_SelectedIndexChanged(object sender, EventArgs e) {
            comboBoxP.SelectedIndex = comboBoxPval.SelectedIndex;
        }

    }
}
