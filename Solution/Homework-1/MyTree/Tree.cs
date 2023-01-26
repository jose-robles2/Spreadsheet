using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homework_1.MyTree
{
    /********************************************************************************************
     * Class: Tree																				*
     * Description:	Tree class. Made up of Node objects. Has a single data member damed root    *
     * which points to the start of the tree. Has default constructor                           *
     *******************************************************************************************/
    internal class Tree
    {
        private Node? root;
        public Node? Root { get { return root; } }
        public Tree() => root = null;

        //https://www.javatpoint.com/relationship-between-number-of-nodes-and-height-of-binary-tree

        /// <summary>
        /// Get min depth of a tree given it's node count -> perfect BST has min depth
        /// In CptS 317 we did a mathematical proof of induction saying that 2^(h+1) - 1
        /// is equal to the number of nodes in a perfect binary tree of height h. But, we 
        /// have the number of nodes already and are looking for the height. So a variation
        /// of this equation will be needed. When searching online, I found the following article: 
        /// https://www.javatpoint.com/relationship-between-number-of-nodes-and-height-of-binary-tree
        /// h = ln(x) where x==num of nodes, we will do ln(x) + 1 since the article talks about height
        /// and the root isnt included in the height whereas we're doing "levels" and the root is included
        /// </summary>
        /// <returns> integer for min level count</returns>
        public int GetMinDepth()
        {
            int nodeCount = GetNodeCount();
            double res = Math.Floor(Math.Log2(nodeCount)) + 1;
            return (int)res;
        }

        /// <summary>
        /// Public interface for GetDepth, pass in root 
        /// </summary>
        public int GetDepth()
        {
            return 1 + GetDepth(this.root); 
        }

        /// <summary>
        /// Public interface for GetNodeCount, pass in root 
        /// </summary>
        public int GetNodeCount()
        {
            return GetNodeCount(this.root); 
        }

        /// <summary>
        /// Public interface for InOrderTraversal, pass in root 
        /// </summary>
        public void InOrderTraversal()
        {
            InOrderTraversal(this.root);
            Console.Write("\n"); 
        }

        /// <summary>
        /// Public interface for InsertNode, pass in root and newData
        /// </summary>
        public bool InsertNode(int newData)
        {
            return InsertNode(this.root, newData); 
        }

        /// <summary>
        /// Private getDepth() - recursively traverse through the tree to find the max depth to get total level count
        /// </summary>
        private int GetDepth(Node node)
        {
            if (node != null)
            {
                return 1 + Math.Max(GetDepth(node.Left), GetDepth(node.Right));
            }
            return -1; 
        }

        /// <summary>
        /// Private getNodeCount - Left, Right, Process -> Post order traversal
        /// Traverse left until null is reached, pop the stack frame to traverse up the tree, then go right until null is reached.
        /// Once both children are null, we can process the current node by doing + 1. 0 Is returned if node is null
        /// </summary>
        /// <param name="node"> Node object, will represent root initially then internal nodes due to recursion </param>
        /// <returns> Integer total node count </returns>
        private int GetNodeCount(Node node)
        {
            int count = 0; 
            if (node != null)
            {
                count += GetNodeCount(node.Left);
                count += GetNodeCount(node.Right);
                return count + 1; 
            }
            return 0;
        }

        /// <summary>
        /// Private inOrderTraversal - Left, Process, Right.
        /// Traverse left until null is reach, pop the stack frame to traverse up the tree, print current node,
        /// then check right node.If null, then pop stack frame again to traverse up the tree, else traverse right
        /// sub tree and repeat the process of Left, process, right.
        /// </summary>
        /// <param name="node"> Node object, will represent root initially then internal nodes due to recursion </param>
        /// <returns> void </returns>
        private void InOrderTraversal(Node node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.Write(node.Data + " ");
                InOrderTraversal(node.Right);
            }
        }

        /// <summary>
        /// Private insertNode.Recursive function that traverses the tree to insert the newData in the according spot.
        /// Utilizes lambda functions to traverse both left and right subtrees of the tree.Go left if newData is less
        /// than current node's data Go right if newData is less than current node's data. Insert once left/right node 
        /// is null. Do not allow duplicates to be inserted
        /// </summary>
        /// <param name="tree"> Node object, will represent root initially then internal nodes due to recursion </param>
        /// <param name="newData"> Integer to be inserted in tree </param>
        /// <returns> bool - successful or failed insert </returns>
        private bool InsertNode(Node tree, int newData)
        {
            /// <summary>
            /// Lambda helper to traverse left. Check if null to insert, else recursion
            /// </summary>            
            Func<Node, int, bool> TraverseLeft = (node, newData) => {
            if (tree.Left == null)
                {
                    tree.Left = new Node(newData);
                    return true;
                }
                InsertNode(tree.Left, newData);
                return false; 
            };
            /// <summary>
            /// Lambda helper to traverse right. Check if null to insert, else recursion
            /// </summary>  
            Func<Node, int, bool> TraverseRight = (node, newData) =>
            {
                if (tree.Right == null)
                {
                    tree.Right = new Node(newData);
                    return true;
                }
                InsertNode(tree.Right, newData);
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
                    return TraverseLeft(tree.Left, newData);
                }
                else if (newData > tree.Data)
                {
                    return TraverseLeft(tree.Right, newData);  
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
