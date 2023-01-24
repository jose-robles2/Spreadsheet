using System;
using System.Collections.Generic;
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

        public Tree()
        {
            root = null;
        }

        public bool insertNode(int newData)
        {
            return insertNode(this.root, newData); 
        }
        private bool insertNode(Node tree, int newData)
        {
            if (tree != null)
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
            this.root = new Node(newData); 
            return true;  
        }
    }
}
