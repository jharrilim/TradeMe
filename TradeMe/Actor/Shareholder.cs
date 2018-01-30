using System;
using System.Collections.Generic;
using System.Net.Mail;
using TradeMe.Trade;

namespace TradeMe.Actor
{
    public class Shareholder
    {
		private readonly Dictionary<Security, long> securities;
		private readonly List<Order> openOrders;
		private readonly List<Order> closedOrders;

		public string Name { get; }
		public decimal Balance { get; private set; }

		public Shareholder(string name)
		{
			Name = name;
			securities = new Dictionary<Security, long>();
			openOrders = new List<Order>();
			closedOrders = new List<Order>();
		}

		public Shareholder(string name, decimal initialBalance) : this(name)
		{
			Balance = initialBalance;
		}

		public void PlaceBuyLimitOrder(Exchange exchange, Security security, decimal price, uint amount)
		{
			var order = new LimitOrder(this, security, price, amount, OrderType.Bid);
			order.LimitOrderFilled += OnLimitOrderFilled;
			exchange.PlaceLimitOrder(order);
			openOrders.Add(order);
			
		}
		
		public void PlaceSellLimitOrder(Exchange exchange, Security security, decimal price, uint amount)
		{
			var order = new LimitOrder(this, security, price, amount, OrderType.Ask);
			exchange.PlaceLimitOrder(order);
			openOrders.Add(order);
		}

		public void PlaceBuyMarketOrder()
		{
			throw new NotImplementedException();
		}

		public void PlaceSellMarketOrder()
		{
			throw new NotImplementedException();
		}

		private void OnLimitOrderFilled(LimitOrderFilledArgs a)
		{
			a.Order.LimitOrderFilled -= OnLimitOrderFilled;
			openOrders.Remove(a.Order);
			closedOrders.Add(a.Order);
			securities[a.Order.Security] += a.Order.OrderType == OrderType.Ask ? a.Order.Amount * -1 : a.Order.Amount;
		}
    }
}
