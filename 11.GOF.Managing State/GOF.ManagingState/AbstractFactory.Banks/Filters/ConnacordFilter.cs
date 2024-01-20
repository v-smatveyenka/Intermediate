using AbstractFactory.Banks.Interfaces;
using AbstractFactory.Banks.Models;

namespace AbstractFactory.Banks.Filters
{
    public class ConnacordFilter : IFilter
    {
        public IEnumerable<Trade> Match(IEnumerable<Trade> trades, Country country)
        {
            return trades.Where(t => t.Type == TradeType.Future && t.Amount > 10 && t.Amount < 40);
        }
    }
}
