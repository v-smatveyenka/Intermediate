namespace Adapter
{
    public class ContainerAdapter<T> : IContainer<T>
    {
        private readonly IElements<T> _elements;

        public ContainerAdapter(IElements<T> elements)
        {
            _elements = elements;
        }

        public IEnumerable<T> Items => _elements.GetElements();

        public int Count => _elements.GetElements().Count();
    }
}
