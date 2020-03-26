using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace B10717028_HW1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void doShifting(int shift)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "*.txt|*.txt|*.*|*.*"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            var sfd = new SaveFileDialog
            {
                Filter = "*.txt|*.txt|*.*|*.*"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            var s = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var os = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.Read);
            while (s.Position < s.Length)
            {
                char c = (char)s.ReadByte();
                if (c >= 'A' && c <= 'Z' ||
                    c >= 'a' && c <= 'z' ||
                    c >= '0' && c <= '9')
                {
                    int cc = (byte)c;
                    cc += shift;
                    if (shift > 0)
                    {
                        if (c >= '0' && c <= '9' && cc > '9')
                        {
                            cc -= 10;
                        }
                        if (c >= 'A' && c <= 'Z' && cc > 'Z')
                        {
                            cc -= 26;
                        }
                        if (c >= 'a' && c <= 'z' && cc > 'z')
                        {
                            cc -= 26;
                        }
                    }
                    else
                    {
                        if (c >= '0' && c <= '9' && cc < '0')
                        {
                            cc += 10;
                        }
                        if (c >= 'A' && c <= 'Z' && cc < 'A')
                        {
                            cc += 26;
                        }
                        if (c >= 'a' && c <= 'z' && cc < 'a')
                        {
                            cc += 26;
                        }
                    }
                    os.WriteByte((byte)cc);
                }
                else os.WriteByte((byte)c);
            }
            os.Close();
            s.Close();
            MessageBox.Show("處理成功。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            doShifting(5);
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            doShifting(-5);
        }
    }
}
