using HomeworkOne.MyTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkOne
{
    /// <summary>
    /// Class: App
    /// Instantiated from main, contains driving code for the application's lifetime. First asks for user
    /// input, then creates a Tree and calls Tree functions to product output onto the console. 
    /// </summary>
    internal class App
    {
        /// <summary>
        /// Default constructor 
        /// </summary>
        public App() => RunApp();
        
        /// <summary>
        /// Driving code to run the app
        /// </summary>
        /// <returns> void </returns>
        private void RunApp()
        {
            int[] input = GetUserInput();
            Tree t = CreateTree(input);
            DisplaySortedTree(t);
            DisplayTreeStats(t);
        }

        /// <summary>
        /// Get user input from user, assume input is entered correctly EVERY time. Read input and
        /// parse into an array of strings, then into an array of ints using ConvertAll() method
        /// </summary>
        /// <param></param>
        /// <returns> Integer array containing user input </returns>
        private int[] GetUserInput()
        {
            Console.WriteLine("Enter a list of numbers in the range [0, 100] separated by SINGLE spaces");
            string input = Console.ReadLine();
            string[] strArray = input.Split(" ");
            return (Array.ConvertAll(strArray, s => int.Parse(s)));
        }

        /// <summary>
        /// Create a BST from an array of integers. Utilize public interface InsertNode() to 
        /// easily insert ints into the tree
        /// </summary>
        /// <param name="input"> int array containing the initial user input</param>
        /// <returns> Nullable type Tree object that represents a BST of the user input </returns>
        private Tree? CreateTree(int[] input)
        {
            Tree? tree = new Tree();
            foreach (int i in input)
            {
                tree.InsertNode(i);
            }
            return tree;
        }

        /// <summary>
        /// Display the int values in the Node's of the BST in ascending order
        /// </summary>
        /// <param name="t"> Tree object created from user input</param>
        /// <returns> void </returns>
        private void DisplaySortedTree(Tree t)
        {
            Console.WriteLine("Printing tree in sorted order...");
            Console.WriteLine("--------------------------------");
            t.InOrderTraversal();
            Console.WriteLine("--------------------------------");
        }

        /// <summary>
        /// Display stats about the tree - node count, depth count, and minimum height
        /// </summary>
        /// <param name="t"> Tree object created from user input</param>
        /// <returns> void </returns>
        private void DisplayTreeStats(Tree t)
        {
            Console.WriteLine("\nDisplaying tree stats...");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Number of items: " + t.GetNodeCount());
            Console.WriteLine("Number of levels: " + t.GetDepth());
            Console.WriteLine("Theoretical min number of levels this tree could have: " + t.GetMinDepth());
            Console.WriteLine("--------------------------------");
        }
    }
}
