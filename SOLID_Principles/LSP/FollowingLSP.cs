using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSP
{
    class FollowingLSP
    {
        public void CheckLSP()
        {
            Fruit fruit = new AppleClass();
            Console.WriteLine("color of Apple is " + fruit.GetColor());
            fruit = new OrangeClass();
            Console.WriteLine("Color of Orange is " + fruit.GetColor());
        }
    }

    public interface Fruit
    {
        string GetColor();
    }

    public class AppleClass : Fruit
    {
        public string GetColor()
        {
            return "Red";
        }
    }

    public class OrangeClass : Fruit
    {
        public string GetColor()
        {
            return "Orange";
        }
    }
}
