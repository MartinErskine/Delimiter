using NUnit.Framework;

namespace Delimiter.Tests
{
    public class DelimiterUnitTests
    {
        private DelimiterService _delimiterService;

        [SetUp]
        public void Setup()
        {
            _delimiterService = new DelimiterService();
        }

        [Test]  
        public void NoStringReturnsZero()
        {
            string[] argument = {};

            var result = _delimiterService.Add(argument);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void OneValueReturnsOneValue()
        {
            string[] argument = { "1" };

            var result = _delimiterService.Add(argument);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void TwoValuesReturnsSummedValue()
        {
            string[] argument = { "1,2" };

            var result = _delimiterService.Add(argument);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void ThreeValuesWithNewlineReturnsSummedValue()
        {
            string[] argument = { @"1\n2,3" };

            var result = _delimiterService.Add(argument);

            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void TwoValuesWithCustomDelimiterReturnsSummedValue()
        {
            string[] argument = { @"//;\n1;2" };

            var result = _delimiterService.Add(argument);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void NegativeValuesReturnListOfNegativesProvided()
        {
            string[] argument = { @"1,-2,3,4,-5,6,7" };

            var result = _delimiterService.Add(argument);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void IncludedValueIsGreaterThanOneThousandReturnsDefaultValue()
        {
            string[] argument = { @"2,1001" };

            var result = _delimiterService.Add(argument);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void CustomDelimiterReturnsSummedValue()
        {
            string[] argument = { @"//[***]\n1***2***3" };

            var result = _delimiterService.Add(argument);

            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void MultipleSingleCustomDelimitersReturnsSummedValue()
        {
            string[] argument = { @"//[*][%]\n1*2%3" };

            var result = _delimiterService.Add(argument);

            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void MultipleCustomDelimitersWithMultipleCharactersReturnsSummedValue()
        {
            string[] argument = { @"//[*.][%,]\n1*.2%,3" };

            var result = _delimiterService.Add(argument);

            Assert.That(result, Is.EqualTo(6));
        }
    }
}