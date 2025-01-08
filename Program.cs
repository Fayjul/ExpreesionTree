﻿using System.Linq.Expressions;

class Program
{
    public static void Main(string[] args)
    {

        ThreadPractice();
    }
    private static void ThreadPractice()
    {
        Console.WriteLine("Practising Theread");
        Thread thread = new Thread(CountNumber);

        thread.Start();

        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine("Main Thread " + i);
        }
        thread.Join();

        Console.WriteLine("Joined");

    }
    static void CountNumber()
    {
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine("Second Thread " + i);
        }
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
    private void expressionTree()
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
}
