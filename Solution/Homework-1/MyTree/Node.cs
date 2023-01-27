using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HomeworkOne.MyTree
{
    /// <summary>
    /// Class: Node
    /// Node class object. The BST is composed of these objects. An integer datatype
    /// is stored in each node, as well as the left and right child nodes.Nodes must be nullable*
    /// types so the '?' is included after theyre declared. Contains parameterized constructor*
    /// </summary>
    internal class Node
    {
        private int data;
        private Node? left; 
        private Node? right;

        /// <summary>
        /// Parameterized constructor. 
        /// </summary>
        /// <param name="newData"> Data passed in to initialize the Node. Optional parameter due to the "= 0"</param>
        public Node(int newData = 0) => data = newData;

        public int Data { get { return data; } set { data = value; } }
        public Node? Left { get { return left; } set { left = value; } }
        public Node? Right { get { return right; } set { right = value; } }
    }
}
