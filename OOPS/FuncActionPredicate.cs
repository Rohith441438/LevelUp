using System;

public class HelloWorld
{
    public static Func<int,int,int> add = (a,b) => a+b;
    public static Action<int> Do = (number) => Console.WriteLine(number);
    public static Predicate<int> IsEven = (number) => number%2 == 0;
    public static void Main(string[] args)
    {
        Console.Write(HelloWorld.add(2,3));
        HelloWorld.Do(10);
        Console.WriteLine("The Give number is a "+HelloWorld.IsEven(10));
    }
}