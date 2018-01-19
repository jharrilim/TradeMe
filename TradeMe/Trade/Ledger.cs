using System;
using System.Collections.Generic;
using System.Text;
using TradeMe.Actor;

namespace TradeMe.Trade
{
    internal class Ledger
    {
		private List<Transaction> transactions;

		internal Ledger()
		{
			transactions = new List<Transaction>();
		}

		internal void AddTransaction(Shareholder buyer, Shareholder seller, Security share, decimal price, int amount)
		{
			transactions.Add(new Transaction(buyer, seller, share, price, amount));
		}
    }
}
