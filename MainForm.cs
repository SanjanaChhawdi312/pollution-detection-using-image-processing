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
using System.Collections;
using System.Drawing.Drawing2D;
using Emgu.CV;
namespace ntccgui
{
    public partial class MainForm : Form
    {
        public Image original;
        String text;
        public Bitmap bmp;
            public MainForm()
            {
            InitializeComponent();
            }
        

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png;)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";    
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = Path.GetFileName(openFileDialog.FileName);
                textBox2.Text = openFileDialog.FileName;
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                bmp = new Bitmap(openFileDialog.FileName);
                original = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CompAvg avg = new CompAvg(bmp);
            avg.ComputeAvg();
            text = "\nThe average of Red, Green and Blue Pixels in image is : " + avg.AvgRed + " , " + avg.AvgGreen + " and " + avg.AvgBlue;
            richTextBox1.Text = text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            text = text + "\nThe variance-covariance matrix is : \n";
            VCVMatrix vcv = new VCVMatrix(original);
            double[][] PDMMatrix = vcv.GetMatrix();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    text = text + PDMMatrix[i][j] + "       ";
                }
                text = text + "\n";
            }
            text = text + "\nCorelation between Red and Green = " + vcv.CorelationRG;
            text = text + "\nCorelation between Green and Blue = " + vcv.CorelationGB;
            text = text + "\nCorelation between Red and Blue = " + vcv.CorelationRB;
            text = text + "\n\nStandard deviation of Red =" + vcv.SDR;
            text = text + "\nStandard deviation of Green =" + vcv.SDG;
            text = text + "\nStandard deviation of Blue =" + vcv.SDB;
            richTextBox1.Text = text;
        }
    }
}
