using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public class FixedRateRebateCalculator : IRebateCalculator
    {
        public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.Price * rebate.Percentage * request.Volume;
        }

        public bool IsApplicable(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Incentive == IncentiveType.FixedRateRebate && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) && rebate.Percentage != 0 && product.Price != 0 && request.Volume != 0;
        }
    }
}
