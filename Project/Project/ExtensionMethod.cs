using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class ExtensionMethod
    {
        public static void CheckExtensionMethod()
        {
            ClassToBeExtended classToBeExtended = new ClassToBeExtended();
            classToBeExtended.SayIamExtension();
        }
    }

    static class ExtensionClass
    {
        public static void SayIamExtension(this ClassToBeExtended ex)
        {
            Console.WriteLine("Yes I am extension Method");
        }
    }
    class ClassToBeExtended
    {
        public void SayHai()
        {
            Console.WriteLine("Say Hai");
        }
    }
}
