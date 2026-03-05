// See https://aka.ms/new-console-template for more information
using System;

class Program
{
    static void Main (string[] args)
    {
        int marks = 85;
        Console.WriteLine("Marks" + marks);
        object objmarks = marks;               //Boxing
        Console.WriteLine("Object Marks" + objmarks);
        int unboxmarks = (int)objmarks;       //Unboxing
        Console.WriteLine("Unboxed Marks" + unboxmarks);
        unboxmarks = unboxmarks + 5;
        Console.WriteLine("Modified Unboxed Marks" + unboxmarks);
    }
}
