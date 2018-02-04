using System;

namespace TradeMe
{
    public enum OrderStatus { Open, Filled, Cancelled, Partial }
    public abstract class Order
    {
        public virtual OrderStatus Status { get; set; }
        public DateTime DateCreated { get; }
        public Security Security { get; }
        public uint Amount { get; private set; }
        public Shareholder Shareholder { get; }

        public Order(Shareholder shareholder, Security security, uint amount)
        {
            Shareholder = shareholder;
            DateCreated = DateTime.Now;
            Security = security;
            Amount = amount;
            Status = OrderStatus.Open;
        }

        public uint Fill(uint amount)
        {
            if (Amount < amount)
            {
                Amount = 0;
                Status = OrderStatus.Filled;
                return amount - Amount;
            }
            else if (Amount < amount)
            {
                Amount = Amount - amount;
                Status = OrderStatus.Partial;
                return 0;
            }
            else
            {
                Amount = 0;
                Status = OrderStatus.Filled;
                return 0;
            }
        }
    }
}
