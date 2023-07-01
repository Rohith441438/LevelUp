using System;

namespace ISP
{
    //Interface Segregation principle says that we should not force client to implement unwanted or unsued methods.instead of a big fat interface we should have multiple smaller interfaces
    //The below inerface is violating the ISP

    public interface IPrinter
    {
        string Print();
        string Scan();
        string Fax();
        string PrintDuplex();
    }

    //Here HpLasetjetPrinter needs all the operation or methods that are provided by teh interface IPrinter, so we are implemening it.
    public class HpLasetJetPrinter : IPrinter
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

    //but LiquidInkJetPrinter dont need Fax and PrintDuplex but as we are implementing teh Iprinter we are forcing LiquidInkJetPrinter to use those methods, but as per the requirement they will be unused forever, so solve this issue we should follow ISP.
    public class LiquidInkJetPrinter : IPrinter
    {
        public string Print()
        {
            return "Printing";
        }        

        public string Scan()
        {
            return "Scanning";
        }
        public string PrintDuplex()
        {
            throw new NotImplementedException();
        }

        public string Fax()
        {
            throw new NotImplementedException();
        }
    }
    public class NotFollowingISP
    {
    }
}
