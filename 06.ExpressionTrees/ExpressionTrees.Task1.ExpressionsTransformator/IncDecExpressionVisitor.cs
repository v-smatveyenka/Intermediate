using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer;

public class IncDecExpressionVisitor : ExpressionVisitor
{
    // todo: feel free to add your code here
    protected override Expression VisitBinary(BinaryExpression node)
    {
        if (node.Right is ConstantExpression rightConstant && rightConstant.Value is int @int && @int == 1)
        {
            if (node.NodeType != ExpressionType.Add && node.NodeType != ExpressionType.Subtract)
            {
                return base.VisitBinary(node);
            }

            if (node.Left is ParameterExpression || node.Right is ConstantExpression leftConstant && leftConstant.Value is int)
            {
                if (node.NodeType == ExpressionType.Add)
                {
                    return Expression.Increment(node.Left);
                }

                return Expression.Decrement(node.Left);
            }
            else if (node.Left is BinaryExpression left && (left.NodeType == ExpressionType.Add || left.NodeType == ExpressionType.Subtract))
            {
                Expression right;
                if (node.NodeType == ExpressionType.Add)
                {
                    right = Expression.Increment(left.Right);
                }
                else
                {
                    right = Expression.Decrement(left.Right);
                }

                if (left.NodeType == ExpressionType.Add)
                {
                    return Expression.Add(Visit(left.Left), right);
                }
                else
                {
                    return Expression.Subtract(Visit(left.Left), right);
                }
            }

            return base.VisitBinary(node);
        }

        return base.VisitBinary(node);
    }
}
