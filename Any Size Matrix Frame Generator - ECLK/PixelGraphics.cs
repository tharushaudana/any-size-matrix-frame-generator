using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Any_Size_Matrix_Frame_Generator___ECLK
{
    class PixelGraphics
    {
        public string[] PixelDrawLine(int x, int y, int x2, int y2, int framesize, int selectedColorIndex, int mWidth)
        {
            string[] outputframe;
            outputframe = new string[framesize];
            for(int i = 0; i < framesize; i++)
            {
                outputframe[i] = "0";
            }

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
                outputframe[XY_MODE_PROGMEM(y, x, mWidth)] = selectedColorIndex.ToString();

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

            return outputframe;
        }

        public string[] PixelDrawRectangle(int x1, int y1, int x2, int y2, int framesize, int selectedColorIndex, int mWidth)
        {
            string[] outputframe;
            string[] tempframe;
            outputframe = new string[framesize];
            tempframe = new string[framesize];
            for (int i = 0; i < framesize; i++)
            {
                outputframe[i] = "0";
                tempframe[i] = "0";
            }

            //layer1
            tempframe = PixelDrawLine(x1, y1, x2, y1, framesize, selectedColorIndex, mWidth);
            for(int i = 0; i < framesize; i++)
            {
                outputframe[i] = tempframe[i];
            }
            //layer2
            tempframe = PixelDrawLine(x2, y1, x2, y2, framesize, selectedColorIndex, mWidth);
            for (int i = 0; i < framesize; i++)
            {
                if (tempframe[i] != "0")
                {
                    outputframe[i] = tempframe[i];
                }
            }
            //layer3
            tempframe = PixelDrawLine(x2, y2, x1, y2, framesize, selectedColorIndex, mWidth);
            for (int i = 0; i < framesize; i++)
            {
                if (tempframe[i] != "0")
                {
                    outputframe[i] = tempframe[i];
                }
            }
            //layer4
            tempframe = PixelDrawLine(x1, y2, x1, y1, framesize, selectedColorIndex, mWidth);
            for (int i = 0; i < framesize; i++)
            {
                if (tempframe[i] != "0")
                {
                    outputframe[i] = tempframe[i];
                }
            }

            return outputframe;
        }
        public int GetRectPosition(int x, int y, int num_of_rects, int[,] rectangle_data)
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
        }

        private bool find_clicked_square(int x, int y, int width, int height, int currentLocX, int currentLocY)
        {
            bool result;
            Rectangle find = new Rectangle(x, y, width, height);
            result = find.Contains(new Point(currentLocX, currentLocY));

            return result;
        }
        private int XY_MODE_PROGMEM(int y, int x, int mWidth)
        {
            int i;
            i = (y * mWidth) + x;
            return i;
        }
    }
}
