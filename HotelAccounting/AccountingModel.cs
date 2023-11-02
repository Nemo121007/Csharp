using System;

namespace HotelAccounting
{
    //создайте класс AccountingModel здесь
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;

        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0) 
                    throw new ArgumentException();
                price = value;
                Notify(nameof(Price));
                Notify(nameof(Total));
            }
        }

        public int NightsCount
        {
            get { return nightsCount; }
            set
            {
                if (value <= 0) 
                    throw new ArgumentException();
                
                nightsCount = value;
                
                Notify(nameof(NightsCount));
                Notify(nameof(Total));
            }
        }

        public double Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                total = price * nightsCount * (1 - discount / 100);

                if (total < 0) 
                    throw new ArgumentException();
                
                Notify(nameof(Discount));
                Notify(nameof(Total));
            }
        }

        public double Total
        {
            get { return price * nightsCount * (1 - discount / 100); }
            set
            {
                if (value <= 0) 
                    throw new ArgumentException();
                
                total = value;
                discount = 100 * (1 - total / (price * nightsCount));

                Notify(nameof(Total));
                Notify(nameof(Discount));
            }
        }
    }
}
