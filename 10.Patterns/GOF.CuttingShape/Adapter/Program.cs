namespace Adapter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var elements = new Elements<string>(new[] { "item_1", "item_2", "item_3" });
            var container = new ContainerAdapter<string>(elements);
            var printer = new Printer();
            printer.Print(container);
        }
    }
}
