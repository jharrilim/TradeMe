using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace TradeMe.Tests
{
    public class IntegrationTests
    {
        private readonly ITestOutputHelper output;

        public IntegrationTests(ITestOutputHelper testOutputHelper)
        {
            output = testOutputHelper;
        }

        private void Setup(out Exchange exchange, out Security security)
        {
            exchange = new Exchange("Test");
            security = new Security("Test", "TST");
            exchange.EnlistSecurity(security);
        }


        [Fact]
        public void PlacingAnOrder_ShouldMatch()
        {
            Exchange exchange;
            Security security;
            Setup(out exchange, out security);
            Shareholder sh1 = new Shareholder("TestFoo", 2000);
            Shareholder sh2 = new Shareholder("TestBar", 2000);
            exchange.AddShareholder(sh1, sh2);
            sh2.PlaceSellLimitOrder(exchange, security, 50, 20);
            sh1.PlaceBuyLimitOrder(exchange, security, 100, 10);
            security.MatchOrders();
            output.WriteLine(security.ReadLedger());  // Prints Transaction data
        }

        [Fact]
        public void PlacingAnOrder_ShouldNotMatch()
        {
            Exchange exchange;
            Security security;
            Setup(out exchange, out security);
            exchange.EnlistSecurity(security);
            Shareholder sh1 = new Shareholder("TestFoo", 2000);
            Shareholder sh2 = new Shareholder("TestBar", 2000);
            exchange.AddShareholder(sh1, sh2);
            sh2.PlaceSellLimitOrder(exchange, security, 500, 20);
            sh1.PlaceBuyLimitOrder(exchange, security, 100, 10);
            security.MatchOrders();
            output.WriteLine(security.ReadLedger()); // Prints
        }

        [Fact]
        public void CanPlaceOrder_OrderInformationIsTracked()
        {
         //   Exchange exchange;
         //   Security security;
         //   Setup(out Exchange, out security);
        }
    }
}
