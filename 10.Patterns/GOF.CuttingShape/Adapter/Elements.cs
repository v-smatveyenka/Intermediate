namespace Adapter
{
    public class Elements<T> : IElements<T>
    {
        private readonly IEnumerable<T> _items;

        public Elements(IEnumerable<T> items)
        {
            _items = items;
        }

        public IEnumerable<T> GetElements() => _items;
    }
}
