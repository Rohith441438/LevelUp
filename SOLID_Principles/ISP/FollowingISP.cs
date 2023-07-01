using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP
{
    //Here to implement ISP we are going to break IPrinter to 3 small interfaces
    public interface IPrinterTasks
    {
        string Print();
        string Scan();
    }

    public interface IFaxTasks
    {
        string Fax();
    }

    public interface IPrintDuplexTasks
    {
        string PrintDuplex();
    }

    public class HpLaserJetPrinter : IPrinterTasks, IFaxTasks, IPrintDuplexTasks
    {
        public string Fax()
        {
            return "Faxing";
        }

        public string Print()
        {
            return "Printing";
        }

        public string PrintDuplex()
        {
            return "Printing Duplex";
        }

        public string Scan()
        {
            return "Scanning";
        }
    }

    public class LiquidJetPrinter : IPrinterTasks
    {
        public string Print()
        {
            return "Printing";
        }

        public string Scan()
        {
            return "Scanning";
        }
    }
    class FollowingISP
    {
    }
}
