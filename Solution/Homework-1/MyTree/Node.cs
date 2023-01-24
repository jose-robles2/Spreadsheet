using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1.MyTree
{
    /*
        Node class object. These make up the tree. An integer datatype is stored as the attribute for each node, as well
        as left and right children objects. Both are Node objects so it is recursive. Nodes must be nullable types so add the '?'
     */
    internal class Node
    {
        private int data;
        private Node? left; 
        private Node? right;

        public int Data { get { return data; } set { data = value; } }
        public Node? Left { get { return left; } set { left = value; } }
        public Node? Right { get { return right; } set { right = value; } }

        public Node (int newData = 0)
        {
            data = newData;
        }
    }
}
