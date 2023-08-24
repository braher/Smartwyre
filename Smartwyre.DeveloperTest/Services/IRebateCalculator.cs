using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Services
{
    public interface IRebateCalculator
    {
        bool IsApplicable(Rebate rebate, Product product, CalculateRebateRequest request);
        decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request);

    }
}

