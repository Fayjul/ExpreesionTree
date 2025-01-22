using System.Linq.Expressions;

class Program
{
    public static async Task Main(string[] args)
    {

        BindingPractice();
    }
    public class Animal
    {
        public void Speak()
        {
            Console.WriteLine("The animal makes a sound.");
        }
    }
    private static void BindingPractice()
    {
        Console.WriteLine("Binding Practice start");
        Animal animal = new Animal();
        animal.Speak(); // Programe know which method need to execute. 
        //animal.Speaks(); //This line will give a error during compilation.

        dynamic animal2 = new Animal();
        animal2.Speaks(); // Don't have compilation error

    }
    private static async Task<string> TaskPracticeAsync()
    {
        Task myTask1 = Task.Run(async () =>
        {
            Console.WriteLine("Chef 1 is preparing Dish 1");
            await Task.Delay(10000); // Chef 1 takes 10 seconds to prepare Dish 1
            Console.WriteLine("Dish 1 is ready!");
        });

        Task myTask2 = Task.Run(async () =>
        {
            Console.WriteLine("Chef 2 is preparing Dish 2");
            await Task.Delay(1000); // Chef 2 takes 1 second to prepare Dish 2
            Console.WriteLine("Dish 2 is ready!");
        });

        Task myTask3 = Task.Run(async () =>
        {
            Console.WriteLine("Chef 3 is preparing Dish 3");
            await Task.Delay(1000); // Chef 3 takes 1 second to prepare Dish 3
            Console.WriteLine("Dish 3 is ready!");
        });

        await Task.WhenAll(myTask1, myTask2, myTask3);

        return "Manager: All dishes are ready! Task completed!";

        //Console.WriteLine("Manager: All dishes are ready! Task completed!");
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
    static async void CountNumber()
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
