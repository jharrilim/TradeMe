using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TradeMe
{
	[Serializable]
	public class Exchange
	{
		private Dictionary<Security, Security> securities; // rip c# hashset

		public List<Shareholder> Shareholders { get; }

		public List<Security> Securities { get => securities.Keys.ToList(); }

		[JsonProperty()]
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

		public void EnlistSecurity(params Security[] securities)
		{
			foreach(var s in securities)
				EnlistSecurity(s);
		}

		public void DelistSecurity(Security security)
		{
			throw new NotImplementedException();
		}

		internal void PlaceLimitOrder(LimitOrder order)
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

		public void AddShareholder(params Shareholder[] shareholders)
		{
			foreach (var s in shareholders)
			{
				AddShareholder(s);
			}
		}
    }
}
