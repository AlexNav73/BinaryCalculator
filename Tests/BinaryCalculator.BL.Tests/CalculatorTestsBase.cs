namespace BinaryCalculator.BL.Tests;

public abstract class CalculatorTestsBase
{
    protected ICalculator _calculator;

    [SetUp]
    public void SetUp()
    {
        _calculator = new Calculator();
    }

    protected int ExecuteOperation(Operation operation, int firstOperand, int secondOperand)
    {
        EnterNumber(firstOperand);
        _calculator.Execute(operation);
        EnterNumber(secondOperand);

        var result = _calculator.Equals();

        return result;
    }

    protected int EnterNumber(int number)
    {
        var mask = MostSignificantBit(number);
        var result = 0;

        while (mask > 0)
        {
            result = _calculator.Push((number & mask) == mask);
            mask >>= 1;
        }

        Assert.That(result, Is.EqualTo(number));

        return result;
    }

    private int MostSignificantBit(int x)
    {
        // folding a number to have only 1s in it's binary representation
        x |= (x >> 1);
        x |= (x >> 2);
        x |= (x >> 4);
        x |= (x >> 8);
        x |= (x >> 16);

        // shifting by 1 and negate operation makes all bits from the left became 1s and
        // all bits from the right 0s. AND operation between all-1s-mask and the result of
        // negation makes the only significant bit stay 1
        return x & ~(x >> 1);
    }
}
