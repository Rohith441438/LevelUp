using System;

public class HelloWorld
{
    public delegate string AddDelegate(string s1, string s2);
    public static void Main(string[] args)
    {
        //HelloWorld hw = new HelloWorld();
        
        AddDelegate ad = new AddDelegate(HelloWorld.Method1);
        ad += new AddDelegate(HelloWorld.Method2);
        
        Console.WriteLine(ad("Rohtih","vinay"));
    }
    public static string Method1(string s1, string s2){
        return s1+s2;
    }
    
    public static string Method2(string s1, string s2){
        return "\n" + s1+ "" +s2;
    }
}