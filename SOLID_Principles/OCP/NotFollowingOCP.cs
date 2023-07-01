using System;

namespace OCP
{
    public class NotFollowingOCP
    {
        //Here this class is not following OCP for the below reasons
        //Here  if the business want to extend the offers,  i.e adding Dasara Offer to the list, then we need to change the method and do the testing related to all the mehtods that depends on this method and need to inform QA to test the dependent places, which is hectic thing
        // and one method is dealing multiple things, which violated single responsiblity principle
        public int getOffer(int amount, Offers offer)
        {
            if(offer == Offers.MonthlySale) 
            {
                return amount - (amount / 4);
            }
            else if(offer == Offers.SeasonalSale)
            {
                return amount - (amount / 3);
            }
            return amount - (amount / 6);
        }
    }

    public enum Offers
    {
        MonthlySale,
        SeasonalSale
    }
}