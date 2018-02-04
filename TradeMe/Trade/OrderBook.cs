using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace TradeMe.Trade
{
    internal class OrderBook
    {
        public SortedList<decimal, List<LimitOrder>> Bids { get; }

        public SortedList<decimal, List<LimitOrder>> Asks { get; }

        public decimal BidQuote { get { return Bids.First().Key; } }

        public decimal AskQuote { get { return Asks.First().Key; } }

        public decimal Spread { get { return Asks.First().Key - Bids.First().Key; } }

        public OrderBook()
        {
            Bids = new SortedList<decimal, List<LimitOrder>>(new BidComparer());
            Asks = new SortedList<decimal, List<LimitOrder>>();
        }

        public LimitOrder PeekBid() => Bids.First().Value.First();

        public LimitOrder PeekAsk() => Asks.First().Value.First();

        internal bool RemoveAsk(LimitOrder order)
        {
            return Asks.First().Value.Remove(order);
        }

        internal bool RemoveBid(LimitOrder order)
        {
            return Bids.First().Value.Remove(order);
        }

        public LimitOrder RemoveFirstBid()
        {
            lock (Bids)
            {
                LimitOrder bid = Bids.First().Value.First();
                Bids.First().Value.RemoveAt(0);
                if (Bids.First().Value.Count == 0)
                {
                    Bids.RemoveAt(0);
                }
                return bid;
            }
        }

        public LimitOrder RemoveFirstAsk()
        {
            lock (Asks)
            {
                LimitOrder ask = Asks.First().Value.First();
                Asks.First().Value.RemoveAt(0);
                if (Asks.First().Value.Count == 0)
                {
                    Asks.RemoveAt(0);
                }
                return ask;
            }
        }

        private class BidComparer : IComparer<decimal>
        {
            public int Compare(decimal x, decimal y)
            {
                return Comparer<decimal>.Default.Compare(y, x);
            }
        }
    }
}