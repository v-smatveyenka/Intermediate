using AbstractFactory.Banks.Models;

namespace AbstractFactory.Banks.Interfaces
{
    public interface IFilter
    {
        IEnumerable<Trade> Match(IEnumerable<Trade> trades, Country country);
    }
}
