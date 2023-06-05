using System;

namespace BinaryCalculator.BL;

internal sealed class Calculator : ICalculator
{
    private const int MaxBits = 1 << 12;
    private const int OverflowMask = ~((MaxBits << 1) - 1);

    private int _storedNumber;
    private int _displayNumber;
    private int _lastEnteredNumber;
    private CalculatorState _state;
    private Operation? _lastOperation;

    public int Push(bool one)
    {
        if (_state == CalculatorState.ShowingResult)
        {
            _displayNumber = 0;
            _state = CalculatorState.WaitingForSecondNumber;
        }

        if (!IsMaxBitsReached(_displayNumber))
        {
            _displayNumber <<= 1;
            _displayNumber |= one ? 1 : 0;
        }

        _lastEnteredNumber = _displayNumber;

        return _displayNumber;
    }

    public int Clear()
    {
        _storedNumber = 0;
        _displayNumber = 0;
        _lastEnteredNumber = 0;
        _state = CalculatorState.WaitingForFirstNumber;
        _lastOperation = null;

        return _displayNumber;
    }

    public int ClearScreen()
    {
        _displayNumber = 0;
        _lastEnteredNumber = 0;

        if (_state == CalculatorState.ShowingResult)
        {
            _storedNumber = 0;
            _lastOperation = null;
            _state = CalculatorState.WaitingForFirstNumber;
        }

        return _displayNumber;
    }

    public int Execute(Operation operation)
    {
        _lastOperation = operation;

        if (_state == CalculatorState.WaitingForFirstNumber)
        {
            _state = CalculatorState.WaitingForSecondNumber;
            _storedNumber = _displayNumber;
            _displayNumber = 0;
        }
        else if (_state == CalculatorState.WaitingForSecondNumber)
        {
            _state = CalculatorState.ShowingResult;
            _storedNumber = ExecuteInternal(operation, _displayNumber);
            _displayNumber = _storedNumber;
        }
        else if (_state == CalculatorState.ShowingResult)
        {
            _state = CalculatorState.WaitingForSecondNumber;
            _displayNumber = 0;
        }

        return _displayNumber;
    }

    public int Equals()
    {
        switch (_state)
        {
            case CalculatorState.WaitingForFirstNumber:
                break;
            case CalculatorState.ShowingResult:
            case CalculatorState.WaitingForSecondNumber:
                {
                    _storedNumber = ExecuteInternal(_lastOperation!.Value, _lastEnteredNumber);
                    _displayNumber = _storedNumber;
                    _state = CalculatorState.ShowingResult;
                }
                break;
        }

        return _displayNumber;
    }

    private int ExecuteInternal(Operation operation, int value)
    {
        if (operation == Operation.Add)
        {
            if (((_storedNumber + value) & OverflowMask) > 0)
            {
                return value;
            }

            return _storedNumber + value;
        }
        else if (operation == Operation.Subtract)
        {
            if (_storedNumber - value < 0)
            {
                throw new NotSupportedException("Negative results are not supported");
            }

            return _storedNumber - value;
        }

        throw new InvalidOperationException();
    }

    private bool IsMaxBitsReached(int value)
    {
        return (value & MaxBits) == MaxBits;
    }
}
