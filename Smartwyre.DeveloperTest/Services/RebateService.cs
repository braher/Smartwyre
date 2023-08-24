using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IEnumerable<IRebateCalculator> _calculators;
    private readonly IProductRepository _productRepository;
    private readonly IRebateRepository _rebateRepository;

    public RebateService(IEnumerable<IRebateCalculator> calculators, IProductRepository productRepository, IRebateRepository rebateRepository)
    {
        _calculators = calculators;
        _productRepository = productRepository;
        _rebateRepository = rebateRepository;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var result = new CalculateRebateResult();
        var rebateAmount = 0m;

        Product product = _productRepository.GetProductByIdentifier(request.ProductIdentifier);
        Rebate rebate = _rebateRepository.GetRebateByIdentifier(request.RebateIdentifier);

        if (rebate is null || product is null)
        {
            result.Success = false;
            return result;
        }

        var applicableCalculator = _calculators.FirstOrDefault(c => c.IsApplicable(rebate, product, request));

        if (applicableCalculator is not null)
        {
            rebateAmount = applicableCalculator.Calculate(rebate, product, request);
            _rebateRepository.StoreCalculationResult(rebate, rebateAmount);
            result.Success = true;
        }
        else
        {
            result.Success = false;
        }

        return result;
    }

}
