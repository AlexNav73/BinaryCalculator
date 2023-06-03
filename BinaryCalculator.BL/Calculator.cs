using System;

namespace BinaryCalculator.BL;

internal sealed class Calculator : ICalculator
{
    private const int MaxValue = 1 << 12;
    private const int OverflowMask = ~((MaxValue << 1) - 1);

    private int _storedNumber;
    private int _displayNumber;
    private CalculatorState _state;
    private Operation? _lastOperation;

    public int Push(bool one)
    {
        if (_state == CalculatorState.ShowingResult)
        {
            _displayNumber = 0;
            _state = CalculatorState.WaitingForSecondNumber;
        }

        if (!IsMaxValue(_displayNumber))
        {
            _displayNumber <<= 1;
            _displayNumber |= one ? 1 : 0;
        }

        return _displayNumber;
    }

    public int Clear()
    {
        _storedNumber = 0;
        _displayNumber = 0;
        _state = CalculatorState.WaitingForFirstNumber;
        _lastOperation = null;

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
            _storedNumber = ExecuteInternal(operation);
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
                {
                    _storedNumber = _displayNumber;
                    _state = CalculatorState.ShowingResult;
                }
                break;
            case CalculatorState.ShowingResult:
            case CalculatorState.WaitingForSecondNumber:
                {
                    if (_lastOperation is not null)
                    {
                        _storedNumber = ExecuteInternal(_lastOperation.Value);
                        _displayNumber = _storedNumber;
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                    _state = CalculatorState.ShowingResult;
                }
                break;
        }

        return _displayNumber;
    }

    private int ExecuteInternal(Operation operation)
    {
        if (operation == Operation.Add)
        {
            if (((_storedNumber + _displayNumber) & OverflowMask) > 0)
            {
                return _displayNumber;
            }

            return _storedNumber + _displayNumber;
        }
        else if (operation == Operation.Subtract)
        {
            if (_storedNumber - _displayNumber < 0)
            {
                return _displayNumber;
            }

            return _storedNumber - _displayNumber;
        }

        throw new InvalidOperationException();
    }

    private bool IsMaxValue(int value)
    {
        return (value & MaxValue) == MaxValue;
    }
}
