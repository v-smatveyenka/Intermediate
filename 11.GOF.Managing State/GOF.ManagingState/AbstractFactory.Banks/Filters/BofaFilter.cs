using AbstractFactory.Banks.Interfaces;
using AbstractFactory.Banks.Models;

namespace AbstractFactory.Banks.Filters
{
    public class BofaFilter : IFilter
    {
        public IEnumerable<Trade> Match(IEnumerable<Trade> trades, Country country)
        {
            return trades.Where(t => t.Amount > 70);
        }
    }
}
