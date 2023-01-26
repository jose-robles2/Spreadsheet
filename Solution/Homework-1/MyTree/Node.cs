using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1.MyTree
{
    /********************************************************************************************
     * Class: Node																				*
     * Description:	Node class object. The BST is composed of these objects. An integer datatype*
     * is stored in each node, as well as the left and right child nodes. Nodes must be nullable*
     * types so the '?' is included after theyre declared. Contains parameterized constructor   *
     *******************************************************************************************/
    internal class Node
    {
        private int data;
        private Node? left; 
        private Node? right;

        public int Data { get { return data; } set { data = value; } }
        public Node? Left { get { return left; } set { left = value; } }
        public Node? Right { get { return right; } set { right = value; } }

        public Node (int newData = 0) => data = newData;
    }
}
