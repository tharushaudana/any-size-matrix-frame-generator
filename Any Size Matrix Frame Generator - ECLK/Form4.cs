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
    public partial class Form4 : Form
    {
        int WIDTH = 100, HEIGHT = 100;

        int mHeight = 0;
        int mWidth = 0;

        int cx, cy;

        string[] Colors;
        string[] frame;

        public Form4(string[] _Colors)
        {
            InitializeComponent();
            Colors = _Colors;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            setBoxSize();
        }

        private void Form4_Resize(object sender, EventArgs e)
        {
            setBoxSize();
        }

        public void init(int _mHeight, int _mWidth)
        {
            mHeight = _mHeight;
            mWidth = _mWidth;
        }

        public void showFrame(string[] _frame)
        {
            frame = _frame;
            DrawBudhurasmala();
        }

        public void savePng()
        {
            pictureBox1.Image.Save("budurasmala_export.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        void setBoxSize()
        {
            //size of box
            WIDTH = pictureBox1.Width;
            HEIGHT = pictureBox1.Height;

            //center
            cx = WIDTH / 2;
            cy = HEIGHT / 2;

            // draw
            DrawBudhurasmala();
        }

        void DrawBudhurasmala()
        {
            Bitmap bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);
            Graphics g;

            float pos = 0;
            float height = 30;
            int col_index = 0;

            float[] handCoord = new float[2];
            g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);

            for (int j = 0; j < mHeight; j++)
            {
                int reverseJ = (mHeight - 1) - j;
                for (int i = 0; i < mWidth; i++)
                {
                    int reverseI = (mWidth - 1) - i;
                    handCoord = msCoord(pos, height);

                    RectangleF rect = new RectangleF(handCoord[0], handCoord[1], 4, 4);
                    Brush fill = new SolidBrush(ColorTranslator.FromHtml("#" + Colors[Convert.ToInt32(frame[XY_MODE_PROGMEM(reverseJ, reverseI)])]));

                    g.FillRectangle(fill, rect);

                    //pos += 1.875f;
                    pos += 60 / (float)mWidth;
                    col_index++;
                }
                height += 7;
            }

            bmp.RotateFlip(RotateFlipType.Rotate90FlipX);
            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox1.Image = bmp;
        }

        private float[] msCoord(float val, float hlen)
        {
            float[] coord = new float[2];
            val *= 6;   //each minute and second make 6 degree

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (float)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (float)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (float)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (float)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

        int XY_MODE_PROGMEM(int y, int x)
        {
            int i;
            i = (y * mWidth) + x;
            return i;
        }
    }
}
