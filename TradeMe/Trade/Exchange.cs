using System;
using System.Collections.Generic;
using System.Linq;
using TradeMe.Actor;

namespace TradeMe.Trade
{
	public class Exchange
	{
		private Dictionary<Security, Security> securities; // rip c# hashset

		public List<Shareholder> Shareholders { get; }

		public List<Security> Securities
		{
			get
			{
				return securities.Keys.ToList();
			}
		}

		public string Name { get; }
		
		public Exchange()
		{
			Shareholders = new List<Shareholder>();
			securities = new Dictionary<Security, Security>();
		}

		public Exchange(string name) : this()
		{
			Name = name;
		}
		public Exchange(string name, Dictionary<Security, Security> securities)
		{
			Name = name;
			Shareholders = new List<Shareholder>();
			this.securities = securities;
		}

		public void EnlistSecurity(Security security)
		{
			if (!securities.ContainsKey(security))
				securities[security] = security;
		}

		public void DelistSecurity(Security security)
		{
			throw new NotImplementedException();
		}

		public void PlaceLimitOrder(LimitOrder order) // throws KeyNotFoundException, InvalidOperationException
		{
			switch (order.OrderType)
			{
				case OrderType.Ask:
					securities[order.Security].AddAsk(order);
					break;
				case OrderType.Bid:
					securities[order.Security].AddBid(order);
					break;
				default:
					throw new InvalidOperationException("OrderType must be set to Bid or Ask.");
			}
		}

		public void AddShareholder(Shareholder shareholder) => Shareholders.Add(shareholder);
    }
}
