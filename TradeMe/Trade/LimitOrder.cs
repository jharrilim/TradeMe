using System;
using System.Collections.Generic;
using System.Text;
using TradeMe.Actor;

namespace TradeMe.Trade
{
	public enum OrderType { Ask, Bid }
	public class LimitOrder : Order
	{
		public OrderType OrderType { get; }
		public decimal Price { get; }

		public LimitOrder(Shareholder shareholder, Security security,
						  decimal price, uint amount, OrderType orderType)
			: base(shareholder, security, amount)
		{
			OrderType = orderType;
			Price = price;
		}

	}
}
