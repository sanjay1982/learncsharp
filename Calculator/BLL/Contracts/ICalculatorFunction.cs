namespace Calculator.BLL.Contracts
{
    public interface ICalculatorFunction
    {
        string Name { get; }
        int ArgumentCount { get; }
        
    }
}