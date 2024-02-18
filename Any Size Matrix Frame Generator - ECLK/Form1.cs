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

namespace Any_Size_Matrix_Frame_Generator___ECLK
{
    public partial class Form1 : Form
    {
        string[] frame;
        string[] frameTemp;
        string[] frameTempForDisplay;

        string[] all_frames;
        bool[] frames_status;
        int num_frames = 0;
        int deleted_frames = 0;
        int selected_frame = 0;
        string filepath = null;

        int colorSectionLength = 21;

        string[] Colors = {
            "000000",
            "ffffff", "e3e3e3", "c8c8c8", "aeaeae", "959595", "7c7c7c", "646464", "4d4d4d", "373737", "222222", "101010", "e3e3e3", "c8c8c8", "aeaeae", "959595", "7c7c7c", "646464", "4d4d4d", "373737", "222222", "101010",
            "ff0000", "e30000", "c80000", "ae0000", "950000", "7c0000", "640000", "4d0000", "370000", "220000", "100000", "ff1b1b", "ff3636", "ff5050", "ff6969", "ff8282", "ff9a9a", "ffb1b1", "ffc7c7", "ffdcdc", "ffeeee",
            "00ff00", "00e300", "00c800", "00ae00", "009500", "007c00", "006400", "004d00", "003700", "002200", "001000", "1bff1b", "36ff36", "50ff50", "69ff69", "82ff82", "9aff9a", "b1ffb1", "c7ffc7", "dcffdc", "eeffee",
            "0000ff", "0000e3", "0000c8", "0000ae", "000095", "00007c", "000064", "00004d", "000037", "000022", "000010", "1b1bff", "3636ff", "5050ff", "6969ff", "8282ff", "9a9aff", "b1b1ff", "c7c7ff", "dcdcff", "eeeeff",
            "ffff00", "e3e300", "c8c800", "aeae00", "959500", "7c7c00", "646400", "4d4d00", "373700", "222200", "101000", "ffff1b", "ffff36", "ffff50", "ffff69", "ffff82", "ffff9a", "ffffb1", "ffffc7", "ffffdc", "ffffee",
            "00ffff", "00e3e3", "00c8c8", "00aeae", "009595", "007c7c", "006464", "004d4d", "003737", "002222", "001010", "1bffff", "36ffff", "50ffff", "69ffff", "82ffff", "9affff", "b1ffff", "c7ffff", "dcffff", "eeffff",
            "ff8800", "e37900", "c86b00", "ae5d00", "954f00", "7c4200", "643500", "4d2900", "371d00", "221200", "100800", "ffa31b", "ffbe36", "ffd850", "fff169", "ffff82", "ffff9a", "ffffb1", "ffffc7", "ffffdc", "ffffee",
            "ff00ff", "e300e3", "c800c8", "ae00ae", "950095", "7c007c", "640064", "4d004d", "370037", "220022", "100010", "ff1bff", "ff36ff", "ff50ff", "ff69ff", "ff82ff", "ff9aff", "ffb1ff", "ffc7ff", "ffdcff", "ffeeff",
            "a349a4", "914192", "803981", "6f3270", "5f2a5f", "4f2350", "401c40", "311631", "230f23", "160916", "0a040a", "be64bf", "d97fda", "f399f4", "ffb2ff", "ffcbff", "ffe3ff", "fffaff", "ffffff", "ffffff", "ffffff",
            "008000", "007200", "006400", "005700", "004a00", "003e00", "003200", "002600", "001b00", "001100", "000800", "1b9b1b", "36b636", "50d050", "69e969", "82ff82", "9aff9a", "b1ffb1", "c7ffc7", "dcffdc", "eeffee",
            "ff0080", "e30072", "c80064", "ae0057", "95004a", "7c003e", "640032", "4d0026", "37001b", "220011", "100008", "ff1b9b", "ff36b6", "ff50d0", "ff69e9", "ff82ff", "ff9aff", "ffb1ff", "ffc7ff", "ffdcff", "ffeeff",
            "800040", "720039", "640032", "57002b", "4a0025", "3e001f", "320019", "260013", "1b000d", "110008", "080004", "9b1b5b", "b63676", "d05090", "e969a9", "ff82c2", "ff9ada", "ffb1f1", "ffc7ff", "ffdcff", "ffeeff",
        };

        string selectedColor = "000000";
        string pickedColor = "000000";
        int selectedColorIndex = 0;
        int pickedColorIndex = 0;

        int[,] rectangle_data;
        int rectSize = 10;
        int num_of_rects = 0;
        int mHeight = 0;
        int mWidth = 0;
        int matrixSize = 0;

        string drawFrom = "";
        string drawTo = "";
        bool isNotDrawing = true;

        string dnarr;
        string uparr;
        string ltarr;
        string rtarr;

        PixelGraphics graphics;

        Form4 previewForm = null;
        public Form1()
        {
            InitializeComponent();
            //MaximizeBox = false;
        }

        /************************************** Draw Functions ***********************************/
        void DrawMatrix(int height, int width)
        {
            Bitmap img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);

            int Xindex = 0;
            int Yindex = 0;
            num_of_rects = 0;
            for (int w = 0; w < height; w++)
            {
                for (int h = 0; h < width; h++)
                {
                    Brush brush = new SolidBrush(ColorTranslator.FromHtml("#" + Colors[Convert.ToInt32(frame[XY_MODE_PROGMEM(w, h)])]));
                    Rectangle rect = new Rectangle(Xindex, Yindex, rectSize, rectSize);
                    g.FillRectangle(brush, rect);
                    rectangle_data[num_of_rects, 0] = Xindex;
                    rectangle_data[num_of_rects, 1] = Yindex;
                    rectangle_data[num_of_rects, 2] = rectSize;
                    rectangle_data[num_of_rects, 3] = rectSize;
                    num_of_rects++;
                    Xindex += rectSize + 1;
                }
                Xindex = 0;  // reset x position for new line
                Yindex += rectSize + 1;
            }

