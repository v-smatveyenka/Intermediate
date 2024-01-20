using AbstractFactory.Banks.Interfaces;
using AbstractFactory.Banks.Models;

namespace AbstractFactory.Banks.Filters
{
    public class BarclaysFilter : IFilter
    {
        public IEnumerable<Trade> Match(IEnumerable<Trade> trades, Country country)
        {
            switch (country)
            {
                case Country.USA:
                    return trades.Where(t => t.Type == TradeType.Option && t.SubType == TradeSubType.NyOption && t.Amount > 50);
                case Country.England:
                    return trades.Where(t => t.Type == TradeType.Future && t.Amount > 100);
                default:
                    throw new NotImplementedException("Unsupported country");
            }
        }
    }
}
