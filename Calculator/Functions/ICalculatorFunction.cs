namespace Calculator.Functions
{
    public interface ICalculatorFunction
    {
        string Name { get; }
        int ArgumentCount { get; }

        object Calculate(object[] arguments);
    }
}