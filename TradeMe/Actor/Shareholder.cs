using System;
using System.Collections.Generic;
using System.Net.Mail;
using TradeMe.Trade;

namespace TradeMe.Actor
{
    public class Shareholder
    {
		public string Name { get; set; }
		public decimal Balance { get; private set; }
		private Dictionary<Security, ulong> Securities { get; set; }
		private List<Order> OpenOrders { get; set; }
		private List<Order> ClosedOrders { get; set; }

		public Shareholder(string name)
		{
			Name = name;
			Securities = new Dictionary<Security, ulong>();
			OpenOrders = new List<Order>();
			ClosedOrders = new List<Order>();
		}

		public Shareholder(string name, decimal initialBalance) : this(name)
		{
			Balance = initialBalance;
		}

		public void PlaceBuyLimitOrder(Exchange exchange, Security security, decimal price, uint amount)
		{
			var order = new LimitOrder(this, security, price, amount, OrderType.Bid);
			order.OrderFilled += OnOrderFilled;
			exchange.PlaceLimitOrder(order);
			OpenOrders.Add(order);
			
		}
		
		public void PlaceSellLimitOrder(Exchange exchange, Security security, decimal price, uint amount)
		{
			var order = new LimitOrder(this, security, price, amount, OrderType.Ask);
			exchange.PlaceLimitOrder(order);
			OpenOrders.Add(order);
		}

		public void PlaceBuyMarketOrder()
		{
		}

		public void PlaceSellMarketOrder()
		{

		}

		private void OnOrderFilled(OrderFilledArgs a)
		{
			// Do stuff
			a.Order.OrderFilled -= OnOrderFilled;
		}
    }
}
