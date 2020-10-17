using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;  

namespace HafmanTree
{
    public partial class MainForm : Form
    {        
        Node HuffmanHead;
        string[] HCode;
        int HCodeCounter = 0;
        int Weight;
        public struct data
        {
          public char c;
          public int Abundance;
        }
        data[] InputData;
        HeapTree heapTree;

        public void ClearAll()
        {
            HuffmanHead = null;
            HCode = null;
            HCodeCounter = 0;
            Weight = 0;
            InputData = null;
            heapTree = null;                                    
        }
        public MainForm()
        {
            InitializeComponent();
        }
        private void doing()
        {
            InputData = null;
            listBox1.Items.Clear();
            string Text = FreeSpace(richTextBox1.Text);
            string MargedText = Marge(Text);
            InputData = new data[MargedText.Length];
            for (int i = 0; i < MargedText.Length; i++)
            {
                InputData[i].Abundance = CountExist(richTextBox1.Text, MargedText[i]);
                InputData[i].c = MargedText[i];                
            }
            //sorting InputData
            for (int i = 0; i < MargedText.Length-1; i++)
                for (int j = i+1; j < MargedText.Length; j++)
                    if (InputData[i].Abundance>InputData[j].Abundance )
                    {
                        char tempCharecter = InputData[i].c;
                        InputData[i].c = InputData[j].c;
                        InputData[j].c = tempCharecter;
                        int tempValue=InputData[i].Abundance;
                        InputData[i].Abundance=InputData[j].Abundance;
                        InputData[j].Abundance=tempValue;
                    }
            for (int i = 0; i < MargedText.Length; i++)
            {
                string temp = InputData[i].c + "  =  " + InputData[i].Abundance;
                listBox1.Items.AddRange(new object[] { temp });
            }
        }

        #region Text Editing


