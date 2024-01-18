using System.Xml.Linq;

namespace Composite
{
    public class Form: IXmlElement
    {
        private readonly string _name;
        private readonly List<IXmlElement> _elements;

        public Form(string name)
        {
            _name = name;
            _elements = new();
        }

        public void AddComponent(IXmlElement element)
        {
            _elements.Add(element ?? throw new ArgumentNullException(nameof(element)));
        }

        public string ConvertToString()
        {
            var body = "\t" + string.Join("\n\t", _elements.Select(x => x.ConvertToString()));

            return $@"
<form name='{_name}'>
{body}
</form>";
        }
    }
}
