﻿using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public class AmountPerUomRebateCalculator : IRebateCalculator
    {
        public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount * request.Volume;
        }

        public bool IsApplicable(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Incentive == IncentiveType.AmountPerUom && product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) && rebate.Amount != 0 && request.Volume != 0;
        }
    }
}
