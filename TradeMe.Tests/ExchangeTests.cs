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
			exchange.EnlistSecurity(new Security("Test", "TST"));
			Shareholder sh1 = new Shareholder("TestFoo", 2000);
			Shareholder sh2 = new Shareholder("TestBar", 2000);
			exchange.AddShareholder(sh1, sh2);
			
        }
    }
}
