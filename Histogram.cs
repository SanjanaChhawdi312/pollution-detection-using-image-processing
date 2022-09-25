using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ntccgui
{
    internal class Histogram
    {
        private int[] mAll;
        private int[] mRed;
        private int[] mGreen;
        private int[] mBlue;
        private int totalPix;
        public enum ColorType { MIX = -1, ALL = 0, RED, GREEN, BLUE };
        private static Color[] histColor = { Color.Gray, Color.Red, Color.Lime };
        public int All
        {
            get;
            private set;
        }
        public int Red
        {
            get;
            private set;
        }
        public int Green
        {
            get;
            private set;
        }
        public int Blue
        {
            get;
            private set;
        }
        public int TotalPix
        {
            get;
            private set;
        }
        public int MaxAllIndex
        {
            get;
            private set;
        }
        public int MaxRedIndex
        {
            get;
            private set;
        }
        public int MaxGreenIndex
        {
            get;
            private set;
        }
        public int MaxBlueIndex
        {
            get;
            private set;
        }
        public static Color HistogramColor(ColorType type)
        {
            return histColor[(int)type];
        }
        
        public Histogram(Bitmap bmp)
        {
            Color[][] clr = null;
            Size size = new Size();
            mAll = new int[256];
            mRed = new int[256];
            mGreen = new int[256];
            mBlue = new int[256];
            
            if (bmp != null)
            {
                size = bmp.Size;
                totalPix=bmp.Width*bmp.Height;
                clr=new Color[size.Width][];
                for(int i=0; i<size.Width; i++)
                {
                    clr[i] = new Color[size.Height];
                }
                for(int i=0;i<size.Width; i++)
                {
                    for(int j=0;j<size.Height; j++)
                    {
                        clr[i][j] = bmp.GetPixel(i, j);
                        mRed[clr[i][j].R]++;
                        mGreen[clr[i][j].G]++;
                        mBlue[clr[i][j].B]++;
                        mAll[(clr[i][j].R + clr[i][j].G+ clr[i][j].B)/3]++;
                    }
                }
            }
            initMaxandTotal();
        }
        private void initMaxandTotal()
        {
            int MaxAll = 0;
            int MaxRed = 0;
            int MaxGreen = 0;
            int MaxBlue = 0;
            int TotAll = mAll[0];
            int TotRed = mRed[0];
            int TotGreen = mGreen[0];
            int TotBlue = mBlue[0];
            for (int i = 1; i < mAll.GetLength(0) ; i++)
            {
                TotAll += mAll[i];
                TotRed += mRed[i];
                TotGreen += mGreen[i];
                TotBlue += mBlue[i];
                if (mAll[MaxAll] < mAll[i])
                    MaxAll = i;
                if (mRed[MaxRed] < mRed[i] )
                MaxRed = i;
                if (mGreen[MaxGreen] < mGreen[i])
                    MaxGreen = i;
                if (mBlue[MaxBlue] < mBlue[i])
                    MaxBlue = i;
            }
        }
        public void GetAllHistogram(int[] lpResult)
        {
            copyArray(mAll, lpResult);
        }
        public void GetRedHistogram(int[] lpResult)
        {
            copyArray(mRed, lpResult);
        }
        public void GetGreenHistogram(int[] lpResult)
        {
            copyArray(mGreen, lpResult);
        }
        public void GetBlueHistogram(int[] lpResult)
        {
            copyArray(mBlue, lpResult);
        }
        private void copyArray(float [] lpFrom, float[] lpTo)
        {
            int iSize;
            if(lpFrom != null && lpTo != null )
            {
                iSize = Math.Min(lpFrom.GetLength(0), lpTo.GetLength(0));
                for (int i = 0; i < iSize ; i++)
                    lpTo[i] = lpFrom[i];
            } 
        }
        private void copyArray(int[] lpFrom, int[] lpTo)
        {
            int iSize;
            if (lpFrom != null && lpTo != null)
            {
                iSize = Math.Min(lpFrom.GetLength(0), lpTo.GetLength(0));
                for (int i = 0; i < iSize; i++)
                    lpTo[i] = lpFrom[i];
            }
        }
        public void drawHistogram(Graphics g, int[] data, Rectangle rect, Color clr, int iMaxIndex)
        {
            drawHistogram(g, data, rect, clr, iMaxIndex, -1);
        }
        public void drawHistogram(Graphics g, int[] data, Rectangle rect, Color color, int iMaxIndex, int intensity)
        {
            float pixWidth, pixHeight;
            int MatMax, i;
            int ptArrSize=data.GetLength(0);
            Point[] pt= new Point[ptArrSize+2];
            pt[0].X = rect.Left;
            pt[0].Y = rect.Bottom;
            pt[pt.GetLength(0) - 1].X = rect.Right;
            pt[pt.GetLength(0) - 1].Y = rect.Bottom;

            MatMax = data[iMaxIndex];
            pixWidth = rect.Width / (float)ptArrSize;
            pixHeight=rect.Height / (float)MatMax;

            for(i = 0; i < ptArrSize; i++)
            {
                int preY = (int)(data[i] * pixHeight);
                if (preY <= 1 && data[i] > 0)
                {
                    preY = 2; 
                }
                pt[i + 1].Y = rect.Bottom - preY;
                pt[i + 1].X = (int)(i * pixWidth) + rect.Left;
            }
            //g.FillPolygon(new SolidBrush(color), pt);
            if (intensity >= 0 && intensity < 256)
            {
                g.DrawLine(new Pen(Color.Black,pixWidth), intensity * pixWidth,0,intensity*pixWidth, rect.Bottom);
            }
            g.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
        }
        public bool GetHistogram(ColorType type, int[] lpResult)
        {
            bool ret = true;
            switch (type)
            {
                case ColorType.ALL:
                    GetAllHistogram(lpResult);
                    break;
                case ColorType.RED:
                    GetRedHistogram(lpResult);
                    break;
                case ColorType.GREEN:
                    GetRedHistogram(lpResult);
                    break;
                case ColorType.BLUE:
                    GetBlueHistogram(lpResult);
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }
        public int GetMaxIndex(ColorType type)
        {
            int ret = -1;
            switch (type)
            {
                case ColorType.ALL :
                    ret = MaxAllIndex;
                    break;
                case ColorType.RED:
                    ret = MaxRedIndex;
                    break;
                case ColorType.GREEN:
                    ret = MaxGreenIndex;
                    break;
                case ColorType.BLUE:
                    ret = MaxBlueIndex;
                    break;
            }
            return (ret);
        }
    }
}
