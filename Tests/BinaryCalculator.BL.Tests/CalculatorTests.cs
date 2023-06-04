namespace BinaryCalculator.BL.Tests
{
    public class CalculatorTests : CalculatorTestsBase
    {
        [TestCase(new[] { true, false, true }, 0b101)]
        [TestCase(new[] { true, true, true }, 0b111)]
        [TestCase(new[] { false, false, false }, 0)]
        [TestCase(new[] { false, true, false }, 0b10)]
        [TestCase(new[] { false, false, true }, 0b1)]
        public void PushTest(bool[] bits, int expectedResult)
        {
            int result = 0;
            foreach (var bit in bits)
            {
                result = _calculator.Push(bit);
            }

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void PushNumberBiggerThenMaxTest()
        {
            var result = EnterNumber(0b1_1111_1111_1111);
            result = _calculator.Push(true);

            Assert.That(result, Is.EqualTo(0b1_1111_1111_1111));
        }

        [Test]
        public void PushAfterPressingEqualsTest()
        {
            var result = ExecuteOperation(Operation.Add, 1, 2);

            Assert.That(result, Is.EqualTo(3));

            result = EnterNumber(6);

            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void ClearTest()
        {
            var result = EnterNumber(7);

            result = _calculator.Execute(Operation.Add);

            Assert.That(result, Is.EqualTo(0));

            result = EnterNumber(7);

            result = _calculator.Clear();

            Assert.That(result, Is.EqualTo(0));
        }

        [TestCase(2, Operation.Add, 1, 3)]
        [TestCase(2, Operation.Subtract, 1, 1)]
        public void ExecuteOperationTest(int firstOperand, Operation operation, int secondOperand, int expectedResult)
        {
            var result = ExecuteOperation(operation, firstOperand, secondOperand);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AddingMultipleTimesWithoutClickingOnEqualsTest()
        {
            var result = EnterNumber(1);
            result = _calculator.Execute(Operation.Add);
            result = EnterNumber(1);
            result = _calculator.Execute(Operation.Add);

            Assert.That(result, Is.EqualTo(2));

            result = EnterNumber(1);
            result = _calculator.Execute(Operation.Add);

            Assert.That(result, Is.EqualTo(3));

            result = EnterNumber(1);
            result = _calculator.Execute(Operation.Add);

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void ClickingOnEqualsMultipleTimesTest()
        {
            var result = ExecuteOperation(Operation.Add, 1, 3);

            Assert.That(result, Is.EqualTo(4));

            result = _calculator.Equals();

            Assert.That(result, Is.EqualTo(7));

            result = _calculator.Equals();

            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void NegativeResultsAreNotSupportedTest()
        {
            Assert.Throws<NotSupportedException>(() => ExecuteOperation(Operation.Subtract, 1, 5));
        }

        [Test]
        public void PressingEqualsAfterFirstNumberEnteredDoNothingTest()
        {
            var result = EnterNumber(5);
            result = _calculator.Equals();

            Assert.That(result, Is.EqualTo(5));

            result = _calculator.Execute(Operation.Add);

            Assert.That(result, Is.EqualTo(0));

            result = EnterNumber(5);
            result = _calculator.Equals();

            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void ClearScreenTest()
        {
            var result = EnterNumber(1);
            result = _calculator.ClearScreen();

            Assert.That(result, Is.EqualTo(0));

            result = EnterNumber(2);

            Assert.That(result, Is.EqualTo(2));

            result = _calculator.Execute(Operation.Add);
            result = EnterNumber(2);

            Assert.That(result, Is.EqualTo(2));
            result = _calculator.ClearScreen();

            result = EnterNumber(2);
            Assert.That(result, Is.EqualTo(2));

            result = _calculator.Equals();

            Assert.That(result, Is.EqualTo(4));

            result = _calculator.Equals();

            Assert.That(result, Is.EqualTo(6));
        }
    }
}