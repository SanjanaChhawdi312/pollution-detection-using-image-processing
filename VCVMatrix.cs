using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ntccgui
{
    internal class VCVMatrix
    {
        public double[][] PDMMatrix;
        private Image image;
        public CompAvg RGB;
        public double CorelationRB, CorelationRG, CorelationGB, SDR, SDG, SDB;
        public VCVMatrix(Image current)
        {
            image = current;
            Bitmap OriginalImage = new Bitmap(current);
            RGB = new CompAvg(OriginalImage);
            RGB.ComputeAvg();
            PDMMatrix= new double[3][];
            for(int i = 0; i < 3; i++)
            {
                PDMMatrix[i]=new double[3];
            }
            GetMatrix();
        }
        public double[][] GetMatrix()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    double iAvg = 0;
                    double jAvg = 0;
                    int[][] iValue = GetColor(i, ref iAvg);
                    int[][] jValue = GetColor(j, ref jAvg);
                    PDMMatrix[i][j] = ComputeSigma(iValue, jValue, iAvg, jAvg);
                }
            }
            CorelationGB = ComputeCorelation(1, 2);
            CorelationRB = ComputeCorelation(0, 2);
            CorelationRG = ComputeCorelation(0, 1);
            SDR=GetStandardDeviation(0);
            SDG=GetStandardDeviation(1);
            SDB=GetStandardDeviation(2);
            return PDMMatrix;
        }
        private int[][] GetColor(int cno, ref double avg)
        {
            switch (cno)
            {
                case 0:
                    {
                        avg = RGB.AvgRed;
                        return RGB.GetRedPix();
                    }
                case 1:
                    {
                        avg = RGB.AvgGreen;
                        return RGB.GetGreenPix();
                    }
                case 2:
                    {
                        avg = RGB.AvgBlue;
                        return RGB.GetBluePix();
                    }
                default:
                    {
                        avg = RGB.AvgRed;
                        return RGB.GetRedPix();
                    }
            }
        }
        private double ComputeSigma(int[][] iColor, int[][] jColor, double iAvg, double jAvg)
        {
            double Sigma = 0;
            double check1 = 0, check2 = 0;
            double temp1=0, temp2=0;
            int count = 0;
            for(int i=0;i< image.Size.Width; i++)
            {
                for(int j=0;j< image.Size.Height; j++)
                {
                    temp1 = iColor[i][j] - iAvg;
                    temp2 = jColor[j][j] - jAvg;
                    if (jColor[i][j] < jAvg)
                    {
                        count++;
                        check1 = check1 + temp1;
                        check2 = check2 + temp2;
                    }
                    Sigma = Sigma + (temp1 * temp2);
                }
            }
            int check3 = (int)check2;
            return (Sigma / RGB.totalPix - 1);
        }
        public double ComputeCorelation(int c1, int c2 )
        {
            double Corelation = 0;
            double covar1 = Math.Sqrt(PDMMatrix[c1][c1]);
            double covar2 = Math.Sqrt(PDMMatrix[c2][c2]);
            Corelation = PDMMatrix[c1][c2]/(covar1*covar2);
            return Corelation;
        }
        public double GetStandardDeviation(int c1)
        {
            return (Math.Sqrt(PDMMatrix[c1][c1]));
        }
    }
}
