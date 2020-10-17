using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HafmanTree
{
    public class Node
    {
        public int Value{ get; set; }
        public char charecter{ get; set; }
        public Node Father { get; set; }
        public Node LeftChilde { get; set; }
        public Node RightChilde { get; set; }
        public Node LeftChildeHuffman { get; set; }
        public Node RightChildeHuffman { get; set; }
        public Node(int Value, char charecter)
        {
            this.Value = Value;
            this.charecter = charecter;
        }
       
        public int GetH()
        {
            Node Temp = this;
            int Ans = 0;
            while (Temp.Father!=null)
            {
                Temp = Temp.Father;
                Ans++;
            }
            return Ans;
        }
    }
}
