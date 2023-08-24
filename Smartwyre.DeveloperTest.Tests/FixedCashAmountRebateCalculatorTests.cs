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
    public class FixedCashAmountRebateCalculatorTests
    {
        private readonly IRebateCalculator _calculator;

        public FixedCashAmountRebateCalculatorTests()
        {
            _calculator = new FixedCashAmountRebateCalculator();
        }

        [Fact]
        public void Calculate_ReturnsRebateAmount()
        {
            // Arrange
            var rebate = new Rebate { Amount = 50m, Incentive = IncentiveType.FixedCashAmount };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
            var request = new CalculateRebateRequest();

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.Equal(50m, result);
        }

        [Fact]
        public void IsApplicable_ReturnsTrueForValidData()
        {
            // Arrange
            var rebate = new Rebate { Amount = 50m, Incentive = IncentiveType.FixedCashAmount };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
            var request = new CalculateRebateRequest();

            // Act
            var isApplicable = _calculator.IsApplicable(rebate, product, request);

            // Assert
            Assert.True(isApplicable);
        }

        [Theory]
        [InlineData(50, IncentiveType.FixedCashAmount, SupportedIncentiveType.FixedCashAmount, true)] // Valid scenario
        [InlineData(0, IncentiveType.FixedCashAmount, SupportedIncentiveType.FixedCashAmount, false)]  // Zero rebate amount
        [InlineData(50, IncentiveType.AmountPerUom, SupportedIncentiveType.FixedCashAmount, false)]    // Incorrect rebate incentive type
        [InlineData(50, IncentiveType.FixedCashAmount, SupportedIncentiveType.AmountPerUom, false)]    // Incorrect product supported incentive
        public void IsApplicable_ReturnsExpectedValues(decimal rebateAmount, IncentiveType rebateIncentive, SupportedIncentiveType productIncentive, bool expected)
        {
            // Arrange
            var rebate = new Rebate { Amount = rebateAmount, Incentive = rebateIncentive };
            var product = new Product { SupportedIncentives = productIncentive };
            var request = new CalculateRebateRequest();

            // Act
            var isApplicable = _calculator.IsApplicable(rebate, product, request);

            // Assert
            Assert.Equal(expected, isApplicable);
        }
    }
}
