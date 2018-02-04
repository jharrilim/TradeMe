using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TradeMe
{
    [Serializable]
    internal class Transaction
    {
        [JsonProperty]
        internal Guid Guid { get; }

        [JsonProperty]
        internal Shareholder Buyer { get; }

        [JsonProperty]
        internal Shareholder Seller { get; }

        [JsonProperty()]
        internal Security Share { get; }

        [JsonProperty()]
        internal decimal Price { get; }

        [JsonProperty()]
        internal decimal Amount { get; }
        internal decimal TotalCost { get { return Amount * Price; } }

        internal Transaction(Shareholder buyer, Shareholder seller, Security share, decimal price, uint amount)
        {
            Guid = Guid.NewGuid();
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

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string ToJsonPretty()
        {
            return JsonConvert.SerializeObject
                (
                    this,
                    Formatting.Indented,
                    new JsonConverter[] { new StringEnumConverter() }
                );
        }
    }
}