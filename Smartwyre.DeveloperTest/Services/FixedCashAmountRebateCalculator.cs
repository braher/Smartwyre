using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public class FixedCashAmountRebateCalculator : IRebateCalculator
    {
        public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount;
        }

        public bool IsApplicable(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Incentive == IncentiveType.FixedCashAmount && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) && rebate.Amount != 0;
        }
    }
}
