using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TradeMe.Actor;

namespace TradeMe.Trade
{
    [Serializable]
    public class Security
    {
        private readonly Ledger ledger;

        private readonly OrderBook orderBook;

        [JsonProperty]
        public string Name { get; }

        [JsonProperty]
        public string Symbol { get; }

        [JsonIgnore]
        public decimal MarketQuote { get { return ledger.MostRecentPrice; } }

        [JsonProperty]
        public DateTime DateAdded { get; }
        public DateTime IPODate { get; private set; }

        private Security()
        {
            DateAdded = DateTime.Now;
            ledger = new Ledger(this);
            orderBook = new OrderBook();
        }
        public Security(string name, string symbol) : this()
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
                    uint aRemainder = ask.Fill(ask.Amount);
                    uint bRemainder = bid.Fill(ask.Amount);
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

        public string ReadLedger()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var transaction in ledger.Transactions)
            {
                sb.Append(transaction.ToJson());
                sb.Append(',');
            }
            return sb.ToString();
        }

        private class Ledger
        {
            private readonly Stack<Transaction> transactions;
            private readonly Security security;

            internal IEnumerable<Transaction> Transactions { get => new Stack<Transaction>(transactions); }

            internal decimal MostRecentPrice
            {
                get
                {
                    return transactions.Peek().Price;
                }
            }

            internal Ledger(Security security)
            {
                this.security = security;
                transactions = new Stack<Transaction>();
            }

            internal void AddTransaction(Shareholder buyer, Shareholder seller, decimal price, uint amount)
            {
                transactions.Push(new Transaction(buyer, seller, security, price, amount));
            }
        }
    }
}
