using AbstractFactory.Banks.Filters;
using AbstractFactory.Banks.Models;

namespace AbstractFactory.Banks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var trades = new List<Trade>
            {
                new() { Name = "BarclaysEnglandTrade", Amount = 110, Type = TradeType.Future, SubType = TradeSubType.NewOption },
                new() { Name = "BarclaysUsaTrade", Amount = 55, Type = TradeType.Option, SubType = TradeSubType.NyOption },
                new() { Name = "BofaTrade", Amount = 75, Type = TradeType.Future, SubType = TradeSubType.NyOption },
                new() { Name = "ConnacordTrade", Amount = 35, Type = TradeType.Future, SubType = TradeSubType.NewOption },
                new() { Name = "DeutscheTrade", Amount = 95, Type = TradeType.Option, SubType = TradeSubType.NewOption }
            };

            var tradeFilter = new TradeFilter();

            var filteredTradesForBarclaysEngland = tradeFilter.FilterForBank(trades, Bank.Barclays, Country.England);
            Console.WriteLine($"Number of trades for Barclays in England: {filteredTradesForBarclaysEngland.Count()}");
            foreach (var trade in filteredTradesForBarclaysEngland)
            {
                Console.WriteLine($"Trade name - {trade.Name}");
            }

            var filteredTradesForBarclaysUsa = tradeFilter.FilterForBank(trades, Bank.Barclays, Country.USA);
            Console.WriteLine($"Number of trades for Barclays in USA: {filteredTradesForBarclaysUsa.Count()}");
            foreach (var trade in filteredTradesForBarclaysUsa)
            {
                Console.WriteLine($"Trade name - {trade.Name}");
            }

            var filteredTradesForBofa = tradeFilter.FilterForBank(trades, Bank.Bofa, Country.USA);
            Console.WriteLine($"Number of trades for BOFA: {filteredTradesForBofa.Count()}");
            foreach (var trade in filteredTradesForBofa)
            {
                Console.WriteLine($"Trade name - {trade.Name}");
            }

            var filteredTradesForConnacord = tradeFilter.FilterForBank(trades, Bank.Connacord, Country.USA);
            Console.WriteLine($"Number of trades for Connacord: {filteredTradesForConnacord.Count()}");
            foreach (var trade in filteredTradesForConnacord)
            {
                Console.WriteLine($"Trade name - {trade.Name}");
            }

            var filteredTradesForDeutsche = tradeFilter.FilterForBank(trades, Bank.Deutsche, Country.England);
            Console.WriteLine($"Number of trades for Deutsche: {filteredTradesForDeutsche.Count()}");
            foreach (var trade in filteredTradesForDeutsche)
            {
                Console.WriteLine($"Trade name - {trade.Name}");
            }
        }
    }
}
