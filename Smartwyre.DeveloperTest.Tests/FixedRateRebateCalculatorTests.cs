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
    public  class FixedRateRebateCalculatorTests
    {
        private readonly IRebateCalculator _calculator;

        public FixedRateRebateCalculatorTests()
        {
            _calculator = new FixedRateRebateCalculator();
        }

        [Fact]
        public void Calculate_ReturnsCorrectValue()
        {
            // Arrange
            var rebate = new Rebate { Percentage = 0.1m, Incentive = IncentiveType.FixedRateRebate };
            var product = new Product { Price = 100m, SupportedIncentives = SupportedIncentiveType.FixedRateRebate };
            var request = new CalculateRebateRequest { Volume = 2 };

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.Equal(20m, result);  // 100 * 0.1 * 2
        }

        [Fact]
        public void IsApplicable_ReturnsTrueForValidData()
        {
            // Arrange
            var rebate = new Rebate { Percentage = 0.1m, Incentive = IncentiveType.FixedRateRebate };
            var product = new Product { Price = 100m, SupportedIncentives = SupportedIncentiveType.FixedRateRebate };
            var request = new CalculateRebateRequest { Volume = 2 };

            // Act
            var isApplicable = _calculator.IsApplicable(rebate, product, request);

            // Assert
            Assert.True(isApplicable);
        }

        [Theory]
        [InlineData(0, IncentiveType.FixedRateRebate, 100, SupportedIncentiveType.FixedRateRebate, 2, false)] // Zero percentage
        [InlineData(0.1, IncentiveType.AmountPerUom, 100, SupportedIncentiveType.FixedRateRebate, 2, false)] // Incorrect rebate incentive type
        [InlineData(0.1, IncentiveType.FixedRateRebate, 0, SupportedIncentiveType.FixedRateRebate, 2, false)] // Zero product price
        [InlineData(0.1, IncentiveType.FixedRateRebate, 100, SupportedIncentiveType.AmountPerUom, 2, false)] // Incorrect product supported incentive
        [InlineData(0.1, IncentiveType.FixedRateRebate, 100, SupportedIncentiveType.FixedRateRebate, 0, false)] // Zero request volume
        public void IsApplicable_ReturnsExpectedValues(decimal rebatePercentage, IncentiveType rebateIncentive, decimal productPrice, SupportedIncentiveType productIncentive, int requestVolume, bool expected)
        {
            // Arrange
            var rebate = new Rebate { Percentage = rebatePercentage, Incentive = rebateIncentive };
            var product = new Product { Price = productPrice, SupportedIncentives = productIncentive };
            var request = new CalculateRebateRequest { Volume = requestVolume };

            // Act
            var isApplicable = _calculator.IsApplicable(rebate, product, request);

            // Assert
            Assert.Equal(expected, isApplicable);
        }
    }
}
