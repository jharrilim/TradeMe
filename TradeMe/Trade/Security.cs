using System;
using System.Collections.Generic;
using System.Text;

namespace TradeMe.Trade
{
    public class Security
    {
		private Ledger ledger;
		private OrderBook orderBook;

		public string Name { get; }
		public string Symbol { get; }
		public decimal MarketPrice { get; set; }

		public DateTime DateAdded { get; }
		public DateTime IPODate { get; private set; }

		private Security()
		{
			DateAdded = DateTime.Now;
			MarketPrice = 0;
			ledger = new Ledger();
			orderBook = new OrderBook();
		}
		public Security(string name, string symbol): this()
		{
			Name = name;
			Symbol = symbol;
		}

		public void MatchOrders()
		{
			if (orderBook.IsIntersecting)
			{
				var ask = orderBook.RemoveFirstAsk();
				var bid = orderBook.RemoveFirstBid();
			}
			//a.Order.Status = OrderStatus.Filled;
		}

		public void AddBid(LimitOrder order)
		{
			if (orderBook.Bids.ContainsKey(order.Price))
			{
				orderBook.Bids[order.Price].Add(order);
			}
			else
			{
				lock (orderBook.Bids)
				{
					var innerList = new List<LimitOrder>() { order };
					orderBook.Bids.Add(order.Price, innerList);
				}
			}

		}

		public void AddAsk(LimitOrder order)
		{
			if (orderBook.Asks.ContainsKey(order.Price))
			{
				orderBook.Asks[order.Price].Add(order);
			}
			else
			{
				lock (orderBook.Asks)
				{
					var innerList = new List<LimitOrder>() { order };
					orderBook.Asks.Add(order.Price, innerList);
				}
			}
		}

		public override string ToString()
		{
			return $"{Name} - ({Symbol})";
		}
	}
}
