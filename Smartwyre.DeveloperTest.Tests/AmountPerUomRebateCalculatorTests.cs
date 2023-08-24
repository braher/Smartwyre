using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class AmountPerUomRebateCalculatorTests
    {
        private readonly IRebateCalculator _calculator;

        public AmountPerUomRebateCalculatorTests()
        {
            _calculator = new AmountPerUomRebateCalculator();
        }

        [Theory]
        [InlineData(10, 5, 50)]
        [InlineData(3, 3, 9)]
        public void Calculate_ReturnsExpectedValue(decimal rebateAmount, decimal volume, decimal expected)
        {
            // Arrange
            var rebate = new Rebate { Amount = rebateAmount, Incentive = IncentiveType.AmountPerUom };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
            var request = new CalculateRebateRequest { Volume = volume };

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 5, IncentiveType.AmountPerUom, SupportedIncentiveType.AmountPerUom, true)]   // Valid scenario
        [InlineData(0, 5, IncentiveType.AmountPerUom, SupportedIncentiveType.AmountPerUom, false)]    // Zero rebate amount
        [InlineData(10, 0, IncentiveType.AmountPerUom, SupportedIncentiveType.AmountPerUom, false)]   // Zero request volume
        [InlineData(10, 5, IncentiveType.FixedCashAmount, SupportedIncentiveType.AmountPerUom, false)]// Incorrect rebate incentive type
        [InlineData(10, 5, IncentiveType.AmountPerUom, SupportedIncentiveType.FixedCashAmount, false)]// Incorrect product supported incentive
        public void IsApplicable_ReturnsExpectedValues(decimal rebateAmount, decimal volume, IncentiveType rebateIncentive, SupportedIncentiveType productIncentive, bool expected)
        {
            // Arrange
            var rebate = new Rebate { Amount = rebateAmount, Incentive = rebateIncentive };
            var product = new Product { SupportedIncentives = productIncentive };
            var request = new CalculateRebateRequest { Volume = volume };

            // Act
            var isApplicable = _calculator.IsApplicable(rebate, product, request);

            // Assert
            Assert.Equal(expected, isApplicable);
        }
    }
}
