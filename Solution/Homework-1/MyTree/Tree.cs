using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            Get min depth of a tree given it's node count -> perfect BST has min depth
            In CptS 317 we did a mathematical proof of induction saying that 2^(h+1) - 1
            is equal to the number of nodes in a perfect binary tree of height h. This was 
            proven to be true so that is where the formula is coming from. But, we have the
            number of nodes already and are looking for the height. When solving for h we get:
            h = ln(x+1) / ln(2) - 1 where x==num of nodes
            Height of a tree == depth of a tree. Math.Log(number being log'd, base of the log)
         */
        public double getMinDepth()
        {
            int nodeCount = getNodeCount();
            double numerator = 0.0, denominator = 0.0, height = 0.0;
            numerator = Math.Log(nodeCount+1, Math.E); 
            denominator = Math.Log(2, Math.E);
            height = numerator / denominator - 1;
            return Math.Floor(numerator) + 1;
        }

        /*
            Public interface for getNodeCount, pass in root 
         */
        public int getDepth()
        {
            return 1 + getDepth(this.root); 
        }

        /*
            Public interface for getNodeCount, pass in root 
         */
        public int getNodeCount()
        {
            return getNodeCount(this.root); 
        }

        /*
            Public interface for inOrderTraversal, pass in root 
         */
        public void inOrderTraversal()
        {
            inOrderTraversal(this.root);
            Console.Write("\n"); 
        }

        /*
            Public interface for insertNode, pass in root and the data
         */
        public bool insertNode(int newData)
        {
            return insertNode(this.root, newData); 
        }

        /*
            Private getDepth() - recursively traverse through the tree to find the max depth to get total level count
         */
        private int getDepth(Node node)
        {
            if (node != null)
            {
                return 1 + Math.Max(getDepth(node.Left), getDepth(node.Right));
            }
            return -1; 
        }

        /*
            Private getNodeCount - Left, Right, Process -> Post order traversal
            Traverse left until null is reached, pop the stack frame to traverse up the tree, then go right until null is reached.
            Once both children are null, we can process the current node by doing + 1. 0 Is returned if node is null
         */
        private int getNodeCount(Node node)
        {
            int count = 0; 
            if (node != null)
            {
                count += getNodeCount(node.Left);
                count += getNodeCount(node.Right);
                return count + 1; 
            }
            return 0;
        }

        /*
            Private inOrderTraversal - Left, Process, Right. 
            Traverse left until null is reach, pop the stack frame to traverse up the tree, print current node,
            then check right node. If null, then pop stack frame again to traverse up the tree, else traverse right 
            sub tree and repeat the process of Left, process, right. 
         */
        private void inOrderTraversal(Node node)
        {
            if (node != null)
            {
                inOrderTraversal(node.Left);
                Console.Write(node.Data + " ");
                inOrderTraversal(node.Right);
            }
        }

        /*
            Private insertNode. Recursive function that traverses the tree to insert the newData in the according spot. 
            Utilizes lambda functions to traverse both left and right subtrees of the tree.
            Go left if newData is less than current node's data
            Go right if newData is less than current node's data
            Insert once left/right node is null
            Do not allow duplicates to be inserted
         */
        private bool insertNode(Node tree, int newData)
        {
            // Lambda helper functions to traverse left and right
            Func<Node, int, bool> traverseLeft = (node, newData) => {
                if (tree.Left == null)
                {
                    tree.Left = new Node(newData);
                    return true;
                }
                insertNode(tree.Left, newData);
                return false; 
            };
            Func<Node, int, bool> traverseRight = (node, newData) =>
            {
                if (tree.Right == null)
                {
                    tree.Right = new Node(newData);
                    return true;
                }
                insertNode(tree.Right, newData);
                return false;
            };

            if (tree == null)
            {
                this.root = new Node(newData); 
            }
            else
            {
                if (newData < tree.Data)
                {
                    return traverseLeft(tree.Left, newData);
                }
                else if (newData > tree.Data)
                {
                    return traverseRight(tree.Right, newData);  
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