            pictureBox1.Image = img;
        }

        void DrawMatrixTemp(int height, int width)
        {
            Bitmap img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);

            int Xindex = 0;
            int Yindex = 0;
            num_of_rects = 0;
            for (int w = 0; w < height; w++)
            {
                for (int h = 0; h < width; h++)
                {
                    Brush brush = new SolidBrush(ColorTranslator.FromHtml("#" + Colors[Convert.ToInt32(frameTempForDisplay[XY_MODE_PROGMEM(w, h)])]));
                    Rectangle rect = new Rectangle(Xindex, Yindex, rectSize, rectSize);
                    g.FillRectangle(brush, rect);
                    rectangle_data[num_of_rects, 0] = Xindex;
                    rectangle_data[num_of_rects, 1] = Yindex;
                    rectangle_data[num_of_rects, 2] = rectSize;
                    rectangle_data[num_of_rects, 3] = rectSize;
                    num_of_rects++;
                    Xindex += rectSize + 1;
                }
                Xindex = 0;  // reset x position for new line
                Yindex += rectSize + 1;
            }

            pictureBox1.Image = img;
        }
        /**********************************************************************************************/

        void DrawBudhurasmala()
        {
            if (previewForm != null)
            {
                previewForm.showFrame(frame);
            }
        }

        /*int GetRectPosition(int x, int y)
        {
            int matching_rect = -1;
            for (int i = 0; i < num_of_rects; i++)
            {
                int rectX = rectangle_data[i, 0];  // get rectangle X position from array
                int rectY = rectangle_data[i, 1];  // get rectangle Y position from array
                int rectW = rectangle_data[i, 2];  // get rectangle WIDTH      from array
                int rectH = rectangle_data[i, 3];  // get rectangle HEIGHT     from array
                if (find_clicked_square(rectX, rectY, rectW, rectH, x, y) == true)
                {
                    matching_rect = i;
                    i = num_of_rects + 1;  // For stop the loop
                }
            }
            return matching_rect;
        }*/

        bool find_clicked_square(int x, int y, int width, int height, int currentLocX, int currentLocY)
        {
            bool result;
            Rectangle find = new Rectangle(x, y, width, height);
            result = find.Contains(new Point(currentLocX, currentLocY));

            return result;
        }

        Button GenerateColorBtn(int n, int size = 30, float fsize = 13f)
        {
            Button b = new Button();
            b.Name = "cbtn" + n.ToString();
            b.ForeColor = Color.White;
            b.Font = new Font(b.Font.FontFamily, fsize, FontStyle.Bold);
            b.BackColor = ColorTranslator.FromHtml("#" + Colors[n]);
            b.FlatStyle = FlatStyle.Popup;
            b.Width = size;
            b.Height = size;
            b.Margin = new Padding(1);

            return b;
        }

        void DisplayFrame()
        {
            int index = 0;
            string output = "";
            for(int i = 0; i < mHeight; i++)
            {
                string temp = "";
                for(int j = 0; j < mWidth; j++)
                {
                    temp += frame[XY_MODE_PROGMEM(i, j)] + ",";
                }
                output += temp + "\r\n";
            }
            textBox1.Text = output;
        }

        void line(int x, int y, int x2, int y2)
        {
            //string points = "";
            //frameTempClear();

            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                //putpixel(x, y, color);
                frameTemp[XY_MODE_PROGMEM(y, x)] = selectedColorIndex.ToString();

                //points += x.ToString() + ", " + y.ToString() + "\r\n";

                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

        void rectangle(int x1, int y1, int x2, int y2)
        {
            line(x1, y1, x2, y1);
            line(x2, y1, x2, y2);
            line(x2, y2, x1, y2);
            line(x1, y2, x1, y1);
        }

        void frameTempClear()
        {
            for(int i = 0; i < matrixSize; i++)
            {
                frameTemp[i] = "0";
                frameTempForDisplay[i] = "0";
            }
        }

        void combinedFrames()
        {
            //First layer
            for (int i = 0; i < matrixSize; i++)
            {
                frameTempForDisplay[i] = frame[i];
            }
            //Second layer
            for(int i = 0; i < matrixSize; i++)
            {
                if (frameTemp[i] != "0")
                {
                    frameTempForDisplay[i] = frameTemp[i];
                }
            }
        }

        void line_move_left(int linenum, int Start, int Stop)
        {
            for (int i = Start; i < Stop + 1; i++)
            {
                int index = i + 1;
                if (index > Stop)
                {
                    index = 0;
                }
                if (i == Start)
                {
                    dnarr = frame[XY_MODE_PROGMEM(linenum, i)];
                }
                frame[XY_MODE_PROGMEM(linenum, i)] = frame[XY_MODE_PROGMEM(linenum, index)];
                if (i == Stop)
                {
                    frame[XY_MODE_PROGMEM(linenum, i)] = dnarr;
                }
            }
        }

        void line_move_right(int linenum, int Start, int Stop)
        {
            for (int i = Stop; i >= Start; i--)
            {
                int index = i - 1;
                if (index > Stop)
                {
                    index = 0;
                }
                if(index < Start)
                {
                    index = Stop;
                }
                if (i == Stop)
                {
                    uparr = frame[XY_MODE_PROGMEM(linenum, i)];
                }
                frame[XY_MODE_PROGMEM(linenum, i)] = frame[XY_MODE_PROGMEM(linenum, index)];
                if (i == Start)
                {
                    frame[XY_MODE_PROGMEM(linenum, i)] = uparr;
                }
            }
        }

        void line_move_up(int linenum, int Start, int Stop)
        {
            for (int i = Start; i < Stop + 1; i++)
            {
                int index = i + 1;
                if (index > Stop)
                {
                    index = 0;
                }
                if (i == Start)
                {
                    ltarr = frame[XY_MODE_PROGMEM(i, linenum)];
                }
                frame[XY_MODE_PROGMEM(i, linenum)] = frame[XY_MODE_PROGMEM(index, linenum)];
                if (i == Stop)
                {
                    frame[XY_MODE_PROGMEM(i, linenum)] = ltarr;
                }
            }
        }

        void line_move_down(int linenum, int Start, int Stop)
        {
            for (int i = Stop; i >= Start; i--)
            {
                int index = i - 1;
                if (index > Stop)
                {
                    index = 0;
                }
                if (index < Start)
                {
                    index = Stop;
                }
                if (i == Stop)
                {
                    rtarr = frame[XY_MODE_PROGMEM(i, linenum)];
                }
                frame[XY_MODE_PROGMEM(i, linenum)] = frame[XY_MODE_PROGMEM(index, linenum)];
                if (i == Start)
                {
                    frame[XY_MODE_PROGMEM(i, linenum)] = rtarr;
                }
            }
        }

        void left_all()
        {
            for (int i = 0; i < mHeight; i++)
            {
                line_move_left(i, 0, mWidth - 1);
            }
        }

        void right_all()
        {
            for (int i = 0; i < mHeight; i++)
            {
                line_move_right(i, 0, mWidth - 1);
            }
        }

        void up_all()
        {
            for (byte i = 0; i < mWidth; i++)
            {
                line_move_up(i, 0, mHeight - 1);
            }
        }

        void down_all()
        {
            for (byte i = 0; i < mWidth; i++)
            {
                line_move_down(i, 0, mHeight - 1);
            }
        }

        int XY_MODE_PROGMEM(int y, int x)
        {
            int i;
            i = (y * mWidth) + x;
            return i;
        }

        Color invertOfColor(int index)
        {
            Color color = System.Drawing.ColorTranslator.FromHtml("#" + Colors[Convert.ToInt32(index)]);
            return Color.FromArgb(color.ToArgb() ^ 0xffffff);
        }

        void setSelectedColor(int index)
        {
            selectedColor = Colors[Convert.ToInt32(index)];
            selectedColorIndex = Convert.ToInt32(index);
            panel1.BackColor = ColorTranslator.FromHtml("#" + Colors[Convert.ToInt32(index)]);

            label3.ForeColor = invertOfColor(index);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mHeight = Convert.ToInt32(numericUpDown1.Value);
            mWidth = Convert.ToInt32(numericUpDown2.Value);
            matrixSize = mHeight * mWidth;
            label8.Text = mHeight.ToString() + "x" + mWidth.ToString();
            label9.Text = matrixSize.ToString() + " LEDs";

            frame = new string[matrixSize];
            frameTemp = new string[matrixSize];
            frameTempForDisplay = new string[matrixSize];
            
            for (int i = 0; i < matrixSize; i++)
            {
                frame[i] = "0";
                frameTemp[i] = "0";
                frameTempForDisplay[i] = "0";
            }

            previewForm.init(mHeight, mWidth);

            DrawMatrix(mHeight, mWidth);
            DrawBudhurasmala();
            DisplayFrame();

            button1.Enabled = false;
            button11.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rectangle_data = new int[5000, 4];
            label4.Text = "0";
            label5.Text = "Y: 0, X: 0";
            label6.Text = "... - ...";

            // Black button
            Button black_btn = GenerateColorBtn(0);
            flowLayoutPanel1.Controls.Add(black_btn);
            black_btn.MouseClick += B_MouseClick;

            for (int i = 1; i < Colors.Length; i += colorSectionLength)
            {
                Button b = GenerateColorBtn(i);
                flowLayoutPanel1.Controls.Add(b);
                b.MouseClick += B_MouseClick;
            }

            all_frames = new string[1000000];
            frames_status = new bool[1000000];

            graphics = new PixelGraphics();

            button11.Enabled = false;

            previewForm = new Form4(Colors);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            DrawMatrix(mHeight, mWidth);
        }

        Button previous_selected_color_btn = null;

        private void B_MouseClick(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            string btnIndex = b.Name.Substring(4);
            setSelectedColor(Convert.ToInt32(btnIndex));

            // Indicate not selected previous btn
            if (previous_selected_color_btn != null)
            {
                previous_selected_color_btn.Text = "";
            }

            // Indicate selected
            b.Text = "+";
            b.ForeColor = invertOfColor(selectedColorIndex);
            previous_selected_color_btn = b;

            // Generate fade color btns
            flowLayoutPanel2.Controls.Clear();

            if (selectedColorIndex == 0) return; // because no fades for black color

            for (int i = selectedColorIndex + 1; i < selectedColorIndex + colorSectionLength; i++)
            {
                Button fade_btn = GenerateColorBtn(i, 20, 8f);
                flowLayoutPanel2.Controls.Add(fade_btn);
                fade_btn.MouseClick += Fade_btn_MouseClick;
            }
        }

        private void Fade_btn_MouseClick(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            string btnIndex = b.Name.Substring(4);
            setSelectedColor(Convert.ToInt32(btnIndex));

            // Indicate not selected previous btn
            if (previous_selected_color_btn != null)
            {
                previous_selected_color_btn.Text = "";
            }

            // Indicate selected
            b.Text = "+";
            b.ForeColor = invertOfColor(selectedColorIndex);
            previous_selected_color_btn = b;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(radioButton2.Checked)
            {
                int pos = graphics.GetRectPosition(e.X, e.Y, num_of_rects, rectangle_data);

                if (checkBox1.Checked == false)
                {
                    if (pos >= 0)
                    {
                        frame[pos] = selectedColorIndex.ToString();
                        DrawMatrix(mHeight, mWidth);
                        DrawBudhurasmala();
                        DisplayFrame();
                    }
                }
                else
                {
                    if (pos >= 0)
                    {
                        pickedColorIndex = Convert.ToInt32(frame[pos]);
                        pickedColor = Colors[pickedColorIndex];
                        panel3.BackColor = ColorTranslator.FromHtml("#" + pickedColor);

                        Color color = System.Drawing.ColorTranslator.FromHtml("#" + pickedColor);
                        label7.ForeColor = Color.FromArgb(color.ToArgb() ^ 0xffffff);
                    }
                }
            }
        }

        int Xcen, Ycen, rad = 0;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int pos = graphics.GetRectPosition(e.X, e.Y, num_of_rects, rectangle_data);
            int posX = 0;
            int posY = 0;

            if (radioButton2.Checked)
            {
                if (ModifierKeys.HasFlag(Keys.Control) || Control.MouseButtons == MouseButtons.Left)
                {
                    if (pos >= 0)
                    {
                        frame[pos] = selectedColorIndex.ToString();
                        DrawMatrix(mHeight, mWidth);
                        //DrawBudhurasmala();
                        DisplayFrame();

                        if (isNotDrawing)
                        {
                            drawFrom = (pos + 1).ToString();
                            isNotDrawing = false;
                        }
                        else
                        {
                            drawTo = (pos + 1).ToString();
                        }
                        label6.Text = drawFrom + " - " + drawTo;
                    }
                }
                else
                {
                    DrawBudhurasmala();
                    isNotDrawing = true;
                    label6.Text = "... - ...";
                }

                if (pos >= 0)
                {
                    for (int i = 0; i < mHeight; i++)
                    {
                        for (int j = 0; j < mWidth; j++)
                        {
                            if (XY_MODE_PROGMEM(i, j) == pos)
                            {
                                posX = j;
                                posY = i;
                            }
                        }
                    }
                    label4.Text = (pos + 1).ToString();
                    label5.Text = "Y: " + (posY + 1).ToString() + ", X: " + (posX + 1).ToString();
                }
            }

            if (radioButton1.Checked)
            {
                if (pos >= 0)
                {
                    for (int i = 0; i < mHeight; i++)
                    {
                        for (int j = 0; j < mWidth; j++)
                        {
                            if (XY_MODE_PROGMEM(i, j) == pos)
                            {
                                posX = j;
                                posY = i;
                            }
                        }
                    }
                    label4.Text = (pos + 1).ToString();
                    label5.Text = "Y: " + (posY + 1).ToString() + ", X: " + (posX + 1).ToString();

                    if (Control.MouseButtons == MouseButtons.Left)
                    {
                        int x = posX;
                        int y = posY;

                        /*frameTempClear();
                        line(Xcen, Ycen, x, y);
                        combinedFrames();
                        DrawMatrixTemp(mHeight, mWidth);*/
                        frameTemp = graphics.PixelDrawLine(Xcen, Ycen, x, y, matrixSize, selectedColorIndex, mWidth);
                        combinedFrames();
                        DrawMatrixTemp(mHeight, mWidth);
                    }
                    else
                    {
                        Xcen = posX;
                        Ycen = posY;
                    }
                }
            }

            if (radioButton3.Checked)
            {
                if (pos >= 0)
                {
                    for (int i = 0; i < mHeight; i++)
                    {
                        for (int j = 0; j < mWidth; j++)
                        {
                            if (XY_MODE_PROGMEM(i, j) == pos)
                            {
                                posX = j;
                                posY = i;
                            }
                        }
                    }
                    label4.Text = (pos + 1).ToString();
                    label5.Text = "Y: " + (posY + 1).ToString() + ", X: " + (posX + 1).ToString();

                    if (Control.MouseButtons == MouseButtons.Left)
                    {
                        int x = posX;
                        int y = posY;

                        /*frameTempClear();
                        rectangle(Xcen, Ycen, x, y);
                        combinedFrames();
                        DrawMatrixTemp(mHeight, mWidth);*/
                        frameTemp = graphics.PixelDrawRectangle(Xcen, Ycen, x, y, matrixSize, selectedColorIndex, mWidth);
                        combinedFrames();
                        DrawMatrixTemp(mHeight, mWidth);
                    }
                    else
                    {
                        Xcen = posX;
                        Ycen = posY;
                    }
                }
                /*if (pos >= 0)
                {
                    for (int i = 0; i < mHeight; i++)
                    {
                        for (int j = 0; j < mWidth; j++)
                        {
                            if (XY_MODE_PROGMEM(i, j) == pos)
                            {
                                posX = j;
                                posY = i;
                            }
                        }
                    }

                    if (Control.MouseButtons == MouseButtons.Left)
                    {
                        int x = posX;
                        rad = x - Xcen;

                        frameTempClear();
                        circle(Xcen, Ycen, rad);
                        combinedFrames();
                        DrawMatrixTemp(mHeight, mWidth);
                    }
                    else
                    {
                        Xcen = posX;
                        Ycen = posY;
                    }
                }*/
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Clipboard.SetText(textBox1.Text);
                MessageBox.Show("Frame copied to clipboard", "Frame Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Frame Not copied to clipboard", "Frame Copy Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            left_all();
            DrawMatrix(mHeight, mWidth);
            DrawBudhurasmala();
            DisplayFrame();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            right_all();
            DrawMatrix(mHeight, mWidth);
            DrawBudhurasmala();
            DisplayFrame();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            up_all();
            DrawMatrix(mHeight, mWidth);
            DrawBudhurasmala();
            DisplayFrame();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            down_all();
            DrawMatrix(mHeight, mWidth);
            DrawBudhurasmala();
            DisplayFrame();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("matrix_export.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (previewForm != null)
            {
                previewForm.savePng();
            }
        }

        //all_frames code stared 2020-08-04 8:16AM ------------------
        private void button11_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Add("dfd");
            //listBox1.Items.Remove(listBox1.SelectedIndex);
            try
            {
                int j = 0;
                for(int i = num_frames * matrixSize; i < (num_frames * matrixSize) + matrixSize; i++)
                {
                    all_frames[i] = frame[j];
                    j++;
                }
                frames_status[num_frames] = true;

                num_frames++;
                string text = "Frame : " + num_frames.ToString();
                listBox1.Items.Add(text);
                label12.Text = "Total Size : " + (num_frames * matrixSize).ToString() + " bytes (" + ((num_frames * matrixSize) / 1024).ToString() + "KB) | Address Count : " + ((num_frames * matrixSize) - 16).ToString("X8");
            }
            catch
            {
                MessageBox.Show("Frame was not added!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ReloadFrameListBox()
        {
            listBox1.Items.Clear();

            for(int i = 0; i < num_frames; i++)
            {
                if (frames_status[i])
                {
                    string text = "Frame : " + (i + 1).ToString();
                    listBox1.Items.Add(text);
                }
                else
                {
                    string text = "Frame : " + (i + 1).ToString() + " (Deleted)";
                    listBox1.Items.Add(text);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = listBox1.SelectedIndex;
                selected_frame = index;

                if (frames_status[selected_frame])
                {
                    for (int i = 0; i < matrixSize; i++)
                    {
                        frame[i] = all_frames[(matrixSize * index) + i];
                    }
                    DrawMatrix(mHeight, mWidth);
                    DrawBudhurasmala();
                    DisplayFrame();
                }
                else
                {
                    for(int i = 0; i < matrixSize; i++)
                    {
                        frame[i] = "0";
                    }
                    DrawMatrix(mHeight, mWidth);
                    DrawBudhurasmala();
                    DisplayFrame();
                    MessageBox.Show("This frame is not available! You can restore it.", "Frame is deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("There are some error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void replaceFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if(num_frames > 0)
                {
                    if (frames_status[selected_frame])
                    {
                        for (int i = 0; i < matrixSize; i++)
                        {
                            all_frames[(matrixSize * selected_frame) + i] = frame[i];
                        }
                        MessageBox.Show("Frame " + (selected_frame + 1).ToString() + " Replaced", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("This frame is not available! You can restore it.", "Frame is deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Frames not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Frame not replaced!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frames_status[selected_frame] = false;
            deleted_frames++;
            ReloadFrameListBox();
            label12.Text = "Total Size : " + ((num_frames - deleted_frames) * matrixSize).ToString() + " bytes (" + (((num_frames - deleted_frames) * matrixSize) / 1024).ToString() + "KB) | Address Count : " + (((num_frames - deleted_frames) * matrixSize) - 16).ToString("X8");
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            all_frames = new string[1000000];
            num_frames = 0;
            selected_frame = 0;
            filepath = null;
            for(int i = 0; i < matrixSize; i++)
            {
                frame[i] = "0";
            }
            if(pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            //DrawMatrix(mHeight, mWidth);
            DrawBudhurasmala();
            DisplayFrame();

            listBox1.Items.Clear();
            button1.Enabled = true;
            button11.Enabled = false;
            this.Text = "Any Size Frame Generator - ECLK ...[NEW]";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(num_frames > 0)
            {
                Form2 form2 = new Form2(all_frames, frames_status, num_frames, mHeight, mWidth);
                form2.Show();
            }
            else
            {
                MessageBox.Show("Frames not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void SaveProject()
        {
            try
            {
                string fileName = filepath;
                StreamWriter sw = new StreamWriter(fileName);
                sw.Write("H" + mHeight.ToString() + "\r\n");
                sw.Write("W" + mWidth.ToString() + "\r\n");
                sw.Write("N" + num_frames.ToString() + "\r\n");
                sw.Write("F");
                for (int i = 0; i < num_frames * matrixSize; i++)
                {
                    sw.Write(all_frames[i] + ",");
                }
                sw.Close();

                filepath = fileName;
                this.Text = "Any Size Frame Generator - ECLK | " + filepath;
                MessageBox.Show("project saved in exiting location", "Project Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Not saved!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void SaveAsProject()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Any Size Matrix Frame Generator | Save Project";
                sfd.Filter = "ASMFG File (*.asmfg)|*.asmfg";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    StreamWriter sw = new StreamWriter(fileName);
                    sw.Write("H" + mHeight.ToString() + "\r\n");
                    sw.Write("W" + mWidth.ToString() + "\r\n");
                    sw.Write("N" + (num_frames - deleted_frames).ToString() + "\r\n");
                    sw.Write("F");

                    /*for (int i = 0; i < num_frames * matrixSize; i++)
                    {
                        sw.Write(all_frames[i] + ",");
                    }*/

                    for(int i = 0; i < num_frames; i++)
                    {
                        if (frames_status[i])
                        {
                            for(int j = 0; j < matrixSize; j++)
                            {
                                sw.Write(all_frames[(i * matrixSize) + j] + ",");
                            }
                        }
                    }

                    sw.Close();

                    filepath = fileName;
                    this.Text = "Any Size Frame Generator - ECLK | " + filepath;
                    MessageBox.Show("project saved in : " + fileName, "Project Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Not saved!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void OpenProject()
        {
            try
            {
                all_frames = new string[1000000];
                frames_status = new bool[1000000];
                deleted_frames = 0;

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "ASMFG File (*.asmfg)|*.asmfg";
                ofd.Title = "Any Size Matrix Frame Generator | Open Project";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filename = ofd.FileName;
                    using (StreamReader sr = File.OpenText(filename))
                    {
                        string s = String.Empty;
                        int i = 0;
                        while ((s = sr.ReadLine()) != null)
                        {
                            if(s.Substring(0, 1) == "H")
                            {
                                mHeight = Convert.ToInt32(s.Substring(1));
                            }
                            else if (s.Substring(0, 1) == "W")
                            {
                                mWidth = Convert.ToInt32(s.Substring(1));
                            }
                            else if (s.Substring(0, 1) == "N")
                            {
                                num_frames = Convert.ToInt32(s.Substring(1));
                            }
                            else if (s.Substring(0, 1) == "F")
                            {
                                string[] temp_all_frames = s.Substring(1).Split(',');
                                for(int index = 0; index < temp_all_frames.Length; index++)
                                {
                                    all_frames[index] = temp_all_frames[index];
                                }
                            }
                        }
                    }
                    matrixSize = mHeight * mWidth;
                    filepath = filename;
                    this.Text = "Any Size Frame Generator - ECLK | " + filepath;
                }

                // add items to listbox
                listBox1.Items.Clear();
                for(int i = 0; i < num_frames; i++)
                {
                    string text = "Frame : " + (i + 1).ToString();
                    frames_status[i] = true;
                    listBox1.Items.Add(text);
                }

                frame = new string[matrixSize];
                frameTemp = new string[matrixSize];
                frameTempForDisplay = new string[matrixSize];

                for (int i = 0; i < matrixSize; i++)
                {
                    frameTemp[i] = "0";
                    frameTempForDisplay[i] = "0";
                }

                // display first frame
                for (int i = 0; i < matrixSize; i++)
                {
                    frame[i] = all_frames[i];
                }
                DrawMatrix(mHeight, mWidth);
                DrawBudhurasmala();
                DisplayFrame();

                button1.Enabled = false;
                button11.Enabled = true;
                label8.Text = mHeight.ToString() + "x" + mWidth.ToString();
                label9.Text = matrixSize.ToString() + " LEDs";
                label12.Text = "Total Size : " + (num_frames * matrixSize).ToString() + " bytes (" + ((num_frames * matrixSize) / 1024).ToString() + "KB) | Address Count : " + ((num_frames * matrixSize) - 16).ToString("X8");
            }
            catch
            {
                MessageBox.Show("Not saved!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ExportAsText()
        {
            try
            {
                // GENERATE TEXT
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < num_frames; i++)
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

                // SAVE TEXT
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Any Size Matrix Frame Generator | Export Project [Text]";
                sfd.Filter = "Text File (*.txt)|*.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    StreamWriter sw = new StreamWriter(fileName);
                    sw.Write(sb.ToString());
                    sw.Close();

                    MessageBox.Show("project exported in : " + fileName, "Project Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Not exported!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        void ExportAsBin()
        {
            try
            {
                // GENERATE HEX ARRAY
                StringBuilder sb = new StringBuilder();

                /*for (int i = 0; i < num_frames * matrixSize; i++)
                {
                    byte b = Convert.ToByte(all_frames[i]);
                    string hexval = b.ToString("X2");
                    sb.Append(hexval);
                }*/

                for(int i = 0; i < num_frames; i++)
                {
                    if (frames_status[i])
                    {
                        for(int j = 0; j < matrixSize; j++)
                        {
                            byte b = Convert.ToByte(all_frames[(i * matrixSize) + j]);
                            string hexval = b.ToString("X2");
                            sb.Append(hexval);
                        }
                    }
                }

                byte[] buffer = StringToByteArray(sb.ToString());

                // SAVE AS BIN FILE
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Any Size Matrix Frame Generator | Export Project [Bin]";
                sfd.Filter = "Bin File (*.bin)|*.bin";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string filename = sfd.FileName;
                    FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(buffer);
                    bw.Close();
                    fs.Close();

                    MessageBox.Show("project .Bin file saved in (" + filename + ")", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Not exported!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(filepath != null)
            {
                SaveProject();
            }
            else
            {
                SaveAsProject();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsProject();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        private void textFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportAsText();
        }

        private void bINFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportAsBin();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        /*########################### MIRROR FUNCTIONS ###############################*/

        string[] clearMainFrmAndTransferValues()
        {
            string[] tempFrm = new string[matrixSize];

            for (int i = 0; i < matrixSize; i++)
            {
                tempFrm[i] = frame[i];
                frame[i] = "0";
                frameTemp[i] = "0";
                frameTempForDisplay[i] = "0";
            }

            return tempFrm;
        }

        //-----------------------
        void Frame_Xmirror()
        {
            string[] tempFrm = clearMainFrmAndTransferValues();

            for (int i = 0; i < mHeight; i++)
            {
                for (int j = 0; j < mWidth; j++)
                {
                    int reverseJ = (mWidth - 1) - j;
                    int reverseI = (mHeight - 1) - i;
                    frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(i, reverseJ)];
                }
            }

            DrawMatrix(mHeight, mWidth);
            DrawBudhurasmala();
            DisplayFrame();
        }
        void Frame_Ymirror()
        {
            string[] tempFrm = clearMainFrmAndTransferValues();

            for (int i = 0; i < mHeight; i++)
            {
                for (int j = 0; j < mWidth; j++)
                {
                    int reverseJ = (mWidth - 1) - j;
                    int reverseI = (mHeight - 1) - i;
                    frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(reverseI, j)];
                }
            }

            DrawMatrix(mHeight, mWidth);
            DrawBudhurasmala();
            DisplayFrame();
        }

        void Frame_LeftHalf_Xmirror()
        {
            if(mWidth % 2 == 0)
            {
                string[] tempFrm = clearMainFrmAndTransferValues();

                for (int i = 0; i < mHeight; i++)
                {
                    for (int j = 0; j < mWidth / 2; j++)
                    {
                        int reverseJ = (mWidth / 2 - 1) - j;
                        int reverseI = (mHeight - 1) - i;
                        frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(i, reverseJ)];
                    }
                    for (int j = mWidth / 2; j < mWidth; j++)
                    {
                        frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(i, j)];
                    }
                }

                DrawMatrix(mHeight, mWidth);
                DrawBudhurasmala();
                DisplayFrame();
            }
            else
            {
                MessageBox.Show("Matrix size not support for this function", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Frame_RightHalf_Xmirror()
        {
            if (mWidth % 2 == 0)
            {
                string[] tempFrm = clearMainFrmAndTransferValues();

                for (int i = 0; i < mHeight; i++)
                {
                    for (int j = mWidth / 2; j < mWidth; j++)
                    {
                        int reverseJ = ((mWidth - 1) + (mWidth / 2)) - j;
                        int reverseI = (mHeight - 1) - i;
                        frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(i, reverseJ)];
                    }
                    for (int j = 0; j < mWidth / 2; j++)
                    {
                        frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(i, j)];
                    }
                }

                DrawMatrix(mHeight, mWidth);
                DrawBudhurasmala();
                DisplayFrame();
            }
            else
            {
                MessageBox.Show("Matrix size not support for this function", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Frame_LeftHalf_Ymirror()
        {
            if (mWidth % 2 == 0)
            {
                string[] tempFrm = clearMainFrmAndTransferValues();

                for (int i = 0; i < mHeight; i++)
                {
                    for (int j = 0; j < mWidth / 2; j++)
                    {
                        int reverseJ = (mWidth / 2 - 1) - j;
                        int reverseI = (mHeight - 1) - i;
                        frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(reverseI, j)];
                    }
                    for (int j = mWidth / 2; j < mWidth; j++)
                    {
                        frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(i, j)];
                    }
                }

                DrawMatrix(mHeight, mWidth);
                DrawBudhurasmala();
                DisplayFrame();
            }
            else
            {
                MessageBox.Show("Matrix size not support for this function", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Frame_RightHalf_Ymirror()
        {
            if (mWidth % 2 == 0)
            {
                string[] tempFrm = clearMainFrmAndTransferValues();

                for (int i = 0; i < mHeight; i++)
                {
                    for (int j = mWidth / 2; j < mWidth; j++)
                    {
                        int reverseJ = (mWidth - 1) - j;
                        int reverseI = (mHeight - 1) - i;
                        frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(reverseI, j)];
                    }
                    for (int j = 0; j < mWidth / 2; j++)
                    {
                        frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(i, j)];
                    }
                }

                DrawMatrix(mHeight, mWidth);
                DrawBudhurasmala();
                DisplayFrame();
            }
            else
            {
                MessageBox.Show("Matrix size not support for this function", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*############################################################################*/

        /*######################### FILL WITH FIRST BLOCK FUNCS ######################*/

        void FillFirstBlock4X()
        {
            if (mWidth % 4 == 0)
            {
                string[] tempFrm = clearMainFrmAndTransferValues();

                for (int i = 0; i < mWidth / 4; i++)
                {
                    for (int j = 0; j < mHeight; j++)
                    {
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 4) * 0) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 4) * 1) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 4) * 2) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 4) * 3) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                    }
                }

                DrawMatrix(mHeight, mWidth);
                DrawBudhurasmala();
                DisplayFrame();
            }
            else
            {
                MessageBox.Show("Matrix size not support for this function", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void FillFirstBlock4X_Reverse()
        {
            if (mWidth % 4 == 0)
            {
                string[] tempFrm = clearMainFrmAndTransferValues();

                for (int i = 0; i < mWidth / 4; i++)
                {
                    int reverseI = ((mWidth / 4) - 1) - i;

                    for (int j = 0; j < mHeight; j++)
                    {
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 4) * 0) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 4) * 1) + i)] = tempFrm[XY_MODE_PROGMEM(j, reverseI)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 4) * 2) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 4) * 3) + i)] = tempFrm[XY_MODE_PROGMEM(j, reverseI)];
                    }
                }

                DrawMatrix(mHeight, mWidth);
                DrawBudhurasmala();
                DisplayFrame();
            }
            else
            {
                MessageBox.Show("Matrix size not support for this function", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void FillFirstBlock8X()
        {
            if (mWidth % 8 == 0)
            {
                string[] tempFrm = clearMainFrmAndTransferValues();

                for (int i = 0; i < mWidth / 8; i++)
                {
                    for (int j = 0; j < mHeight; j++)
                    {
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 0) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 1) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 2) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 3) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 4) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 5) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 6) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 7) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                    }
                }

                DrawMatrix(mHeight, mWidth);
                DrawBudhurasmala();
                DisplayFrame();
            }
            else
            {
                MessageBox.Show("Matrix size not support for this function", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void FillFirstBlock8X_Reverse()
        {
            if (mWidth % 8 == 0)
            {
                string[] tempFrm = clearMainFrmAndTransferValues();

                for (int i = 0; i < mWidth / 8; i++)
                {
                    int reverseI = ((mWidth / 8) - 1) - i;
                    for (int j = 0; j < mHeight; j++)
                    {
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 0) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 1) + i)] = tempFrm[XY_MODE_PROGMEM(j, reverseI)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 2) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 3) + i)] = tempFrm[XY_MODE_PROGMEM(j, reverseI)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 4) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 5) + i)] = tempFrm[XY_MODE_PROGMEM(j, reverseI)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 6) + i)] = tempFrm[XY_MODE_PROGMEM(j, i)];
                        frame[XY_MODE_PROGMEM(j, ((mWidth / 8) * 7) + i)] = tempFrm[XY_MODE_PROGMEM(j, reverseI)];
                    }
                }

                DrawMatrix(mHeight, mWidth);
                DrawBudhurasmala();
                DisplayFrame();
            }
            else
            {
                MessageBox.Show("Matrix size not support for this function", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void FillSecond_BlockWith_FirstBlock_Xmirror()
        {
            string[] tempFrm = clearMainFrmAndTransferValues();

            for (int i = 0; i < mHeight; i++)
            {
                for (int j = 0; j < mWidth; j++)
                {
                    int reverseJ = (mWidth - 1) - j;
                    int reverseI = (mHeight - 1) - i;
                    frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(i, reverseJ)];
                }
                for (int j = 0; j < mWidth / 2; j++)
                {
                    frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(i, j)];
                }
            }

            DrawMatrix(mHeight, mWidth);
            DrawBudhurasmala();
            DisplayFrame();
        }

        void FillDown_BlockWith_UpBlock_Ymirror()
        {
            string[] tempFrm = clearMainFrmAndTransferValues();

            for (int i = 0; i < mHeight; i++)
            {
                for (int j = 0; j < mWidth; j++)
                {
                    int reverseJ = (mWidth - 1) - j;
                    int reverseI = (mHeight - 1) - i;
                    frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(reverseI, j)];
                }
            }

            for (int i = 0; i < mHeight / 2; i++)
            {
                for (int j = 0; j < mWidth; j++)
                {
                    frame[XY_MODE_PROGMEM(i, j)] = tempFrm[XY_MODE_PROGMEM(i, j)];
                }
            }

            DrawMatrix(mHeight, mWidth);
            DrawBudhurasmala();
            DisplayFrame();
        }

        /*############################################################################*/

        private void button13_Click(object sender, EventArgs e)
        {
            //Frame_Xmirror();
            //Frame_Ymirror();
            //Frame_LeftHalf_Xmirror();
            //Frame_RightHalf_Xmirror();
            //Frame_LeftHalf_Ymirror();
            //Frame_RightHalf_Ymirror();
            //FillFirstBlock4X();
            //FillFirstBlock8X();
            //FillSecond_BlockWith_FirstBlock_Xmirror();
            //FillDown_BlockWith_UpBlock_Ymirror();

            //MessageBox.Show(frames_status[0].ToString());
            //listBox1.Refresh();
        }

        private void xMirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frame_Xmirror();
        }

        private void yMirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frame_Ymirror();
        }

        private void leftHXMirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frame_LeftHalf_Xmirror();
        }

        private void rightHYMirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frame_RightHalf_Xmirror();
        }

        private void leftHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frame_LeftHalf_Ymirror();
        }

        private void rightHYMirrorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Frame_RightHalf_Ymirror();
        }

        private void fillFirstBlock4XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillFirstBlock4X();
        }

        private void fillFirstBlock8XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillFirstBlock8X();
        }

        private void fillFirstBlock4XReverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillFirstBlock4X_Reverse();
        }

        private void fillFirstBlock8XReverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillFirstBlock8X_Reverse();
        }

        private void fill2BWith1BXMirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillSecond_BlockWith_FirstBlock_Xmirror();
        }

        private void fillDBWithUBYMirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillDown_BlockWith_UpBlock_Ymirror();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
        }

        private void restoreFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(frames_status[selected_frame] == false)
            {
                frames_status[selected_frame] = true;
                deleted_frames--;
                ReloadFrameListBox();
                label12.Text = "Total Size : " + ((num_frames - deleted_frames) * matrixSize).ToString() + " bytes (" + (((num_frames - deleted_frames) * matrixSize) / 1024).ToString() + "KB) | Address Count : " + (((num_frames - deleted_frames) * matrixSize) - 16).ToString("X8");

                for (int i = 0; i < matrixSize; i++)
                {
                    frame[i] = all_frames[(matrixSize * selected_frame) + i];
                }
                DrawMatrix(mHeight, mWidth);
                DrawBudhurasmala();
                DisplayFrame();

                MessageBox.Show("Restored Successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Selected frame is not deleted!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            rectSize = Convert.ToInt32(numericUpDown3.Value);

            DrawMatrix(mHeight, mWidth);
            DrawBudhurasmala();
            DisplayFrame();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (previewForm == null || previewForm.IsDisposed)
            {
                previewForm = new Form4(Colors);

                if (mWidth > 0 && mHeight > 0)
                {
                    previewForm.init(mHeight, mWidth);
                    previewForm.showFrame(frame);
                }
            }

            previewForm.Show();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked || radioButton3.Checked)
            {
                for (int i = 0; i < matrixSize; i++)
                {
                    if (frameTempForDisplay[i] != "0")
                    {
                        frame[i] = frameTempForDisplay[i];
                    }
                }
            }
            DrawBudhurasmala();
            DisplayFrame();
            isNotDrawing = true;
            label6.Text = "... - ...";
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < matrixSize; i++)
            {
                if(frame[i] == pickedColorIndex.ToString())
                {
                    frame[i] = selectedColorIndex.ToString();
                }
            }
            DrawMatrix(mHeight, mWidth);
            DrawBudhurasmala();
            DisplayFrame();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked = false;
                radioButton3.Checked = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < matrixSize; i++)
            {
                frame[i] = "0";
            }
            DrawMatrix(mHeight, mWidth);
            DrawBudhurasmala();
            DisplayFrame();
        }
    }
}
