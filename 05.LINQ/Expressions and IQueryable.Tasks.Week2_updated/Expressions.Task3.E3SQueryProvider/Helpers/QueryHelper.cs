using System.Collections.Generic;

namespace Expressions.Task3.E3SQueryProvider.Helpers
{
    public static class QueryHelper
    {
        public static readonly IReadOnlyDictionary<string, Brackets> QueryTranslator = new Dictionary<string, Brackets>
        {
            {"Equals", new Brackets("(", ")") },
            {"StartsWith", new Brackets("(", "*)") },
            {"EndsWith", new Brackets ("(*", ")") },
            {"Contains", new Brackets("(*", "*)") }
        };

        public static readonly string[] Operators = new string[] { Operator.And };

        public static class Operator
        {
            public const string And = "AND";
        }
    }
}
