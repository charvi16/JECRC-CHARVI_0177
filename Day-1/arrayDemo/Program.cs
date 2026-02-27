// See https://aka.ms/new-console-template for more information
using System;

class Program
{
    static void Main (string[] args)
    {
        int []intArray = new int[5] {34, 23, 41, 89, 77};
        Array.Sort(intArray);
        foreach (int i in intArray)
        {
            Console.Write(i + " ");
        }
    }
}
