using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TradeMe
{
    static class HashSetExtensions
    {
        public static T GetItem<T>(this HashSet<T> hset, T item)
        {
            return hset.Contains(item) ? item : throw new KeyNotFoundException();
        }
    }

	[Serializable]
	public class Exchange
	{
        private HashSet<Security> securities;

		public List<Shareholder> Shareholders { get; }

		public IEnumerable<Security> Securities { get => securities; }

		[JsonProperty]
		public string Name { get; }
		
		public Exchange()
		{
            securities = new HashSet<Security>();
			Shareholders = new List<Shareholder>();
		}

		public Exchange(string name) : this()
		{
			Name = name;
		}

		public Exchange(string name, IEnumerable<Security> securities)
		{
			Name = name;
			Shareholders = new List<Shareholder>();
			this.securities = new HashSet<Security>(securities);
		}

		public void EnlistSecurity(Security security)
		{
            securities.Add(security);
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
                    securities.GetItem(order.Security).AddAsk(order);
					break;
				case OrderType.Bid:
                    securities.GetItem(order.Security).AddBid(order);
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
