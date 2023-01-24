using System;
using Homework_1.MyTree;

namespace Homework_1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        /*
            Get user input from user, assume input is entered correctly EVERY time. Read input and 
            parse into an array of strings, then into an array of ints using CovertAll() method
         */
        static int[] getUserInput()
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
        static Tree? createTree(int[] input)
        {
            Tree? tree = new Tree();
            foreach (int i in input)
            {
                tree.insertNode(i);
            }
            return tree; 
        }

        static void displaySortedTree(Tree t)
        {
            Console.WriteLine("Printing tree in sorted order..."); 
            Console.WriteLine("--------------------------------");
            t.inOrderTraversal();
            Console.WriteLine("--------------------------------");
        }

        static void displayTreeStats(Tree t)
        {
            Console.WriteLine("\nDisplaying tree stats...");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Number of items: " + t.getNodeCount()); 
            Console.WriteLine("--------------------------------");
        }
        static void Main(string[] args)
        {
            int[] input = getUserInput();
            Tree t = createTree(input);
            displaySortedTree(t);
            displayTreeStats(t); 
        }
    }
}