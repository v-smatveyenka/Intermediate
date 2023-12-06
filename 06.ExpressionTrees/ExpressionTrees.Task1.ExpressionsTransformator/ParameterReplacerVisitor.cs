using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer;

public class ParameterReplacerVisitor : ExpressionVisitor
{
    private readonly Dictionary<string, int> _parameters;
    public ParameterReplacerVisitor(Dictionary<string, int> parameters)
    {
        _parameters = parameters;
    }

    protected override Expression VisitLambda<T>(Expression<T> node)
    {
        var result = Expression.Lambda(base.Visit(node.Body));

        return result;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        if (_parameters.ContainsKey(node.Name))
        {
            var value = _parameters[node.Name];

            return base.Visit(Expression.Constant(value));
        }

        return base.VisitParameter(node);
    }
}
