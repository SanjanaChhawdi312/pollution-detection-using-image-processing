using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ntccgui
{
    internal class CompAvg
    {
        private int[][] red;
        private int[][] green;
        private int[][] blue;
        private Size size;
        public double AvgRed, AvgGreen, AvgBlue,totalPix;
        public CompAvg(Bitmap bmp)
        {
            size= bmp.Size;
            Color[][] clr = null;
            red = null;
            green = null;
            blue = null;
            int[][] CurrentRed = new int[size.Width][];
            int[][] CurrentGreen = new int[size.Width][];
            int[][] CurrentBlue=new int[size.Width][];
            clr=new Color[size.Width][];
            for(int i = 0; i < size.Width; i++)
            {
                CurrentRed[i]=new int[size.Height];
                CurrentGreen[i]=new int[size.Height];
                CurrentBlue[i]=new int[size.Height];
                clr[i] = new Color[size.Height];
            }
            if (bmp != null)
            {
                for(int i = 0; i < size.Width; i++)
                {
                    for(int j=0;j<size.Height; j++)
                    {
                        clr[i][j] = bmp.GetPixel(i, j);
                        CurrentRed[i][j] = clr[i][j].R;
                        CurrentGreen[i][j] = clr[i][j].G;
                        CurrentBlue[i][j] = clr[i][j].B;
                    }
                }
            }
            red = CurrentRed;
            green = CurrentGreen;
            blue = CurrentBlue;
        }
        public int[][] GetRedPix() { return red; }
        public int[][] GetGreenPix() { return green; }
        public int[][] GetBluePix() { return blue; }
        public void ComputeAvg()
        {
            totalPix = size.Width * size.Height;
            AvgRed = 0;
            AvgGreen = 0;
            AvgBlue = 0;
            for (int i = 0; i < size.Width; i++)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    AvgRed+=red[i][j];
                    AvgGreen+=green[i][j];
                    AvgBlue+=blue[i][j];
                }
            }
            AvgRed /= totalPix;
            AvgGreen /= totalPix;
            AvgBlue /= totalPix;
        }
    }
}
