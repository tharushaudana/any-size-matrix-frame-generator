using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Any_Size_Matrix_Frame_Generator___ECLK
{
    public partial class Form2 : Form
    {
        string[] all_frames;
        bool[] frames_status;
        int num_frames = 0;
        int mHeight = 0;
        int mWidth = 0;
        int matrixSize = 0;
        public Form2(string[] frames, bool[] status, int nframes, int h, int w)
        {
            InitializeComponent();
            all_frames = frames;
            frames_status = status;
            num_frames = nframes;
            mHeight = h;
            mWidth = w;
        }

        int XY_MODE_PROGMEM(int y, int x)
        {
            int i;
            i = (y * mWidth) + x;
            return i;
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            matrixSize = mHeight * mWidth;

            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < num_frames; i++)
            {
                if (frames_status[i])
                {
                    string[] tempFrm = new string[matrixSize];
                    for (int ii = 0; ii < matrixSize; ii++)
                    {
                        tempFrm[ii] = all_frames[(matrixSize * i) + ii];
                    }

                    for (int mh = 0; mh < mHeight; mh++)
                    {
                        for (int mw = 0; mw < mWidth; mw++)
                        {
                            sb.Append(tempFrm[XY_MODE_PROGMEM(mh, mw)] + ",");
                        }
                        sb.Append("\r\n");
                    }
                    sb.Append("\r\n");
                }
            }
            textBox1.Text = sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Clipboard.SetText(textBox1.Text);
                MessageBox.Show("Frames copied to clipboard", "Frame Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Frames Not copied to clipboard", "Frame Copy Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
