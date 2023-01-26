using Homework_1.MyTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1
{
    /********************************************************************************************
     * Class: App																				*
     * Description:	Node class object. The BST is composed of these objects. An integer datatype*
     * is stored in each node, as well as the left and right child nodes. Nodes must be nullable*
     * types so the '?' is included after theyre declared. Contains parameterized constructor   *
     *******************************************************************************************/
    internal class App
    {
        /*
            Get user input from user, assume input is entered correctly EVERY time. Read input and 
            parse into an array of strings, then into an array of ints using CovertAll() method
         */
        private int[] getUserInput()
        {
            Console.WriteLine("Enter a list of numbers in the range [0, 100] separated by SINGLE spaces");
            string input = Console.ReadLine();
            string[] strArray = input.Split(" ");
            return (Array.ConvertAll(strArray, s => int.Parse(s)));
        }

        /*
            Create a BST from an array of integers. Utilize public interface insertNode() to 
            easily insert ints into the tree 
         */
        private Tree? createTree(int[] input)
        {
            Tree? tree = new Tree();
            foreach (int i in input)
            {
                tree.insertNode(i);
            }
            return tree;
        }

        private void displaySortedTree(Tree t)
        {
            Console.WriteLine("Printing tree in sorted order...");
            Console.WriteLine("--------------------------------");
            t.inOrderTraversal();
            Console.WriteLine("--------------------------------");
        }

        private void displayTreeStats(Tree t)
        {
            Console.WriteLine("\nDisplaying tree stats...");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Number of items: " + t.getNodeCount());
            Console.WriteLine("Number of levels: " + t.getDepth());
            Console.WriteLine("Theoretical min number of levels this tree could have: " + t.getMinDepth());
            Console.WriteLine("--------------------------------");
        }

        private void runApp()
        {
            int[] input = getUserInput();
            Tree t = createTree(input);
            displaySortedTree(t);
            displayTreeStats(t);
        }

        public App() => runApp(); 
    }
}
