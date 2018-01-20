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
		public decimal MarketQuote { get { return ledger.MostRecentPrice; } }
		public DateTime DateAdded { get; }
		public DateTime IPODate { get; private set; }

		private Security()
		{
			DateAdded = DateTime.Now;
			ledger = new Ledger(this);
			orderBook = new OrderBook();
		}
		public Security(string name, string symbol): this()
		{
			Name = name;
			Symbol = symbol;
		}

		public void MatchOrders()
		{
			if (orderBook.BidQuote == orderBook.AskQuote)
			{
				var ask = orderBook.RemoveFirstAsk();
				var bid = orderBook.RemoveFirstBid();
				uint askRemaining = ask.Amount - bid.Amount;
				// TODO
				//ask.Fill(bid.Amount);
				if (askRemaining > 0)
				{
					//LimitOrder remainderAsk = new LimitOrder(ask.Shareholder, this, ask.Price, askRemaining, OrderType.Ask);
					AddAsk(ask);
				}
				ledger.AddTransaction(bid.Shareholder, ask.Shareholder, bid.Price, bid.Amount);
			}
			else if (orderBook.BidQuote > orderBook.AskQuote)
			{
				var ask = orderBook.RemoveFirstAsk();
				var bid = orderBook.RemoveFirstBid();
				if (ask.DateCreated < bid.DateCreated)
				{
					ledger.AddTransaction(bid.Shareholder, ask.Shareholder, ask.Price, ask.Amount);
				}
			}
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
