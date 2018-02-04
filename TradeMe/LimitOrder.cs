using System;

namespace TradeMe
{
    public enum OrderType { Ask, Bid }
    public class LimitOrder : Order
    {
        public delegate void LimitOrderFilledHandler(LimitOrderFilledArgs a);
        public event LimitOrderFilledHandler LimitOrderFilled;

        public OrderType OrderType { get; }
        public decimal Price { get; }

        public override OrderStatus Status
        {
            get { return base.Status; }
            set
            {
                base.Status = value;
                if (base.Status == OrderStatus.Filled && LimitOrderFilled != null)
                    LimitOrderFilled(new LimitOrderFilledArgs(this));
            }
        }

        public LimitOrder(Shareholder shareholder, Security security,
                          decimal price, uint amount, OrderType orderType)
            : base(shareholder, security, amount)
        {
            OrderType = orderType;
            Price = price;
        }
    }

    public class LimitOrderFilledArgs : EventArgs
    {
        public LimitOrder Order { get; }
        public DateTime DateFilled { get; }

        public LimitOrderFilledArgs(LimitOrder order) : base()
        {
            Order = order;
            DateFilled = DateTime.Now;
        }
    }
}
