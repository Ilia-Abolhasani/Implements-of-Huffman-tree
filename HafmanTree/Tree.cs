using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HafmanTree
{
    public class HeapTree
    {
        public Node Head=null;
        public int NodeCounter=0;
        public int Htree;
        public HeapTree()
        {
        }
        public void InsertNode(Node ExtensionNode)
        {
            if (Head==null)
            {
                Head = ExtensionNode;
                Head.Father = null;                
                NodeCounter++;
            }
            else
            {
                Add(Head, 1, ExtensionNode);
            }
        }
        private void Add(Node node, int Number, Node ExtensionNode)
        {
            int TempNodeCounter=NodeCounter+1;
            while (TempNodeCounter / 2 != Number)
                TempNodeCounter /= 2;
            if (TempNodeCounter%2==0)
            {
                if (node.LeftChilde!=null)
                    Add(node.LeftChilde, Number * 2, ExtensionNode);                
                else
                {
                    node.LeftChilde = ExtensionNode;
                    ExtensionNode.Father = node;
                    NodeCounter++;
                    HeapRoul(ExtensionNode);  
                }
            }
            else
            {
                if (node.RightChilde!= null)
                    Add(node.RightChilde, Number * 2 + 1, ExtensionNode);                
                else
                {                    
                    node.RightChilde = ExtensionNode;
                    ExtensionNode.Father = node;
                    NodeCounter++;
                    HeapRoul(ExtensionNode);
                }
            }
        }             
        public void HeapRoul(Node Temp)
        {
            while (Temp.Father != null)
            {
                if (Temp.Value < Temp.Father.Value)
                {
                    int TempValue = Temp.Father.Value;
                    char TempCharecter = Temp.Father.charecter;
                    Temp.Father.Value = Temp.Value;
                    Temp.Father.charecter = Temp.charecter;
                    Temp.charecter = TempCharecter;
                    Temp.Value = TempValue;

                    Node RightChildeHuffman = Temp.RightChildeHuffman;
                    Node LeftChildeHuffman = Temp.LeftChildeHuffman;
                    Temp.RightChildeHuffman = Temp.Father.RightChildeHuffman;
                    Temp.LeftChildeHuffman = Temp.Father.LeftChildeHuffman;
                    Temp.Father.RightChildeHuffman = RightChildeHuffman;
                    Temp.Father.LeftChildeHuffman= LeftChildeHuffman;
                }
                Temp = Temp.Father;
            }
        }        
        public Node RemoveNode()
        {
            Node Ans = Head;
            if (NodeCounter != 1)
            {          
            Node LastNode = FindNode(Head, 1, NodeCounter);
            Node FatherOfLastNode = FindNode(Head, 1, NodeCounter/2);
            if (NodeCounter % 2 == 0)
                FatherOfLastNode.LeftChilde = null;
            else
                FatherOfLastNode.RightChilde = null;
            LastNode.Father = null;
            LastNode.LeftChilde = Head.LeftChilde;
            LastNode.RightChilde = Head.RightChilde;
            if (Head.LeftChilde!=null)
                Head.LeftChilde.Father = LastNode;
            if (Head.RightChilde!= null)
                Head.RightChilde.Father = LastNode;
            Head = LastNode;
            }
            else
                Head = null;
            for (int i = 1; i < NodeCounter; i++)
                HeapRoul(FindNode(Head, 1, i));
            NodeCounter--;
            Ans.LeftChilde = null;
            Ans.RightChilde= null;
            Ans.Father = null;
            return Ans;            
        }
        public Node FindNode(Node node,int Number,int NodeNumber)
        {            
            if (NodeNumber == Number)
                return node;
            int TempNodeNumber= NodeNumber;
            while (TempNodeNumber / 2 != Number)
                TempNodeNumber /= 2;
            if (TempNodeNumber % 2 == 0)
                return (FindNode(node.LeftChilde, Number * 2, NodeNumber));
            else
                return (FindNode(node.RightChilde, Number * 2 + 1, NodeNumber));
        }
    }
}
