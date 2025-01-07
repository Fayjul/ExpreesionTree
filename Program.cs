using System.Linq.Expressions;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World");

        var sumExpressionTree = CreateExpressionTreeUsingExpressionTreeClass();
        Console.WriteLine(sumExpressionTree.NodeType); // prints "Lambda"
        Console.WriteLine(sumExpressionTree.Parameters[0].Name); // prints "number1"
        Console.WriteLine(sumExpressionTree.Parameters[1].Name); // prints "number2"
        Console.WriteLine(sumExpressionTree.Body.ToString()); // prints "(number1 + number2)"
        Console.WriteLine(sumExpressionTree.ReturnType); // prints "System.Int32"

        var compileSumExpressionTree = CompileExpressionTreeToLambdaExpression(sumExpressionTree)(1, 3);
        Console.WriteLine(compileSumExpressionTree);

    }
    public static Expression<Func<int, int, int>> CreateExpressionTreeUsingExpressionTreeClass()
    {
        ParameterExpression param1 = Expression.Parameter(typeof(int), "number1");
        ParameterExpression param2 = Expression.Parameter(typeof(int), "number2");
        BinaryExpression sumBody = Expression.Add(param1, param2);
        Expression<Func<int, int, int>> sumExpression
            = Expression.Lambda<Func<int, int, int>>(sumBody, param1, param2);
        return sumExpression;
    }
    public static Func<int, int, int> CompileExpressionTreeToLambdaExpression(Expression<Func<int, int, int>> expressionTree)
    {
        var delegateRepresentingLambdaExpression = expressionTree.Compile();

        return delegateRepresentingLambdaExpression;
    }
}
