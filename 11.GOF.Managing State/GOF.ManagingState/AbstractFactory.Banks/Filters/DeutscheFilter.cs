using AbstractFactory.Banks.Interfaces;
using AbstractFactory.Banks.Models;

namespace AbstractFactory.Banks.Filters
{
    public class DeutscheFilter : IFilter
    {
        public IEnumerable<Trade> Match(IEnumerable<Trade> trades, Country country)
        {
            return trades.Where(t => t.Type == TradeType.Option && t.SubType == TradeSubType.NewOption && t.Amount > 90 && t.Amount < 120);
        }
    }
}
