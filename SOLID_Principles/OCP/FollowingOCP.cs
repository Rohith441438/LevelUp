using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCP
{
    public class Offer
    {
        public virtual int GetOffer(int amount)
        {
            return amount - (amount / 6);
        }
    }

    public class MonthlyOffer : Offer
    {
        public override int GetOffer(int amount)
        {
            return base.GetOffer(amount) - (amount/12);
        }
    }

    public class SeasonalOffer : Offer
    {
        public override int GetOffer(int amount)
        {
            return base.GetOffer(amount) - (amount/6);
        }
    }

    public class DasaraOffer : Offer
    {
        public override int GetOffer(int amount)
        {
            return base.GetOffer(amount) - (amount/3);
        }
    }
}
