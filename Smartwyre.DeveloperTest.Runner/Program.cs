using Microsoft.EntityFrameworkCore;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var request = new CalculateRebateRequest();

        LoadDataForRebateRequest(request);

        CalculateRebate(request);

        DisplayData(request);

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    private static void LoadDataForRebateRequest(CalculateRebateRequest request)
    {
        Console.WriteLine("Data Loading Application for CalculateRebateRequest");

        Console.Write("Enter Rebate Identifier: ");
        request.RebateIdentifier = Console.ReadLine();

        Console.Write("Enter Product Identifier: ");
        request.ProductIdentifier = Console.ReadLine();

        Console.Write("Enter Volume: ");

        decimal volume;
        while (!decimal.TryParse(Console.ReadLine(), out volume) || volume < 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid positive number for Volume.");
        }
        request.Volume = volume;
    }

    private static void DisplayData(CalculateRebateRequest request)
    {
        Console.WriteLine("\nLoaded Data:");
        Console.WriteLine($"Rebate Identifier: {request.RebateIdentifier}");
        Console.WriteLine($"Product Identifier: {request.ProductIdentifier}");
        Console.WriteLine($"Volume: {request.Volume}");
    }

    private static void CalculateRebate(CalculateRebateRequest request)
    {
        IEnumerable<IRebateCalculator> calculators = new List<IRebateCalculator> { new FixedCashAmountRebateCalculator(), new AmountPerUomRebateCalculator(), new FixedRateRebateCalculator() };
        var contextOptions = new DbContextOptionsBuilder<RetailContext>().UseSqlServer("Server=localhost;Database=master;Trusted_Connection=True;Encrypt=False").Options;

        var context = new RetailContext(contextOptions);
        IProductRepository productRepository = new ProductRepository(context);
        IRebateRepository rebateRepository = new RebateRepository(context);

        IRebateService rebateService = new RebateService(calculators, productRepository, rebateRepository);
        rebateService.Calculate(request);
    }
}
