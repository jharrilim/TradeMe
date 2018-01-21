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
				LimitOrder ask = orderBook.PeekAsk();
				LimitOrder bid = orderBook.PeekBid();
				uint least = ask.Amount < bid.Amount ? ask.Amount : bid.Amount;
				uint aRemainder = ask.Fill(least);
				uint bRemainder = bid.Fill(least);
				if (aRemainder == 0)
					orderBook.RemoveAsk(ask);
				if (bRemainder == 0)
					orderBook.RemoveBid(bid);
				ledger.AddTransaction(bid.Shareholder, ask.Shareholder, bid.Price, least);
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
