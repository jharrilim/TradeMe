using System;
using System.Collections.Generic;
using System.Text;
using TradeMe.Actor;

namespace TradeMe.Trade
{
    internal class Ledger
    {
		private readonly Stack<Transaction> transactions;
		private readonly Security security;

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
