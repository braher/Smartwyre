using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class RebateServiceTests
    {
        private readonly IRebateService _rebateService;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly Mock<IRebateRepository> _mockRebateRepo;
        private readonly List<IRebateCalculator> _calculators;

        public RebateServiceTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _mockRebateRepo = new Mock<IRebateRepository>();
            _calculators = new List<IRebateCalculator> { new Mock<IRebateCalculator>().Object };
            _rebateService = new RebateService(_calculators, _mockProductRepo.Object, _mockRebateRepo.Object);
        }

        [Fact]
        public void Calculate_ReturnsFailure_WhenRebateOrProductIsNull()
        {
            var request = new CalculateRebateRequest { ProductIdentifier = "prodId", RebateIdentifier = "rebateId" };

            _mockProductRepo.Setup(r => r.GetProductByIdentifier(It.IsAny<string>())).Returns((Product)null);
            _mockRebateRepo.Setup(r => r.GetRebateByIdentifier(It.IsAny<string>())).Returns(new Rebate());

            var result = _rebateService.Calculate(request);

            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_ReturnsFailure_WhenNoApplicableCalculatorFound()
        {
            var request = new CalculateRebateRequest { ProductIdentifier = "prodId", RebateIdentifier = "rebateId" };

            _mockProductRepo.Setup(r => r.GetProductByIdentifier(It.IsAny<string>())).Returns(new Product());
            _mockRebateRepo.Setup(r => r.GetRebateByIdentifier(It.IsAny<string>())).Returns(new Rebate());

            var result = _rebateService.Calculate(request);

            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_ReturnsSuccess_WhenApplicableCalculatorFound()
        {
            var request = new CalculateRebateRequest { ProductIdentifier = "prodId", RebateIdentifier = "rebateId" };

            _mockProductRepo.Setup(r => r.GetProductByIdentifier(It.IsAny<string>())).Returns(new Product());
            _mockRebateRepo.Setup(r => r.GetRebateByIdentifier(It.IsAny<string>())).Returns(new Rebate());

            var mockCalculator = new Mock<IRebateCalculator>();
            mockCalculator.Setup(c => c.IsApplicable(It.IsAny<Rebate>(), It.IsAny<Product>(), It.IsAny<CalculateRebateRequest>())).Returns(true);

            _calculators.Add(mockCalculator.Object);

            var result = _rebateService.Calculate(request);

            Assert.True(result.Success);
        }
    }
}
