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
        private void buttonOpen_Click(object sender, EventArgs e)
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
            while (s.Position<s.Length)
            {
                char c = (char)s.ReadByte();
                if (c >= 'A' && c <= 'Z' ||
                    c >= 'a' && c <= 'z' ||
                    c >= '0' && c <= '9')
                {
                    byte cc = (byte)c;
                    cc += 5;
                    if(c >= '0' && c <= '9' && cc > '9')
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
                    os.WriteByte(cc);
                }
                else os.WriteByte((byte)c);
            }
            os.Close();
            s.Close();
            MessageBox.Show("處理成功。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
