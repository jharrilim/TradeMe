using System;
using TradeMe.Actor;
using TradeMe.Trade;
using Xunit;

namespace TradeMe.Tests
{
    public class ExchangeTests
    {
        [Fact]
        public void PlacingAnOrder_IntegrationTest()
        {
            Exchange exchange = new Exchange("Test");
            Security security = new Security("Test", "TST");
            exchange.EnlistSecurity(security);
            Shareholder sh1 = new Shareholder("TestFoo", 2000);
            Shareholder sh2 = new Shareholder("TestBar", 2000);
            exchange.AddShareholder(sh1, sh2);
            sh2.PlaceSellLimitOrder(exchange, security, 50, 20);
            sh1.PlaceBuyLimitOrder(exchange, security, 100, 10);
            security.MatchOrders();

            System.Diagnostics.Debug.WriteLine(security.ReadLedger());
        }
    }
}
