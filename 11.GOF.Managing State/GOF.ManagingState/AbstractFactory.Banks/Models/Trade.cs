namespace AbstractFactory.Banks.Models
{
    public class Trade
    {
        public string Name { get; set; }

        public TradeType Type { get; set; }

        public TradeSubType SubType { get; set; }

        public int Amount { get; set; }
    }
}
