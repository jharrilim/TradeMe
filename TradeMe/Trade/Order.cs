using System;
using System.Collections.Generic;
using System.Text;
using TradeMe.Actor;

namespace TradeMe.Trade
{
	public enum OrderStatus { Open, Filled, Cancelled, Partial }
	public abstract class Order
	{
		private OrderStatus status;
		public OrderStatus Status
		{
			get { return status; }
			set
			{
				status = value;
				if (status == OrderStatus.Filled)
					OrderFilled(new OrderFilledArgs(this));
			}
		}
		public DateTime DateCreated { get; }
		public Security Security { get; }
		public uint Amount { get; }
		public Shareholder Shareholder { get; }

		public delegate void OrderFilledHandler(OrderFilledArgs a);
		public event OrderFilledHandler OrderFilled;

		public Order(Shareholder shareholder, Security security, uint amount)
		{
			Shareholder = shareholder;
			DateCreated = DateTime.Now;
			Security = security;
			Amount = amount;
			Status = OrderStatus.Open;
		}
	}

	public class OrderFilledArgs : EventArgs
	{
		public Order Order { get; }
		public DateTime DateFilled { get; }

		public OrderFilledArgs(Order order) : base()
		{
			Order = order;
			DateFilled = DateTime.Now;
		}
	}
}
