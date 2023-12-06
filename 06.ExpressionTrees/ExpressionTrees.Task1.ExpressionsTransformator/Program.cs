/*
 * Create a class based on ExpressionVisitor, which makes expression tree transformation:
 * 1. converts expressions like <variable> + 1 to increment operations, <variable> - 1 - into decrement operations.
 * 2. changes parameter values in a lambda expression to constants, taking the following as transformation parameters:
 *    - source expression;
 *    - dictionary: <parameter name: value for replacement>
 * The results could be printed in console or checked via Debugger using any Visualizer.
 */
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Expression Visitor for increment/decrement.");
        Console.WriteLine();

        Expression<Func<int, int, int, int>> sourceExpression = (param1, param2, param3) => (param1 + 1) * (param2 - 1) - (param3 + 1);
        Console.WriteLine($"Source Expression: {sourceExpression}");

        var incDecExpressionVisitor = new IncDecExpressionVisitor();
        var result = incDecExpressionVisitor.Visit(sourceExpression);
        Console.WriteLine($"Result of source expression: {result}\n");

        var parameterReplacerVisitor = new ParameterReplacerVisitor(
            new Dictionary<string, int>()
            {
                { "param1", 5 },
                { "param2", 7 },
                { "param3", 17 },
            }
        );

        var convertedExpression = parameterReplacerVisitor.Visit(sourceExpression);
        Console.WriteLine($"Converted expression: {convertedExpression}");

        result = incDecExpressionVisitor.Visit(convertedExpression);
        Console.WriteLine($"Result of source expression: {result}\n");

        var compiled = ((Expression<Func<int>>)convertedExpression).Compile();
        Console.WriteLine($"Result: {compiled()}");

        Console.ReadLine();
    }
}
