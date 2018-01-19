using System;
using TradeMe.Actor;

namespace TradeMe.Trade
{
	internal class Transaction
	{
		internal Guid guid { get; }
		internal Shareholder Buyer { get; }
		internal Shareholder Seller { get; }
		internal Security Share { get; }
		internal decimal Price { get; }
		internal decimal Amount { get; }
		internal decimal TotalCost { get { return Amount * Price; } }

		internal Transaction(Shareholder buyer, Shareholder seller, Security share, decimal price, decimal amount)
		{
			guid = Guid.NewGuid();
			Buyer = buyer;
			Seller = seller;
			Share = share;
			Price = price;
			Amount = amount;
		}

		public override string ToString()
		{
			return $"{Buyer.Name} bought {Share.Name}({Share.Symbol}) from {Seller.Name} for {Price:c2} each.";
		}
	}
}