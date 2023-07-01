using System;

namespace LSP
{
    public class NotFollowingLSP
    {
        //LSP states that, if S is a subtype of T, then objects of T should be replaced with the objects of type S.
        public void CheckLSP()
        {
            Apple apple = new Orange();
            Console.WriteLine("Apple color is " + apple.GetColor());//Here we are going to get orange as the color, its violating LSP as derived class is not successfully replaced base class without effecting any errors.
        }
    }

    public class Apple
    {
        public  virtual string GetColor(){
            return "Red";
        }
    }

    public class Orange : Apple
    {
        public override string GetColor()
        {
            return "Orange";
        }
    }
}
