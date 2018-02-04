using System;
using System.Diagnostics;
using TradeMe.Actor;
using TradeMe.Trade;
using Xunit;

namespace TradeMe.Tests
{

    public class TransactionTests
    {
		[Fact]
		public void ToJson_WillSerialize()
		{
			Shareholder sh1 = new Shareholder("John");
			Shareholder sh2 = new Shareholder("Mary");
			Security sec = new Security("FOOBAR Inc.", "FBR");
			Transaction transaction = new Transaction(sh1, sh2, sec, 124.76m, 100);
			var json = transaction.ToJson();
			Assert.NotEqual("{}", json);
			Assert.NotEqual("{},", json);
			Debug.WriteLine(json);
		}

		[Fact]
		public void ToJsonPretty_WillSerialize()
		{
			Shareholder sh1 = new Shareholder("John");
			Shareholder sh2 = new Shareholder("Mary");
			Security sec = new Security("FOOBAR Inc.", "FBR");
			Transaction transaction = new Transaction(sh1, sh2, sec, 124.76m, 100);
			var json = transaction.ToJsonPretty();
			Assert.NotEqual("{}", json);
			Assert.NotEqual("{},", json);
			Assert.NotEqual(transaction.ToJson(), json);
			Debug.WriteLine(json);
		}
    }
}
