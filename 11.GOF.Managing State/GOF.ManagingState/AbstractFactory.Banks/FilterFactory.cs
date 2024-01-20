using AbstractFactory.Banks.Filters;
using AbstractFactory.Banks.Interfaces;
using AbstractFactory.Banks.Models;

namespace AbstractFactory.Banks
{
    public static class FilterFactory
    {
        public static IFilter CreateFilter(Bank bank)
        {
            switch (bank)
            {
                case Bank.Bofa:
                    return new BofaFilter();
                case Bank.Connacord:
                    return new ConnacordFilter();
                case Bank.Barclays:
                    return new BarclaysFilter();
                case Bank.Deutsche:
                    return new DeutscheFilter();
                default:
                    throw new NotImplementedException("Unsupported bank");
            }
        }
    }
}