        private string Marge(string input)
        {
            string Ans="";
            for (int i = 0; i < input.Length; i++)
                if (!IsExist(Ans, input[i]))
                    Ans += input[i];
            return Ans;
        }
        private bool IsExist(string ReferenceList,char input)
        {
            for (int i = 0; i < ReferenceList.Length; i++)
                if (ReferenceList[i] == input)
                    return true;
            return false;
        }
        private int CountExist(string ReferenceList,char input)
        {
            int Ans = 0;
            for (int i = 0; i < ReferenceList.Length; i++)
                if (ReferenceList[i] == input)
                    Ans++;
            return Ans;
        }
        private string FreeSpace(string input)
        {
            string ans = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != ' ' && input[i] != '\n') 
                    ans +=input[i];
            }
            return ans;
        }

        #endregion
        private int Pow2(int input)
        {
            int Ans = 1;
            for (int i = 0; i < input; i++)
                Ans *= 2;   
            return Ans;
        }
        
        private Bitmap DrowNode(Node N, int w, int h,int W, int H, Bitmap bitmap,bool IsLeft)
        {
            Graphics graphicImage = Graphics.FromImage(bitmap);
            graphicImage.SmoothingMode = SmoothingMode.AntiAlias;
            string Str = "";
            if (N.charecter != ' ')
                Str = N.charecter.ToString();
            else
                Str = N.Value.ToString();

            if (Str.ToString().Length == 1)
            {
                graphicImage.DrawString(Str,
                new Font("Arial", 12, FontStyle.Bold),
                SystemBrushes.WindowText, new Point(w - 10, h + 13));
            }
            else
            {            
                graphicImage.DrawString(Str,
                new Font("Arial", 12, FontStyle.Bold),
                SystemBrushes.WindowText, new Point(w - 15, h + 13));
            }            
            graphicImage.DrawArc(new Pen(Color.Red, 5), w-25, h, 50, 50, 0, 360);
            if (IsLeft)
                graphicImage.DrawLine(new Pen(Color.Cyan, 5), w+3, h-2, W - 25, H + 35);
            else
                graphicImage.DrawLine(new Pen(Color.Cyan, 5), w-3, h-2, W + 25, H + 35);

            return bitmap;
        }
        private Bitmap Drow(Node node, int W, int h,Bitmap bitmap,int HeghitTree)
        {
            int temp = ((Weight / 50) / (Pow2(HeghitTree+1)))*50;
            if (node.LeftChildeHuffman != null)
                bitmap = Drow(node.LeftChildeHuffman, W - temp, h + 150, DrowNode(node.LeftChildeHuffman, W - temp, h + 150, W, h, bitmap, true), HeghitTree + 1);
            if (node.RightChildeHuffman!= null)
                bitmap = Drow(node.RightChildeHuffman, W + temp, h + 150, DrowNode(node.RightChildeHuffman, W + temp, h + 150, W, h, bitmap, false), HeghitTree + 1);
            return bitmap;
        }



        private Bitmap CreateImage(Node node)
        {
            int Max=0;
            for (int i = 0; i < HCode.Length; i++)
                if (Max < HCode[i].Length)
                    Max = HCode[i].Length;
            Max--;
            Bitmap bitmap = new System.Drawing.Bitmap(50 * Pow2(Max+1), 100 + Max * 150);
            //set bitmap back ground Light Yellow
            for (int i = 0; i < bitmap.Width; i++)
                for (int j = 0; j < bitmap.Height; j++)
                    bitmap.SetPixel(i, j, Color.LightYellow);
            //Graphics Setting
            Graphics graphicImage = Graphics.FromImage(bitmap);
            graphicImage.SmoothingMode = SmoothingMode.AntiAlias;
            //Drow Head
            if (node.Value.ToString().Length==1)
            {
                graphicImage.DrawString(node.Value + "",
                new Font("Arial", 12, FontStyle.Bold),
                SystemBrushes.WindowText, new Point(bitmap.Width / 2 -10, 38));
            }
            else
            {
                graphicImage.DrawString(node.Value + "",
            new Font("Arial", 12, FontStyle.Bold),
            SystemBrushes.WindowText, new Point(bitmap.Width / 2 - 15, 38));
            }
            graphicImage.DrawArc(new Pen(Color.Red, 5),bitmap.Width/2-25,25, 50, 50, 0, 360);
            Weight = bitmap.Width;
            bitmap = Drow(node, bitmap.Width/2 , 25, bitmap,1);            
                        
            bitmap.Save("huffmanTree.jpg",ImageFormat.Jpeg);
            return bitmap;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            doing();
        }
        


        private void HCodeCreator(Node node,string Str)
        {            
            if (node.charecter!=' ')
                HCode[HCodeCounter++] = Str + node.charecter;
            else
        	{
                if (node.LeftChildeHuffman!=null)
                    HCodeCreator( node.LeftChildeHuffman , Str + "0");
                if (node.RightChildeHuffman != null)
                    HCodeCreator( node.RightChildeHuffman , Str + "1");
	        }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            heapTree = null;
            HCode = null;
            HuffmanHead = null;
            HCodeCounter = 0;
            if (FreeSpace(richTextBox1.Text)== "")
            {
                MessageBox.Show("Text Box is empty!!! Please fill it");
                return;
            }
            doing();
            heapTree = new HeapTree();
            foreach (var item in InputData)
            {
                Node ExtensionNode = new Node(item.Abundance, item.c);
                heapTree.InsertNode(ExtensionNode);
            }
            Node T = heapTree.Head;
            while (heapTree.NodeCounter>1)
            {
                Node NewNode = new Node(0,' ');
                NewNode.LeftChildeHuffman = heapTree.RemoveNode();
                NewNode.RightChildeHuffman= heapTree.RemoveNode();
                NewNode.Value = NewNode.LeftChildeHuffman.Value + NewNode.RightChildeHuffman.Value;
                heapTree.InsertNode(NewNode);                                                
            }
            HuffmanHead=heapTree.RemoveNode();
            HCode = new string[listBox1.Items.Count];
            HCodeCreator(HuffmanHead,"");
            listBox2.Items.Clear();
            for (int i = 0; i < HCode.Length; i++)
            {                
                string Temp = ""+HCode[i][((HCode[i].Length) - 1)];
                Temp += " = ";
                for (int j = 0; j < HCode[i].Length- 1; j++)
                    Temp += HCode[i][j];
                listBox2.Items.AddRange(new object[] { Temp });                 
            }
            pictureBox1.Image = CreateImage(HuffmanHead);
        }
                                  
    }
}
