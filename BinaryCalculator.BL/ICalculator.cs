namespace BinaryCalculator.BL;

public interface ICalculator
{
    int Push(bool one);

    int Clear();

    int ClearScreen();

    int Execute(Operation operation);

    int Equals();
}
