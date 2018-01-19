using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace TradeMe.Trade
{
	public class OrderBook
	{
		public SortedList<decimal, List<LimitOrder>> Bids { get; }

		public SortedList<decimal, List<LimitOrder>> Asks { get; }

		public bool IsIntersecting
		{
			get
			{
				return Asks.First().Key <= Bids.First().Key;
			}
		}

		public OrderBook()
		{
			Bids = new SortedList<decimal, List<LimitOrder>>();
			Asks = new SortedList<decimal, List<LimitOrder>>();
		}

		public Order RemoveFirstBid()
		{
			lock (Bids)
			{
				Order bid = Bids.First().Value.First();
				Bids.First().Value.RemoveAt(0);
				if (Bids.First().Value.Count == 0)
				{
					Bids.RemoveAt(0);
				}
				return bid;
			}
		}

		public Order RemoveFirstAsk()
		{
			lock (Asks)
			{
				Order ask = Asks.First().Value.First();
				Asks.First().Value.RemoveAt(0);
				if (Asks.First().Value.Count == 0)
				{
					Asks.RemoveAt(0);
				}
				return ask;
			}
		}


	}
}