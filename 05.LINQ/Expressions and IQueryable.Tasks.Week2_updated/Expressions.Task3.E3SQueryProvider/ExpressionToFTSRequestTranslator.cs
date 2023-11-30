using Expressions.Task3.E3SQueryProvider.Helpers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider
{
    public class ExpressionToFtsRequestTranslator : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;

        public ExpressionToFtsRequestTranslator()
        {
            _resultStringBuilder = new StringBuilder();
        }

        public string Translate(Expression exp)
        {
            Visit(exp);

            return _resultStringBuilder.ToString();
        }

        #region protected methods

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }

            if (node.Method.DeclaringType == typeof(string)
                && QueryHelper.QueryTranslator.ContainsKey(node.Method.Name))
            {
                var translatedBrackets = QueryHelper.QueryTranslator[node.Method.Name];

                Visit(node.Object);
                _resultStringBuilder.Append(translatedBrackets.Left);
                Visit(node.Arguments[0]);
                _resultStringBuilder.Append(translatedBrackets.Right);

                return node;
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:

                    if (!ValidNodeTypes(node))
                    {
                        throw new NotSupportedException("NodeTypes are not supported");
                    }

                    var memberAccess = node.Left.NodeType == ExpressionType.MemberAccess ? node.Left : node.Right;
                    var constant = node.Left.NodeType == ExpressionType.Constant ? node.Left : node.Right;

                    Visit(memberAccess);
                    _resultStringBuilder.Append("(");
                    Visit(constant);
                    _resultStringBuilder.Append(")");
                    break;
                case ExpressionType.AndAlso:
                    Visit(node.Left);
                    _resultStringBuilder.Append($" {QueryHelper.Operator.And} ");
                    Visit(node.Right);
                    break;

                default:
                    throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultStringBuilder.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append(node.Value);

            return node;
        }

        #endregion

        private static bool ValidNodeTypes(BinaryExpression node)
        {
            if (node.Left.NodeType != ExpressionType.MemberAccess && node.Left.NodeType != ExpressionType.Constant)
            {
                return false;
            }

            if (node.Right.NodeType != ExpressionType.MemberAccess && node.Right.NodeType != ExpressionType.Constant)
            {
                return false;
            }

            if (node.Left.NodeType == ExpressionType.MemberAccess && node.Right.NodeType != ExpressionType.Constant
                || node.Left.NodeType == ExpressionType.Constant && node.Right.NodeType != ExpressionType.MemberAccess)
            {
                return false;
            }

            if (node.Right.NodeType == ExpressionType.MemberAccess && node.Left.NodeType != ExpressionType.Constant
               || node.Right.NodeType == ExpressionType.Constant && node.Left.NodeType != ExpressionType.MemberAccess)
            {
                return false;
            }

            return true;
        }
    }
}
