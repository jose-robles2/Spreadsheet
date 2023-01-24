using System;
using Homework_1.MyTree;

namespace Homework_1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tree t = new Tree();
            t.insertNode(10); 
            t.insertNode(5); 
            t.insertNode(15); 
            t.insertNode(7); 
            t.insertNode(12); 
            t.insertNode(3); 
            t.insertNode(17); 
        }
    }
}