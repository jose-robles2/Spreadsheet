using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1.MyTree
{
    /*
        Tree class. Made up of Node class objects. Has a single data member named root which points
        to the root, or the beginning of the tree. Nodes must be nullable types so add the '?'
     */
    internal class Tree
    {
        private Node? root;

        public Node? Root { get { return root; } } 

        public Tree()
        {
            root = null;
        }

        /*
            Public interface for insertNode, pass in root and the data
         */
        public bool insertNode(int newData)
        {
            return insertNode(this.root, newData); 
        }

        /*
            Private insertNode. Recursive function that traverses the tree to insert the newData in the according spot. 
            Go left if newData is less than current node's data
            Go right if newData is less than current node's data
            Insert once left/right node is null
            Do not allow duplicates to be inserted
         */
        private bool insertNode(Node tree, int newData)
        {
            if (tree == null)
            {
                this.root = new Node(newData); 
            }
            else
            {
                if (newData < tree.Data)
                {
                    if (tree.Left == null)
                    {
                        tree.Left = new Node(newData);
                        return true;
                    }
                    insertNode(tree.Left, newData);
                }
                else if (newData > tree.Data)
                {
                    if (tree.Right == null)
                    {
                        tree.Right = new Node(newData);
                        return true;
                    }
                    insertNode(tree.Right, newData);
                }
                else
                {
                    Console.WriteLine("Duplicate detected, " + newData + " already exists in the tree.");
                    return false;
                }
            }
            return true;  
        }
    }
}
