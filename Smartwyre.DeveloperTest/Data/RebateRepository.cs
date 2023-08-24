using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Data
{
    public class RebateRepository : IRebateRepository
    {
        private readonly RetailContext _context;

        public RebateRepository(RetailContext context)
        {
            _context = context;
        }
        public void AddRebate(Rebate rebate)
        {
            _context.Rebates.Add(rebate);
        }

        public void DeleteRebate(int rebateId)
        {
            _context.Rebates.Remove(GetRebateById(rebateId));
        }

        public IEnumerable<Rebate> GetAllRebates()
        {
            return _context.Rebates.ToList();
        }

        public Rebate GetRebateById(int rebateId)
        {
            return _context.Rebates.FirstOrDefault(r => r.Id == rebateId);
        }

        public Rebate GetRebateByIdentifier(string rebateIdentifier)
        {
            return _context.Rebates.FirstOrDefault(r => r.Identifier == rebateIdentifier);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void StoreCalculationResult(Rebate account, decimal rebateAmount)
        {
            //update the rebate amount
            account.Amount = rebateAmount;
            _context.Rebates.Update(account);
            _context.SaveChanges();
        }

        public void UpdateRebate(Rebate rebate)
        {
            _context.Rebates.Update(rebate);
        }
    }
}
