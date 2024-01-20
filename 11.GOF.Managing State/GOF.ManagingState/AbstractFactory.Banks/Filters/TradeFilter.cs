using AbstractFactory.Banks.Interfaces;
using AbstractFactory.Banks.Models;

namespace AbstractFactory.Banks.Filters
{
    public class TradeFilter
    {
        public IEnumerable<Trade> FilterForBank(IEnumerable<Trade> trades, Bank bank, Country country)
        {
            IFilter filter = FilterFactory.CreateFilter(bank);
            return filter.Match(trades, country);
        }
    }
}
